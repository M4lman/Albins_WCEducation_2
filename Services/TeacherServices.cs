using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;

namespace Client.Services;

public class TeacherServices : ITeacherService
{
    private readonly IHttpService<TeacherRestDb> _teachersFromRestDb;
    private readonly IHttpService<Teacher> _teachers;

    public TeacherServices(
        IHttpService<TeacherRestDb> teachersFromRestDb,
        IHttpService<Teacher> teachers)
    {
        _teachersFromRestDb = teachersFromRestDb;
        _teachers = teachers;
    }

    public async Task<List<TeacherRestDb>> List()
    {
        return await _teachersFromRestDb.Get("teachers");
    }

    public async Task<TeacherRestDb> Find(string objectId)
    {
        var teachers = await List();
        return teachers.FirstOrDefault(teacher =>
                   string.Equals(teacher._id, objectId, StringComparison.Ordinal))
               ?? throw new InvalidOperationException($"Läraren med ID '{objectId}' hittades inte.");
    }

    public async Task Add(Teacher teacher)
    {
        await _teachers.Post("teachers", teacher);
    }

    public async Task Update(Teacher teacher, string objectId)
    {
        await _teachers.Put("teachers", teacher, objectId);
    }
}
