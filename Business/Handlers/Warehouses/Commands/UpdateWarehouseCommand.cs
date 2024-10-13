
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Warehouses.ValidationRules;


namespace Business.Handlers.Warehouses.Commands
{


    public class UpdateWarehouseCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int WareHouseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool IsReadyForSale { get; set; }

        public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, IResult>
        {
            private readonly IWarehouseRepository _warehouseRepository;
            private readonly IMediator _mediator;

            public UpdateWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IMediator mediator)
            {
                _warehouseRepository = warehouseRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateWarehouseValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
            {
                var isThereWarehouseRecord = await _warehouseRepository.GetAsync(u => u.ProductId == request.ProductId);


                isThereWarehouseRecord.CreatedDate = request.CreatedDate;
                isThereWarehouseRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereWarehouseRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereWarehouseRecord.Status = request.Status;
                isThereWarehouseRecord.IsDeleted = request.IsDeleted;
                isThereWarehouseRecord.WareHouseId = request.WareHouseId;
                isThereWarehouseRecord.ProductId = request.ProductId;
                isThereWarehouseRecord.Quantity = request.Quantity;
                isThereWarehouseRecord.IsReadyForSale = request.IsReadyForSale;


                _warehouseRepository.Update(isThereWarehouseRecord);
                await _warehouseRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
            // sen ekledin unutmaaa 
            public class UpdateWarehouseQuantityCommand : IRequest<IResult>
            {
                public int WareHouseId { get; set; }
                public int Quantity { get; set; }

                public class UpdateWarehouseQuantityCommandHandler : IRequestHandler<UpdateWarehouseQuantityCommand, IResult>
                {
                    private readonly IWarehouseRepository _warehouseRepository;

                    public UpdateWarehouseQuantityCommandHandler(IWarehouseRepository warehouseRepository)
                    {
                        _warehouseRepository = warehouseRepository;
                    }

                    [CacheRemoveAspect("Get")]
                    [LogAspect(typeof(FileLogger))]
                    [SecuredOperation(Priority = 1)]
                    public async Task<IResult> Handle(UpdateWarehouseQuantityCommand request, CancellationToken cancellationToken)
                    {
                        var warehouse = await _warehouseRepository.GetAsync(w => w.WareHouseId == request.WareHouseId);
                        if (warehouse == null)
                        {
                            return new ErrorResult("Warehouse not found");
                        }

                        warehouse.Quantity = request.Quantity;
                        await _warehouseRepository.SaveChangesAsync();

                        return new SuccessResult(Messages.Updated);
                    }
                }
            }

        }
    }
}

