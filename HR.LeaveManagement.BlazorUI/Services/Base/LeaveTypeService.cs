using HR.LeaveManagement.BlazorUI.Contracts;

namespace HR.LeaveManagement.BlazorUI.Services.Base;

public class LeaveTypeService : BaseHttpService, ILeaveTypeService
{
    public LeaveTypeService(IClient client) : base(client)
    {}
}
