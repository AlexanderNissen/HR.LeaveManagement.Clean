using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    public LeaveTypeRepository(HrDatabaseContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsLeaveTypeUnique(string name)
    {
        return await _dbContext.LeaveTypes.AnyAsync(x => x.Name == name) == false;
    }
}