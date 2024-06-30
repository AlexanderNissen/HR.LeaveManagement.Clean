using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Contracts;

public interface ILeaveTypeService
{
    Task<List<LeaveTypeViewModel>> GetLeaveTypes();
    Task<LeaveTypeViewModel> GetLeaveTypeDetails(Guid id);
    Task<Response<Guid>> CreateLeaveType(LeaveTypeViewModel leaveType);
    Task<Response<Guid>> UpdateLeaveType(Guid id, LeaveTypeViewModel leaveType);
    Task<Response<Guid>> DeleteLeaveType(Guid id);
}