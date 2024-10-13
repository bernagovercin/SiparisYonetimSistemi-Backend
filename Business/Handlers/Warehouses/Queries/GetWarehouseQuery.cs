
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Warehouses.Queries
{
    public class GetWarehouseQuery : IRequest<IDataResult<Warehouse>>
    {
        public int CreatedUserId { get; set; }

        public class GetWarehouseQueryHandler : IRequestHandler<GetWarehouseQuery, IDataResult<Warehouse>>
        {
            private readonly IWarehouseRepository _warehouseRepository;
            private readonly IMediator _mediator;

            public GetWarehouseQueryHandler(IWarehouseRepository warehouseRepository, IMediator mediator)
            {
                _warehouseRepository = warehouseRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Warehouse>> Handle(GetWarehouseQuery request, CancellationToken cancellationToken)
            {
                var warehouse = await _warehouseRepository.GetAsync(p => p.CreatedUserId == request.CreatedUserId && p.IsDeleted == false);
                return new SuccessDataResult<Warehouse>(warehouse);
            }
        }
    }
}
