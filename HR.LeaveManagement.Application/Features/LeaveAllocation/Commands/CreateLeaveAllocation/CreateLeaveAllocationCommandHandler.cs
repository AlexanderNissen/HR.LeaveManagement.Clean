using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _typeRepository;
    private readonly ILeaveAllocationRepository _allocationRepository;

    public CreateLeaveAllocationCommandHandler(
        IMapper mapper,
        ILeaveTypeRepository typeRepository,
        ILeaveAllocationRepository allocationRepository
        )
    {
        _mapper = mapper;
        _typeRepository = typeRepository;
        _allocationRepository = allocationRepository;
    }

    public async Task<Unit> Handle(CreateLeaveAllocationCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationValidator(_typeRepository);
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.Errors.Any()) 
            throw new BadRequestException("Invalid leave allocation request", validationResult);

        var id = command.LeaveTypeId;
        var leaveType = await _typeRepository.GetByIdAsync(id);

        // Get employees

        // Get period

        // Assign allocations
        var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(command);
        await _allocationRepository.CreateAsync(leaveAllocation);
        return Unit.Value;
    }
}
