using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommand : IRequest<Unit>
{

    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public int DefaultDays { get; set; }

    public UpdateLeaveTypeCommand(Guid id, string name, int defaultDays)
    {
        Id = id;
        Name = name;
        DefaultDays = defaultDays;
    }
}