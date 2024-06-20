using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _typeRepository;
    private readonly ILeaveAllocationRepository _allocationRepository;

    public UpdateLeaveAllocationValidator(ILeaveTypeRepository typeRepository, ILeaveAllocationRepository allocationRepository)
    {
        _typeRepository = typeRepository;
        _allocationRepository = allocationRepository;

        RuleFor(p => p.NumberOfDays)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must greater than {ComparisonValue}");

        RuleFor(p => p.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year)
            .WithMessage("{PropertyName} must be after {ComparisonValue}");

        RuleFor(p => p.LeaveTypeId)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist");

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveAllocationMustExist)
            .WithMessage("{PropertyName} must be present");
    }

    private async Task<bool> LeaveTypeMustExist(Guid id, CancellationToken cancellationToken)
    {
        var leaveType = await _typeRepository.GetByIdAsync(id);
        return leaveType is null;
    }

    private async Task<bool> LeaveAllocationMustExist(Guid id, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _allocationRepository.GetByIdAsync(id);
        return leaveAllocation is null;
    }
}
