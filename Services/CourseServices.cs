using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;

namespace Client.Services;

public class CourseServices : ICourseService
{
    private readonly IHttpService<Course> _courses;
    private readonly IHttpService<CourseRestDb> _coursesFromRestDb;

    public CourseServices(
        IHttpService<Course> courses,
        IHttpService<CourseRestDb> coursesFromRestDb)
    {
        _courses = courses;
        _coursesFromRestDb = coursesFromRestDb;
    }

    public async Task Add(Course course)
    {
        await _courses.Post("courses", course);
    }

    public async Task Delete(string courseNumber)
    {
        var course = await Find(courseNumber);
        await _courses.Delete("courses", course._id);
    }

    public async Task<CourseRestDb> Find(string courseNumber)
    {
        var courses = await _coursesFromRestDb.Get("courses");
        var matches = courses
            .Where(course => string.Equals(
                course.CourseNumber.Trim(),
                courseNumber.Trim(),
                StringComparison.OrdinalIgnoreCase))
            .ToList();

        return matches.Count switch
        {
            1 => matches[0],
            0 => throw new InvalidOperationException($"Kursen med kursnummer '{courseNumber}' hittades inte."),
            _ => throw new InvalidOperationException($"Flera kurser har kursnummer '{courseNumber}'.")
        };
    }

    public async Task<List<Course>> List()
    {
        return await _courses.Get("courses");
    }

    public async Task Update(Course course, string objectId)
    {
        await _courses.Put("courses", course, objectId);
    }
}
