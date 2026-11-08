using System;

namespace Client.Models;

public record Course
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string CourseNumber { get; set; } = "";
    public required string Title { get; set; } ="";
    public int Length { get; set; }
    public required string StartDate { get; set; }
    public required string EndDate { get; set; }
}
