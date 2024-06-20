using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<Guid> Handle(CreateLeaveTypeCommand command, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(command);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid leaveType", validationResult);
        }

        // Convert to domain entity object
        Domain.LeaveType leaveType = _mapper.Map<Domain.LeaveType>(command);

        // Add to database
        /* 
         * EF Core automatically assigns the Id property to the object referenced by variable leaveType.
         * We don't have to do any assignment
        */
        await _leaveTypeRepository.CreateAsync(leaveType);

        // Return record id
        return leaveType.Id;
    }
}
