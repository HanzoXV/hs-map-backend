using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hs_map_backend.Models;

[Table("users")]
[Index(nameof(Email), Name = "users_email_key", IsUnique = true)]
public class User
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("email")]
    [StringLength(256)]
    public string Email { get; set; } = null!;

    [Column("display_name")]
    [StringLength(100)]
    public string? DisplayName { get; set; }

    [Column("preferred_language")]
    [StringLength(10)]
    public string? PreferredLanguage { get; set; } = "URDU";

    [Column("password_hash")]
    [StringLength(500)]
    public string PasswordHash { get; set; } = null!;

    [Column("security_stamp")]
    [StringLength(100)]
    public string? SecurityStamp { get; set; } = Guid.NewGuid().ToString();
}