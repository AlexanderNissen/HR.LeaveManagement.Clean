using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationValidator : AbstractValidator<CreateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _repository;

    public CreateLeaveAllocationValidator(ILeaveTypeRepository repository)
    {
        _repository = repository;

        RuleFor(p => p.LeaveTypeId)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist.");
    }

    private async Task<bool> LeaveTypeMustExist(Guid guid, CancellationToken token)
    {
        var leaveType = await _repository.GetByIdAsync(guid);
        return leaveType is not null;
    }
}
