using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
     public LeaveAllocationRepository(HrDatabaseContext dbContext) : base(dbContext) { }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await _dbContext.AddRangeAsync(allocations);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> AllocationExists(Guid userId, Guid leaveTypeId, int period)
    {
        var allocationExists = await _dbContext.LeaveAllocations
            .AnyAsync(
                q => q.EmployeeId == userId &&
                q.LeaveTypeId == leaveTypeId &&
                q.Period == period
            );
        return allocationExists;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        var leaveAllocations = await _dbContext.LeaveAllocations
            .Include(q => q.LeaveType)
            .ToListAsync();
        return leaveAllocations;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(Guid userId)
    {
        var leaveAllocations = await _dbContext.LeaveAllocations
            .Where(q => q.EmployeeId == userId)
            .Include(q => q.LeaveType)
            .ToListAsync();
        return leaveAllocations;
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(Guid id)
    {
        var leaveAllocation = await _dbContext.LeaveAllocations
            .Include(q => q.LeaveType)
            .FirstOrDefaultAsync(q => q.Id == id);
        return leaveAllocation;
    }

    public async Task<LeaveAllocation> GetUserAllocations(Guid userId, Guid leaveTypeId)
    {
        var leaveAllocation = await _dbContext.LeaveAllocations
            .FirstOrDefaultAsync(
                q => q.EmployeeId == userId &&
                q.LeaveTypeId == leaveTypeId
            );
        return leaveAllocation;
    }
}
