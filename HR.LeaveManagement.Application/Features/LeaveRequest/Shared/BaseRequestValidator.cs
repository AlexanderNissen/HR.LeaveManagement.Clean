using FluentValidation;
using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Shared;

public class BaseRequestValidator : AbstractValidator<BaseLeaveRequest>
{
    private readonly ILeaveTypeRepository _repository;

    public BaseRequestValidator(ILeaveTypeRepository repository)
    {
        _repository = repository;

        RuleFor(p => p.StartDate)
            .LessThan(p => p.EndDate)
            .WithMessage("{PropertyName} must be before {ComparisonValue}");

        RuleFor(p => p.EndDate)
            .GreaterThan(p => p.StartDate)
            .WithMessage("{PropertyName} must be after {ComparisonValue}");

        RuleFor(p => p.LeaveTypeId)
            .MustAsync(LeaveTypeExists)
            .WithMessage("{PropertyName} does not exist");
    }

    private async Task<bool> LeaveTypeExists(Guid id, CancellationToken cancellationToken)
    {
        var leaveType = await _repository.GetByIdAsync(id);

        return leaveType is not null;
    }
}
