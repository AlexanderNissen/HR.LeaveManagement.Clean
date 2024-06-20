using AutoMapper;
using HR.LeaveManagement.Application.Contracts;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequests;

public class GetLeaveRequestsQueryHandler : IRequestHandler<GetLeaveRequestsQuery, List<LeaveRequestDto>>
{
    private readonly ILeaveRequestRepository _repository;
    private readonly IMapper _mapper;

    public GetLeaveRequestsQueryHandler(ILeaveRequestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestsQuery query, CancellationToken cancellationToken)
    {
        // Check if employee is logged in

        var leaveRequests = await _repository.GetLeaveRequestsWithDetails();
        var dtos = _mapper.Map<List<LeaveRequestDto>>(leaveRequests);

        // Fill dtos with employee information

        return dtos;
    }
}
