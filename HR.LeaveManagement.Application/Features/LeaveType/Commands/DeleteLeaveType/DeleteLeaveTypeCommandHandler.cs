using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        
        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<Unit> Handle(DeleteLeaveTypeCommand command, CancellationToken cancellationToken) 
        {
            // Validation

            // Retrieve domain entity object from database
            Domain.LeaveType leaveType = await _leaveTypeRepository.GetByIdAsync(command.Id);

            // Verify the record exists
            if (leaveType is null)
                throw new NotFoundException(nameof(leaveType), command.Id);

            // Remove from the database
            await _leaveTypeRepository.DeleteAsync(leaveType);

            return Unit.Value;
        }
    }

}
