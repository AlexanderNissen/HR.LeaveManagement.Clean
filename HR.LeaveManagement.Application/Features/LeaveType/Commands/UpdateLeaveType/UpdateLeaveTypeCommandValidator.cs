using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    ILeaveTypeRepository _repository;

    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository repository)
    {
        _repository = repository;

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveTypeMustExist);

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

        RuleFor(p => p.DefaultDays)
            .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
            .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");

        RuleFor(q => q)
            .MustAsync(LeaveTypeUniqueName)
            .WithMessage("Leave type already exists");
    }

    public async Task<bool> LeaveTypeMustExist(Guid id, CancellationToken cToken)
    {
        return await _repository.GetByIdAsync(id) != null;
    }
    
    public async Task<bool> LeaveTypeUniqueName(UpdateLeaveTypeCommand command, CancellationToken cToken)
    {
        return await _repository.IsLeaveTypeUnique(command.Name);
    }
}
