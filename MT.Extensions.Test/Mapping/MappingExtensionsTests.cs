using NUnit.Framework;
using MT.Extensions;
using System;

namespace MT.Extensions.Test.Mapping
{
    public class SourceObj { public int Id { get; set; } public string Name { get; set; } }
    public class DestObj { public int Id { get; set; } public string Name { get; set; } }
    public class DestObjDiffType { public string Id { get; set; } public string Name { get; set; } }

    [TestFixture]
    public class MappingExtensionsTests
    {
        [Test]
        public void MapTo_同型別屬性_回傳正確映射()
        {
            var src = new SourceObj { Id = 1, Name = "A" };
            var dest = src.MapTo<SourceObj, DestObj>();
            Assert.AreEqual(1, dest.Id);
            Assert.AreEqual("A", dest.Name);
        }

        [Test]
        public void MapTo_型別可轉換_回傳正確映射()
        {
            var src = new SourceObj { Id = 2, Name = "B" };
            var dest = src.MapTo<SourceObj, DestObjDiffType>();
            Assert.AreEqual("2", dest.Id);
            Assert.AreEqual("B", dest.Name);
        }

        [Test]
        public void MapTo_來源為Null_回傳default()
        {
            SourceObj src = null;
            var dest = src.MapTo<SourceObj, DestObj>();
            Assert.IsNull(dest);
        }

        [Test]
        public void MapTo_更新現有目標物件_回傳正確結果()
        {
            var src = new SourceObj { Id = 3, Name = "C" };
            var dest = new DestObj { Id = 0, Name = "" };
            src.MapTo(dest);
            Assert.AreEqual(3, dest.Id);
            Assert.AreEqual("C", dest.Name);
        }
    }
}
