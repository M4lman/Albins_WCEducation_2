using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Interfaces;

public interface ITeacherService
{
    Task<List<TeacherRestDb>> List();
    Task<TeacherRestDb> Find(string objectId);
    Task Add(Teacher teacher);
    Task Update(Teacher teacher, string objectId);
}
