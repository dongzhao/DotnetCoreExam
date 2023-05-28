using Core.Entities;
using Shared.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Catalog, int> CatalogRepository { get; set; }
        public IRepository<Order, int> OrderRepository { get; set; }
        public IRepository<OrderItem, int> OrderItemRepository { get; set; }
        public IRepository<HierarchyItem, int> HierarchyItemRepository { get; set; }

        private readonly AppDbContext _ctx;

        private readonly IMediator _mediator;

        private readonly IDomainEventDispatcher _dispatcher;

        public UnitOfWork(
            AppDbContext ctx,
            IMediator mediator,
            IDomainEventDispatcher dispatcher,
            IRepository<Catalog, int> catalogRepo,
            IRepository<Order, int> orderRepo,
            IRepository<OrderItem, int> orderItemRepo
            )
        {
            this._ctx = ctx;
            this._mediator = mediator;
            this._dispatcher = dispatcher;
            this.CatalogRepository = catalogRepo;
            this.OrderRepository = orderRepo;
            this.OrderItemRepository = orderItemRepo;
        }

        public async Task CommitChangesAsync(CancellationToken cancellationToken = default(CancellationToken)) => await _ctx.SaveChangesAsync(cancellationToken);

        public async Task CommitAndNotifyAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //await _mediator.Send();
            await _dispatchDomainEvents(); // 
            await CommitChangesAsync(cancellationToken);
        }

        private async Task _dispatchDomainEvents()
        {
            var domainEventEntities = _ctx.ChangeTracker.Entries<IEntity<int>>()
                .Select(c => c.Entity)
                .Where(c => c.DomainEvents.Any())
                .ToArray();

            foreach (var entity in domainEventEntities)
            {
                IDomainEvent @event;
                while (entity.DomainEvents.TryTake(out @event))
                    await _dispatcher.Dispatch(@event);
            }
        }

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

        Task CommitChangesAsync(CancellationToken cancellationToken);

        Task CommitAndNotifyAsync(CancellationToken cancellationToken);
    }
}
