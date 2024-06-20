using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsQuery : IRequest<LeaveAllocationDetailsDto>
{
    public Guid Id { get; set; }
}
