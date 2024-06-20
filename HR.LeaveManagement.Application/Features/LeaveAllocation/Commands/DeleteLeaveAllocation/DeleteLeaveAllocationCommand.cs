using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
