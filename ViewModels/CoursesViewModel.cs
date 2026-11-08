using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class CoursesViewModel : ViewModelBase
{
    private readonly ICourseService _courseService;

    [ObservableProperty]
    private bool _showDialog;

    [ObservableProperty]
    private ObservableCollection<Course> _courses = [];

    public CoursesViewModel(ICourseService courseService)
    {
        _courseService = courseService;
        PageTitle = "Våra Kurser";

        _ = LoadCourses();
    }

    [RelayCommand]
    private async Task Delete(string courseNumber)
    {
        try
        {
            await _courseService.Delete(courseNumber);
            await LoadCourses();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
        }
    }

    [RelayCommand]
    private void Confirm()
    {
        Console.WriteLine("Tar bort kursen");
        ShowDialog = false;
    }

    [RelayCommand]
    private void Cancel()
    {
        Console.WriteLine("Borttagningen avbröts.");
        ShowDialog = false;
    }

    private async Task LoadCourses()
    {
        try
        {
            Courses = new ObservableCollection<Course>(await _courseService.List());
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
        }
    }
}
