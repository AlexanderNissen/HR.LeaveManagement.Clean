using AutoMapper;
using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
{
    private readonly ILeaveTypeRepository _typeRepository;
    private readonly ILeaveRequestRepository _requestRepository;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _appLogger;

    public CreateLeaveRequestCommandHandler(ILeaveTypeRepository typeRepository, ILeaveRequestRepository requestRepository, IMapper mapper, IEmailSender emailSender)
    {
        _typeRepository = typeRepository;
        _requestRepository = requestRepository;
        _mapper = mapper;
        _emailSender = emailSender;
    }

    public CreateLeaveRequestCommandHandler(
        ILeaveTypeRepository typeRepository,
        ILeaveRequestRepository requestRepository,
        IMapper mapper,
        IEmailSender emailSender,
        IAppLogger<CreateLeaveRequestCommandHandler> appLogger)
    {
        _typeRepository = typeRepository;
        _requestRepository = requestRepository;
        _mapper = mapper;
        _emailSender = emailSender;
        _appLogger = appLogger;
    }

    public async Task<Unit> Handle(CreateLeaveRequestCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestValidator(_typeRepository);
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid leave request", validationResult);
        }

        // Get requesting employee's ID
        // Check on employee's allocation
        // If allocations are insufficient, return validation error with message

        var leaveRequest = _mapper.Map<Domain.LeaveRequest>(command);
        await _requestRepository.CreateAsync(leaveRequest);

        // Send confirmation email
        var email = new EmailMessage
        {
            To = string.Empty, // Get email from employee record
            Body = $"Your leave request for {command.StartDate:D} to {command.EndDate} "
                   + "has been submitted successfully",
            Subject = "Leave Request Submitted"
        };

        await _emailSender.SendEmail(email);

        return Unit.Value;
    }
}
