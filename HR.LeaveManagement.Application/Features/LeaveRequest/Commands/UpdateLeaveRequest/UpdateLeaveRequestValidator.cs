using FluentValidation;
using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using System.ComponentModel;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestValidator : AbstractValidator<UpdateLeaveRequestCommand>
{
    private readonly ILeaveTypeRepository _typeRepository;
    private readonly ILeaveRequestRepository _requestRepository;

    public UpdateLeaveRequestValidator(ILeaveTypeRepository typeRepository, ILeaveRequestRepository requestRepository)
    {
        _typeRepository = typeRepository;
        _requestRepository = requestRepository;

        Include(new BaseRequestValidator(_typeRepository));

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveRequestExists)
            .WithMessage("{PropertyName} must exist");
    }

    private async Task<bool> LeaveRequestExists(Guid id, CancellationToken cancellationToken)
    {
        var leaveRequest = await _requestRepository.GetByIdAsync(id);

        return leaveRequest is not null;
    }
}
