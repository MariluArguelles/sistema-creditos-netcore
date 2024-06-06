using POS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POS.Domain.Entities;

public partial class User : BaseEntity
{
    public User()
    {
        UserRoles = new HashSet<UserRole>();
    }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Image { get; set; }

    public string? AuthType { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
