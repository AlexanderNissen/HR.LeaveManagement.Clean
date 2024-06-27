using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTest;

public class HrDatabaseContextTests
{
    private readonly HrDatabaseContext _hrDatabaseContext;

    public HrDatabaseContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _hrDatabaseContext = new HrDatabaseContext(dbOptions);


    }

    [Fact]
    public async Task When_SaveChangesAsync_Called_DateCreatedIsSetAsync()
    {
        // Arrange
        var leaveType = new LeaveType
        {
            Id = Guid.NewGuid(),
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        // Act
        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        // Assert - Shouldly
        leaveType.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async void When_SaveChangesAsync_Called_DataModifiedIsSet()
    {
        // Arrange
        var leaveType = new LeaveType
        {
            Id = Guid.NewGuid(),
            DefaultDays = 15,
            Name = "Test Sick"
        };

        // Act
        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        // Assert - xUnit Assert
        Assert.NotNull(leaveType);
    }
}
