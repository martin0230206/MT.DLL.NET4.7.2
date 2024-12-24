using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MT.Extensions
{
	/// <summary>
	/// 提供物件轉換功能，自動對應相同名稱的屬性
	/// </summary>
	public static class MappingExtensions
	{
		/// <summary>
		/// 將來源物件轉換為目標型別，自動對應相同名稱的屬性
		/// </summary>
		/// <typeparam name="TSource">來源型別</typeparam>
		/// <typeparam name="TDestination">目標型別</typeparam>
		/// <param name="source">來源物件</param>
		/// <returns>轉換後的目標物件</returns>
		public static TDestination MapTo<TSource, TDestination>(this TSource source)
			where TDestination : new()
		{
			if (source == null)
				return default;

			var destination = new TDestination();
			var sourceProperties = typeof(TSource).GetProperties();
			var destinationProperties = typeof(TDestination).GetProperties();

			foreach (var sourceProperty in sourceProperties)
			{
				// 尋找目標型別中相同名稱且可寫入的屬性
				var destinationProperty = destinationProperties.FirstOrDefault(x =>
					x.Name == sourceProperty.Name && x.CanWrite
				);

				if (destinationProperty != null)
				{
					try
					{
						var value = sourceProperty.GetValue(source);

						// 處理可為空值型別的轉換
						if (value != null)
						{
							// 如果屬性型別不同但可以轉換，進行轉換
							if (destinationProperty.PropertyType != sourceProperty.PropertyType)
							{
								value = Convert.ChangeType(
									value,
									Nullable.GetUnderlyingType(destinationProperty.PropertyType)
										?? destinationProperty.PropertyType
								);
							}
							destinationProperty.SetValue(destination, value);
						}
					}
					catch (Exception ex)
					{
						// 轉換失敗時可以選擇處理方式，這裡選擇略過並繼續
						Console.WriteLine(
							$"Property {sourceProperty.Name} mapping failed: {ex.Message}"
						);
						continue;
					}
				}
			}

			return destination;
		}

		/// <summary>
		/// 將來源物件轉換為目標型別，並更新現有目標物件的屬性值
		/// </summary>
		/// <typeparam name="TSource">來源型別</typeparam>
		/// <typeparam name="TDestination">目標型別</typeparam>
		/// <param name="source">來源物件</param>
		/// <param name="destination">目標物件</param>
		/// <returns>更新後的目標物件</returns>
		public static TDestination MapTo<TSource, TDestination>(
			this TSource source,
			TDestination destination
		)
		{
			if (source == null || destination == null)
				return destination;

			var sourceProperties = typeof(TSource).GetProperties();
			var destinationProperties = typeof(TDestination).GetProperties();

			foreach (var sourceProperty in sourceProperties)
			{
				var destinationProperty = destinationProperties.FirstOrDefault(x =>
					x.Name == sourceProperty.Name && x.CanWrite
				);

				if (destinationProperty != null)
				{
					try
					{
						var value = sourceProperty.GetValue(source);

						if (value != null)
						{
							if (destinationProperty.PropertyType != sourceProperty.PropertyType)
							{
								value = Convert.ChangeType(
									value,
									Nullable.GetUnderlyingType(destinationProperty.PropertyType)
										?? destinationProperty.PropertyType
								);
							}
							destinationProperty.SetValue(destination, value);
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine(
							$"Property {sourceProperty.Name} mapping failed: {ex.Message}"
						);
						continue;
					}
				}
			}

			return destination;
		}
	}
}
