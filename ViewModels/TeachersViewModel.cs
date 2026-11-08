using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;

namespace Client.ViewModels;

public class TeachersViewModel : ViewModelBase
{
    private readonly ITeacherService _teacherService;

    private ObservableCollection<TeacherRestDb> _teachers = [];
    public ObservableCollection<TeacherRestDb> Teachers
    {
        get => _teachers;
        private set => SetProperty(ref _teachers, value);
    }

    public TeachersViewModel(ITeacherService teacherService)
    {
        _teacherService = teacherService;
        PageTitle = "Lärare";
        _ = LoadTeachers();
    }

    private async Task LoadTeachers()
    {
        try
        {
            Teachers = new ObservableCollection<TeacherRestDb>(await _teacherService.List());
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
        }
    }

}
