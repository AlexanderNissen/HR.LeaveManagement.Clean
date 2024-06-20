using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _repository;

    public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteLeaveRequestCommand command, CancellationToken cancellationToken)
    {
        var leaveRequest = await _repository.GetByIdAsync(command.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(LeaveRequest), command.Id);
        }

        await _repository.DeleteAsync(leaveRequest);

        return Unit.Value;
    }
}
