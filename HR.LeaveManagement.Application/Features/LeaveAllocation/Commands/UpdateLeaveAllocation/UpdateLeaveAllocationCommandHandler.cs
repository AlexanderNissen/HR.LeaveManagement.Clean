using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _typeRepository;
    private readonly ILeaveAllocationRepository _allocationRepository;

    public UpdateLeaveAllocationCommandHandler(IMapper mapper, ILeaveTypeRepository typeRepository, ILeaveAllocationRepository allocationRepository)
    {
        _mapper = mapper;
        _typeRepository = typeRepository;
        _allocationRepository = allocationRepository;
    }

    public async Task<Unit> Handle(UpdateLeaveAllocationCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationValidator(_typeRepository, _allocationRepository);
        var validationResult = await validator.ValidateAsync(command);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid leave allocation request", validationResult);

        var leaveAllocation = await _allocationRepository.GetByIdAsync(command.Id);
        // Following check is more or less redundant because validator checks presence of LeaveAllocation in DB
        if (leaveAllocation is null)
            throw new NotFoundException(nameof(LeaveAllocation), command.Id);

        // Inserts fields in command into instance of leaveAllocation
        _mapper.Map(command, leaveAllocation);

        await _allocationRepository.UpdateAsync(leaveAllocation);

        return Unit.Value;
    }
}
