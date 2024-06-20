using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    ILeaveAllocationRepository _repository;
    IMapper _mapper;

    public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteLeaveAllocationCommand command, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _repository.GetByIdAsync(command.Id);

        if (leaveAllocation is null)
            throw new NotFoundException(nameof(LeaveAllocation), command.Id);

        await _repository.DeleteAsync(leaveAllocation);
        return Unit.Value;
    }
}
