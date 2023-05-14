using Core.Entities;
using Shared.Interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        public IRepository<Catalog, int> CatalogRepository { get; set; }
        public IRepository<Order, int> OrderRepository { get; set; }
        public IRepository<OrderItem, int> OrderItemRepository { get; set; }
        public IRepository<HierarchyItem, int> HierarchyItemRepository { get; set; }

        private readonly AppDbContext _ctx;
        public UnitOfWork(
            AppDbContext ctx,
            IRepository<Catalog, int> catalogRepo,
            IRepository<Order, int> orderRepo,
            IRepository<OrderItem, int> orderItemRepo
            )
        {
            this._ctx = ctx;
            this.CatalogRepository = catalogRepo;
            this.OrderRepository = orderRepo;
            this.OrderItemRepository = orderItemRepo;
        }

        public async Task CommitChangesAsync() => await _ctx.SaveChangesAsync();

        private bool _disposed;

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                // Dispose managed resources.
                await _ctx.DisposeAsync();

                // Dispose any unmanaged resources here...
                _disposed = true;
            }
        }
    }

    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<Catalog, int> CatalogRepository { get; set; }
        IRepository<Order, int> OrderRepository { get; set; }
        IRepository<OrderItem, int> OrderItemRepository { get; set; }
        IRepository<HierarchyItem, int> HierarchyItemRepository { get; set; }

        Task CommitChangesAsync();
    }
}
