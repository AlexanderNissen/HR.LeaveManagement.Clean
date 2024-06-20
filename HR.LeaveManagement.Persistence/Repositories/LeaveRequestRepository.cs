using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(HrDatabaseContext dbContext) : base(dbContext) { }

    public async Task<LeaveRequest> GetLeaveRequestWithDetails(Guid id)
    {
        var leaveRequest = await _dbContext.LeaveRequests
            .Include(q => q.LeaveType)
            .FirstOrDefaultAsync(q => q.Id == id);
        return leaveRequest;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
    var leaveRequests = await _dbContext.LeaveRequests
            .Include(q => q.LeaveType) // Includes the LeaveType that entity q references using a foreign key (equivalent to inner join)
            .ToListAsync();
        return leaveRequests;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(Guid userId)
    {
        var leaveRequests = await _dbContext.LeaveRequests
            .Where(q => q.RequestingEmployeeId == userId)
            .Include(q => q.LeaveType)
            .ToListAsync();
        return leaveRequests;
    }
}
