using System;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class EditCourseViewModel : ViewModelBase
{
    private readonly ICourseService _courseService;
    private readonly string _courseNumberToLoad = string.Empty;
    private string _objectId = string.Empty;
    private string _courseId = string.Empty;

    private string _courseNumber = string.Empty;
    public string CourseNumber
    {
        get => _courseNumber;
        set => SetProperty(ref _courseNumber, value);
    }

    private string _title = string.Empty;
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    private string _startDate = string.Empty;
    public string StartDate
    {
        get => _startDate;
        set => SetProperty(ref _startDate, value);
    }

    private string _endDate = string.Empty;
    public string EndDate
    {
        get => _endDate;
        set => SetProperty(ref _endDate, value);
    }

    private int _length;
    public int Length
    {
        get => _length;
        set => SetProperty(ref _length, value);
    }

    private string _saveMessage = string.Empty;
    public string SaveMessage
    {
        get => _saveMessage;
        private set => SetProperty(ref _saveMessage, value);
    }

    public EditCourseViewModel(ICourseService courseService, string courseNumber)
    {
        _courseService = courseService;
        PageTitle = "Redigera kurs: " + courseNumber;
        _courseNumberToLoad = courseNumber;
        Init();
    }

    private async void Init()
    {
        try
        {
            await GetCourse();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
        }
    }

    private async Task GetCourse()
    {
        var course = await _courseService.Find(_courseNumberToLoad);

        if (string.IsNullOrWhiteSpace(course._id))
        {
            throw new InvalidOperationException("RestDB returned a course without an ID.");
        }

        _objectId = course._id;
        _courseId = course.Id;

        CourseNumber = course.CourseNumber;
        Title = course.Title;
        StartDate = course.StartDate;
        EndDate = course.EndDate;
        Length = course.Length;

    }

    [RelayCommand]
    private async Task Save()
    {
        SaveMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(_objectId))
        {
            return;
        }

        try
        {
            Course course = new()
            {
                Id = _courseId,
                CourseNumber = CourseNumber,
                Title = Title,
                StartDate = StartDate,
                EndDate = EndDate,
                Length = Length
            };

            await _courseService.Update(course, _objectId);
            SaveMessage = "Kursen har sparats.";
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
        }
    }
}
