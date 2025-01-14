﻿using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class LeaveRequestDetailsDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid RequestingEmployeeId { get; set; }
    public LeaveTypeDto LeaveType { get; set; }
    public Guid LeaveTypeId { get; set; }
    public DateTime DateRequested { get; set; }
    public string RequestComments { get; set; }
    public DateTime? DateActioned { get; set; }
    bool? IsApproved { get; set; }
    public bool Cancelled { get; set; }
}
