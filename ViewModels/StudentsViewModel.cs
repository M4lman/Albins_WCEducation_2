using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;

namespace Client.ViewModels;

public class StudentsViewModel : ViewModelBase
{
    private readonly IStudentService _studentService;

    private ObservableCollection<Student> _students = [];
    public ObservableCollection<Student> Students
    {
        get => _students;
        private set => SetProperty(ref _students, value);
    }

    public StudentsViewModel(IStudentService studentService)
    {
        _studentService = studentService;
        PageTitle = "Elever";
        _ = LoadStudents();
    }

    private async Task LoadStudents()
    {
        try
        {
            var students = await _studentService.List();
            Students = new ObservableCollection<Student>(students);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
        }
    }

}
