using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Interfaces;

public interface ICourseService
{
    Task Add(Course course);
    Task Delete(string courseNumber);
    Task<CourseRestDb> Find(string courseNumber);
    Task<List<Course>> List();
    Task Update(Course course, string objectId);
}
