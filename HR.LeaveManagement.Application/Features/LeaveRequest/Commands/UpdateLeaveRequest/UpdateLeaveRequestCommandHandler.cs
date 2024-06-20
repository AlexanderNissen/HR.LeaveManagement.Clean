using AutoMapper;
using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly ILeaveTypeRepository _typeRepository;
    private readonly ILeaveRequestRepository _requestRepository;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _appLogger;

    public UpdateLeaveRequestCommandHandler(
        ILeaveTypeRepository typeRepository,
        ILeaveRequestRepository requestRepository,
        IMapper mapper,
        IEmailSender emailSender,
        IAppLogger<UpdateLeaveRequestCommandHandler> appLogger)
    {
        _typeRepository = typeRepository;
        _requestRepository = requestRepository;
        _mapper = mapper;
        _emailSender = emailSender;
        _appLogger = appLogger;
    }

    public async Task<Unit> Handle(UpdateLeaveRequestCommand command, CancellationToken cancellationToken)
    {
        var leaveRequest = await _requestRepository.GetByIdAsync(command.Id);

        var validator = new UpdateLeaveRequestValidator(_typeRepository, _requestRepository);
        var validationResult = await validator.ValidateAsync(command);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid leave request", validationResult);
        }

        _mapper.Map(command, leaveRequest);

        await _requestRepository.UpdateAsync(leaveRequest);

        // Send confirmation email
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, // Get email from employee record
                Body = $"Your leave request for {command.StartDate:D} to {command.EndDate:D}"
                + "has been updated successfully",
                Subject = "Leave Request Submitted"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _appLogger.LogWarning(ex.Message);
        }


        return Unit.Value;
    }
}
