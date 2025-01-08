using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MT.Utilities.Mapper
{
    public static class ObjectMapper
    {
        /// <summary>
        /// 將來源物件的屬性值映射到目標物件的相同名稱屬性
        /// </summary>
        public static TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : new()
        {
            if (source == null)
                return default(TDestination);

            TDestination destination = new TDestination();
            return Map(source, destination);
        }

        /// <summary>
        /// 將來源物件的屬性值映射到現有的目標物件
        /// </summary>
        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (source == null)
                return destination;
            if (destination == null)
                return default(TDestination);

            MapInternal(source, destination, new HashSet<object>());
            return destination;
        }

        /// <summary>
        /// 將 IEnumerable<TSource> 映射為 IEnumerable<TDestination>
        /// </summary>
        public static IEnumerable<TDestination> MapCollection<TSource, TDestination>(IEnumerable<TSource> source)
            where TDestination : new()
        {
            if (source == null)
                return Enumerable.Empty<TDestination>();

            return source.Select(item => item != null ? Map<TSource, TDestination>(item) : default(TDestination))
                        .Where(item => item != null);
        }

        private static void MapInternal(object source, object destination, HashSet<object> mappedObjects)
        {
            if (source == null || destination == null || mappedObjects.Contains(source))
                return;

            mappedObjects.Add(source);

            var sourceType = source.GetType();
            var destinationType = destination.GetType();

            var sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var destinationProperties = destinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = destinationProperties.FirstOrDefault(x =>
                    x.Name == sourceProperty.Name &&
                    x.CanWrite);

                if (destinationProperty == null) continue;

                try
                {
                    var sourceValue = sourceProperty.GetValue(source);

                    // 如果源值為 null，直接設定目標為 null
                    if (sourceValue == null)
                    {
                        destinationProperty.SetValue(destination, null);
                        continue;
                    }

                    // 處理集合類型
                    if (IsCollectionType(sourceProperty.PropertyType))
                    {
                        MapCollection(sourceValue, destination, destinationProperty, mappedObjects);
                        continue;
                    }

                    // 處理複雜類型（深層映射）
                    if (IsComplexType(sourceProperty.PropertyType))
                    {
                        var destinationValue = destinationProperty.GetValue(destination);

                        // 如果目標值為 null，建立新實例
                        if (destinationValue == null && !destinationProperty.PropertyType.IsAbstract)
                        {
                            destinationValue = Activator.CreateInstance(destinationProperty.PropertyType);
                            destinationProperty.SetValue(destination, destinationValue);
                        }

                        // 只有當目標值不為 null 時才進行映射
                        if (destinationValue != null)
                        {
                            MapInternal(sourceValue, destinationValue, mappedObjects);
                        }
                        continue;
                    }

                    // 基本類型轉換
                    if (CanDirectlyMap(sourceProperty.PropertyType, destinationProperty.PropertyType))
                    {
                        var convertedValue = Convert.ChangeType(sourceValue, destinationProperty.PropertyType);
                        destinationProperty.SetValue(destination, convertedValue);
                    }
                }
                catch (Exception ex)
                {
                    // 這裡可以選擇記錄錯誤但繼續執行，或者拋出例外
                    throw new InvalidOperationException(
                        $"Error mapping property {sourceProperty.Name} from {sourceType} to {destinationType}",
                        ex);
                }
            }
        }

        private static void MapCollection(object sourceCollection, object destination, PropertyInfo destinationProperty, HashSet<object> mappedObjects)
        {
            if (sourceCollection == null)
            {
                destinationProperty.SetValue(destination, null);
                return;
            }

            var destinationType = destinationProperty.PropertyType;
            var elementType = GetElementType(destinationType);

            if (elementType == null) return;

            try
            {
                // 創建適當類型的目標集合
                object destinationCollection;
                if (destinationType.IsArray)
                {
                    var sourceList = ((IEnumerable)sourceCollection).Cast<object>().ToList();
                    destinationCollection = Array.CreateInstance(elementType, sourceList.Count);
                }
                else if (destinationType.IsInterface && destinationType.IsGenericType)
                {
                    var collectionType = typeof(List<>).MakeGenericType(elementType);
                    destinationCollection = Activator.CreateInstance(collectionType);
                }
                else
                {
                    destinationCollection = Activator.CreateInstance(destinationType);
                }

                var index = 0;
                foreach (var sourceItem in (IEnumerable)sourceCollection)
                {
                    if (sourceItem == null) continue;

                    object destinationItem;
                    if (IsComplexType(sourceItem.GetType()))
                    {
                        destinationItem = Activator.CreateInstance(elementType);
                        MapInternal(sourceItem, destinationItem, mappedObjects);
                    }
                    else
                    {
                        destinationItem = Convert.ChangeType(sourceItem, elementType);
                    }

                    if (destinationType.IsArray)
                    {
                        ((Array)destinationCollection).SetValue(destinationItem, index);
                    }
                    else
                    {
                        ((IList)destinationCollection).Add(destinationItem);
                    }
                    index++;
                }

                destinationProperty.SetValue(destination, destinationCollection);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Error mapping collection property {destinationProperty.Name}",
                    ex);
            }
        }

        private static bool IsCollectionType(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string);
        }

        private static bool IsComplexType(Type type)
        {
            return !type.IsPrimitive &&
                   !type.IsEnum &&
                   type != typeof(string) &&
                   type != typeof(decimal) &&
                   type != typeof(DateTime) &&
                   type != typeof(DateTimeOffset) &&
                   type != typeof(TimeSpan) &&
                   type != typeof(Guid);
        }

        private static bool CanDirectlyMap(Type sourceType, Type destinationType)
        {
            if (sourceType == destinationType) return true;
            if (destinationType.IsAssignableFrom(sourceType)) return true;

            try
            {
                var sourceDefault = sourceType.IsValueType ? Activator.CreateInstance(sourceType) : null;
                Convert.ChangeType(sourceDefault, destinationType);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static Type GetElementType(Type type)
        {
            if (type.IsArray)
                return type.GetElementType();

            var enumerableType = type.GetInterfaces()
                .Union(new[] { type })
                .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            return enumerableType?.GetGenericArguments()[0];
        }
    }
}
