using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsHandler : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
{
    private readonly ILeaveAllocationRepository _repository;
    private readonly IMapper _mapper;
    public GetLeaveAllocationDetailsHandler(ILeaveAllocationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery query, CancellationToken cancellationToken)
    {
        var id = query.Id;
        var leaveAllocationDetails = await _repository.GetLeaveAllocationWithDetails(id);
        var dto = _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocationDetails);

        return dto;
    }
}
