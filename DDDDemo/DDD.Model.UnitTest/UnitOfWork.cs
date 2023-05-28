using DDD.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Model.UnitTest
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        //IRepository<Catalog, int> CatalogRepository { get; set; }
        //IRepository<Order, int> OrderRepository { get; set; }
        //IRepository<OrderItem, int> OrderItemRepository { get; set; }

        Task CommitAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {

        public readonly IRepository<Catalog, int> CatalogRepository;
        public readonly IRepository<Order, int> OrderRepository;

        public readonly IRepository<OrderItem, int> OrderItemRepository ;

        private readonly ShoppingCartDbContext _ctx;
        public UnitOfWork(
            ShoppingCartDbContext ctx,
            IRepository<Catalog, int> catalogRepo,
            IRepository<Order, int> orderRepo,
            IRepository<OrderItem, int> orderItemRepo)
        {
            this._ctx = ctx;
            this.CatalogRepository = catalogRepo;
            this.OrderRepository = orderRepo;
            this.OrderItemRepository = orderItemRepo;
        }

        public async Task CommitAsync() => await _ctx.SaveChangesAsync();

        private bool _disposed;

        public async ValueTask DisposeAsync()
        {
            if(!_disposed)
            {
                // Dispose managed resources.
                await _ctx.DisposeAsync();

                // Dispose any unmanaged resources here...
                _disposed = true;
            }
        }
    }
}
