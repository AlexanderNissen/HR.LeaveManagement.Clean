using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "cac43a6e-f7bb-4448-1add431ccbbf",
                UserId = "b005b939-cabd-472f-be81-b68e516eb83c"
            },
            new IdentityUserRole<string>
            {
                RoleId = "cvc43a8e-4445-baaf-1add431ffbbf",
                UserId = "b0c4f59a-c29d-497f-8f30-674bd95ad946"
            }
        );
    }
}
