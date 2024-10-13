
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Warehouses.ValidationRules;

namespace Business.Handlers.Warehouses.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateWarehouseCommand : IRequest<IResult>
    {

        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int WareHouseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool IsReadyForSale { get; set; }


        public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, IResult>
        {
            private readonly IWarehouseRepository _warehouseRepository;
            private readonly IMediator _mediator;
            public CreateWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IMediator mediator)
            {
                _warehouseRepository = warehouseRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateWarehouseValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
            {
                var isThereWarehouseRecord = _warehouseRepository.Query().Any(u => u.ProductId == request.ProductId);

                if (isThereWarehouseRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedWarehouse = new Warehouse
                {
                    CreatedDate = request.CreatedDate,
                    LastUpdatedUserId = request.LastUpdatedUserId,
                    LastUpdatedDate = request.LastUpdatedDate,
                    Status = request.Status,
                    IsDeleted = request.IsDeleted,
                    WareHouseId = request.WareHouseId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    IsReadyForSale = request.IsReadyForSale,

                };

                _warehouseRepository.Add(addedWarehouse);
                await _warehouseRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}