using DDD.Model.Entities;
using DDD.Model.Enums;
using DDD.Model.Values;
using Microsoft.EntityFrameworkCore;

namespace DDD.Model.UnitTest
{
    public class HierarchyInMemoryTest : InMemoryBaseTest
    {
        private ShoppingCartDbContext _ctx ;

        [SetUp]
        public void Setup()
        {
            this._ctx = _serviceProvider.GetService(typeof(ShoppingCartDbContext)) as ShoppingCartDbContext;
        }

        [Test]
        public void test_create()
        {
            var root = new Hierarchy()
            {
                Title = "Root",
            };
            _ctx.HierarchySet.Add(root);
            _ctx.SaveChanges();

            var child = new Hierarchy()
            {
                Title = "A",
                Parent = root,
            };
            _ctx.HierarchySet.Add(child);
            _ctx.SaveChanges();

            var gChild = new Hierarchy()
            {
                Title = "A-A",
                Parent = child,
            };
            _ctx.HierarchySet.Add(gChild);
            _ctx.SaveChanges();

            var actural = _ctx.HierarchySet.SingleOrDefault(c => c.Id == root.Id);
            Assert.IsNotNull(actural);
            Assert.IsNull(actural.Parent);
            Assert.IsTrue(actural.Children.Count > 0 && actural.Children.Any(c => c.Id == child.Id));
        }
    }

    public class HierarchySqlServerTest : SqlServerBaseTest
    {
        private ShoppingCartDbContext _ctx;

        [SetUp]
        public void Setup()
        {
            this._ctx = _serviceProvider.GetService(typeof(ShoppingCartDbContext)) as ShoppingCartDbContext;
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
        }

        [Test]
        public void test_create()
        {
            var root = new Hierarchy()
            {
                Title = "Root",
            };
            _ctx.HierarchySet.Add(root);
            _ctx.SaveChanges();

            var child = new Hierarchy()
            {
                Title = "A",
                Parent = root,
            };
            _ctx.HierarchySet.Add(child);
            _ctx.SaveChanges();

            var gChild = new Hierarchy()
            {
                Title = "A-A",
                Parent = child,
            };
            _ctx.HierarchySet.Add(gChild);
            _ctx.SaveChanges();

            var actural = _ctx.HierarchySet.SingleOrDefault(c => c.Id == root.Id);
            Assert.IsNotNull(actural);
            Assert.IsNull(actural.Parent);
            Assert.IsTrue(actural.Children.Count > 0 && actural.Children.Any(c => c.Id == child.Id));

        }
    }

}