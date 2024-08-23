using AutoMapper;
using Experimento.Application.Services.Interfaces;
using Experimento.Application.UseCases.CreateRide;
using Experimento.Domain.Entities;
using Experimento.Domain.Interfaces;
using Experimento.Domain.Notification;
using MediatR;

namespace Experimento.Application.UseCases.CreateRideByUserId;

public class CreateRideHandler : IRequestHandler<CreateRideCommand, CreateRideResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICheckRideRequirementsService _checkRideRequirementsService;
    private readonly IRideRepository _rideRepository;
    private readonly NotificationContext _notificationContext;

    public CreateRideHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICheckRideRequirementsService checkRideRequirementsService,
        IRideRepository rideRepository,
        NotificationContext notificationContext)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _checkRideRequirementsService = checkRideRequirementsService;
        _rideRepository = rideRepository;
        _notificationContext = notificationContext;
    }

    public async Task<CreateRideResult> Handle(CreateRideCommand request, CancellationToken cancellationToken)
    {
        await _checkRideRequirementsService.CheckIfAreRequirementsToRide(request.Id,request.RiderId, request.VehicleId, cancellationToken);

        if (_notificationContext.HasNotifications())
        {
            return null; 
        }
        
        var ride = _mapper.Map<Ride>(request);
        await _rideRepository.CreateRideAsync(ride, cancellationToken);
        
        await _unitOfWork.Commit(cancellationToken);
        
        return _mapper.Map<CreateRideResult>(ride);
    }
}