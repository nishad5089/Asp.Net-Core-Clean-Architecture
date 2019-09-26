using System;
using Domain.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Entity
{
    // Add profile data for application users by adding properties to this class
    public class ApplicationUser : IdentityUser
    {
        // Extended Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
