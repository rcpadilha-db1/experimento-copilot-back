using AutoMapper;
using Experimento.Application.Services.Interfaces;
using Experimento.Domain.Interfaces;
using Experimento.Domain.Notification;
using MediatR;

namespace Experimento.Application.UseCases.DeleteRideById;

public class DeleteRideByIdHandler : IRequestHandler<DeleteRideByIdCommand, DeleteRideByIdResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICheckIfRideExistsService _checkIfRideExistsService;
    private readonly IRideRepository _rideRepository;
    private readonly NotificationContext _notificationContext;

    public DeleteRideByIdHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICheckIfRideExistsService checkIfRideExistsService,
        IRideRepository rideRepository,
        NotificationContext notificationContext)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _checkIfRideExistsService = checkIfRideExistsService;
        _rideRepository = rideRepository;
        _notificationContext = notificationContext;
    }

    public async Task<DeleteRideByIdResult> Handle(DeleteRideByIdCommand request, CancellationToken cancellationToken)
    {
        var existentRide = await _checkIfRideExistsService.Check(request.RideId, cancellationToken);
        
        var deleteRideByIdResult = new DeleteRideByIdResult();
        if (_notificationContext.HasNotifications() || existentRide == null)
        {
            return deleteRideByIdResult;
        }
    
        await _rideRepository.DeleteRide(existentRide, cancellationToken);

        return _mapper.Map<DeleteRideByIdResult>(existentRide);
    }
}