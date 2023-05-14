
using Core.Entities;
using Core.Enums;
using Core.Values;
using Microsoft.EntityFrameworkCore;

namespace Core.UnitTest
{
    public class HierarchyInMemoryTest : InMemoryDbBaseTest
    {
        [Test]
        public void test_create()
        {
            var root = new HierarchyItem()
            {
                Title = "Root",
                ItemType = HierarchyType.Start,
            };

            _ctx.HierarchyItemSet.Add(root);
            _ctx.SaveChanges();

            var child = new HierarchyItem()
            {
                Title = "A",
                Parent = root,
                ItemType = HierarchyType.Value,
                Metadata = new ItemMetaData()
                {
                    Name = "A",
                    Type = "value",
                    Value = "Test123",
                },
                MetadataList = new List<ItemMetaData>() {
                    new ItemMetaData() {
                        Name = "A",
                        Type = "value",
                        Value = "Test123",
                    },
                },
            };
            _ctx.HierarchyItemSet.Add(child);
            _ctx.SaveChanges();

            var gChild = new HierarchyItem()
            {
                Title = "A-A",
                Parent = child,
                ItemType = HierarchyType.Value,
                Metadata = new ItemMetaData()
                {
                    Name = "A-A",
                    Type = "value",
                    Value = "Test123",
                },
                MetadataList = new List<ItemMetaData>() {
                    new ItemMetaData() {
                        Name = "A-A",
                        Type = "value",
                        Value = "Test123",
                    },
                },
            };
            _ctx.HierarchyItemSet.Add(gChild);
            _ctx.SaveChanges();

            var actural = _ctx.HierarchyItemSet.SingleOrDefault(c => c.Id == root.Id);
            Assert.IsNotNull(actural);
            Assert.IsNull(actural.Parent);
            Assert.IsTrue(actural.Children.Count > 0 && actural.Children.Any(c => c.Id == child.Id));
        }
    }

    public class HierarchyItemSqlServerTest : SqlServerDbBaseTest
    {

        [Test]
        public void test_create()
        {
            var root = new HierarchyItem()
            {
                Title = "Root",
                ItemType = HierarchyType.Start,
                Parent = null,
            };

            _ctx.HierarchyItemSet.Add(root);
            _ctx.SaveChanges();

            var child = new HierarchyItem()
            {
                Title = "A",
                Parent = root,
                ItemType = HierarchyType.Value,
                Metadata = new ItemMetaData()
                {
                    Name = "A",
                    Type = "value",
                    Value = "Test123",
                },

                MetadataList = new List<ItemMetaData>() {
                    new ItemMetaData() {
                        Name = "A",
                        Type = "value",
                        Value = "Test123",
                    },
                },
            };
            _ctx.HierarchyItemSet.Add(child);
            _ctx.SaveChanges();

            var gChild = new HierarchyItem()
            {
                Title = "A-A",
                Parent = child,
                ItemType = HierarchyType.Value,
                Metadata = new ItemMetaData()
                {
                    Name = "A-A",
                    Type = "value",
                    Value = "Test123-test456",
                },
                MetadataList = new List<ItemMetaData>() {
                    new ItemMetaData() {
                        Name = "A-A",
                        Type = "value",
                        Value = "Test123-test456",
                    },
                },
            };
            _ctx.HierarchyItemSet.Add(gChild);
            _ctx.SaveChanges();

            var actural = _ctx.HierarchyItemSet.SingleOrDefault(c => c.Id == root.Id);
            Assert.IsNotNull(actural);
            Assert.IsNull(actural.Parent);
            Assert.IsTrue(actural.Children.Count > 0 && actural.Children.Any(c => c.Id == child.Id));

        }
    }

}