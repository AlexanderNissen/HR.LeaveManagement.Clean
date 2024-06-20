using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class GetLeaveRequestDetailsQuery : IRequest<LeaveRequestDetailsDto>
{
    public Guid Id { get; set; }
}
