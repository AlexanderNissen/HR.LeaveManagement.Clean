using FluentValidation;
using HR.LeaveManagement.Application.Contracts;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandValidator : AbstractValidator<ChangeLeaveRequestApprovalCommand>
{
    public ChangeLeaveRequestApprovalCommandValidator()
    {
        RuleFor(p => p.Approved)
            .NotNull()
            .WithMessage("Approval status must not be null");
    }
}
