
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Customers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteCustomerCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public int CustomerId { get; set; }

        public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, IResult>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IMediator mediator)
            {
                _customerRepository = customerRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
            {
                var customerToDelete = _customerRepository.Get(p => p.CreatedUserId == request.CreatedUserId && p.CustomerId == request.CustomerId);


                //burayı ekledin 
                if (customerToDelete == null)
                {
                    return new ErrorResult("Müşteri bulunamadı.");
                }



                customerToDelete.IsDeleted = true;

                _customerRepository.Update(customerToDelete); // delete sildik update yaptık
                await _customerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

