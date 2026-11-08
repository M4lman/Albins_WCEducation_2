using System;

namespace Client.Models;

public record Teacher
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Expertise { get; set; }
}
