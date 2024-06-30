using AutoMapper;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.MappingProfiles;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        CreateMap<LeaveTypeDto, LeaveTypeViewModel>().ReverseMap();
        CreateMap<CreateLeaveTypeCommand, LeaveTypeViewModel>();
        CreateMap<UpdateLeaveTypeCommand, LeaveTypeViewModel>();
    }
}
