using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _requestRepository;
    private readonly IEmailSender _emailSender;

    public CancelLeaveRequestCommandHandler(ILeaveRequestRepository requestRepository, IEmailSender emailSender)
    {
        _requestRepository = requestRepository;
        _emailSender = emailSender;
    }

    public async Task<Unit> Handle(CancelLeaveRequestCommand command, CancellationToken cancellationToken)
    {
        var leaveRequest = await _requestRepository.GetByIdAsync(command.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(leaveRequest), command.Id);
        }

        leaveRequest.Cancelled = true;

        // If already approved, re-evaluate the employee's allocations for the leave type

        // Send confirmation mail
        var email = new EmailMessage
        {
            To = string.Empty,
            Body = $"Your leave request for {leaveRequest.StartDate:D} to" +
            $"{leaveRequest.EndDate:D} has been cancelled successfully.",
            Subject = "Leave request cancelled"
        };

        await _emailSender.SendEmail(email);

        return Unit.Value;
    }
}