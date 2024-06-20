using AutoMapper;
using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    private readonly ILeaveRequestRepository _requestRepository;
    private readonly ILeaveTypeRepository _typeRepository;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;

    public ChangeLeaveRequestApprovalCommandHandler(
        ILeaveRequestRepository requestRepository,
        ILeaveTypeRepository typeRepository,
        IMapper mapper,
        IEmailSender emailSender)
    {
        _requestRepository = requestRepository;
        _typeRepository = typeRepository;
        _mapper = mapper;
        _emailSender = emailSender;
    }

    public async Task<Unit> Handle(
        ChangeLeaveRequestApprovalCommand command, CancellationToken cancellationToken)
    {
        var validator = new ChangeLeaveRequestApprovalCommandValidator();
        var validationResult = await validator.ValidateAsync(command);

        if (validationResult.Errors.Any())
            throw new BadRequestException(nameof(command), validationResult);
        
        var leaveRequest = await _requestRepository.GetByIdAsync(command.Id);

        if (leaveRequest is null)
            throw new NotFoundException(nameof(command), validationResult);

        leaveRequest.Approved = command.Approved;
        await _requestRepository.UpdateAsync(leaveRequest);

        // If the request is approved, get and update the employee's allocations

        // Send confirmation email
        var email = new EmailMessage
        {
            To = string.Empty, // Get email from employee record
            Body = $"The approval status for your leave request from {leaveRequest.StartDate:D}" +
            $"to {leaveRequest.EndDate:D} has been updated.",
            Subject = "Leave Request Approval Status Updated"
        };

        await _emailSender.SendEmail(email);

        return Unit.Value;
    }
}
