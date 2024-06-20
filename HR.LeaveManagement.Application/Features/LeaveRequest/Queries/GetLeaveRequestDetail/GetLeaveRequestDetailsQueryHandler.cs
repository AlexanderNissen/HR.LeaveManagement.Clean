using AutoMapper;
using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
{
    ILeaveRequestRepository _repository;
    IMapper _mapper;

    public GetLeaveRequestDetailsQueryHandler(ILeaveRequestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery query, CancellationToken cancellationToken)
    {
        var leaveRequest = await _repository.GetLeaveRequestsWithDetails(query.Id);
        if (leaveRequest == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), query.Id);
        }

        var dto = _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);

        // Add Employee details as needed

        return dto;
    }
}
