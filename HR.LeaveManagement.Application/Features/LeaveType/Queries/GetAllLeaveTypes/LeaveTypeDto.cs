﻿namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class LeaveTypeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public int DefaultDays { get; set; }

}
