using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger;

    public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<UpdateLeaveTypeCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateLeaveTypeCommand command, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (validationResult.Errors.Any()) 
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}",
                nameof(LeaveType), command.Id);
            throw new BadRequestException($"Invalid leave type", validationResult);
        }

        // Convert to domain entity
        Domain.LeaveType leaveType = _mapper.Map<Domain.LeaveType>(command);

        // Add to database
        await _leaveTypeRepository.UpdateAsync(leaveType);

        // Return Unit value
        return Unit.Value;
    }
}
