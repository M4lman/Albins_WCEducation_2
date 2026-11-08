using System;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class AddCourseViewModel : ViewModelBase
{
    private readonly ICourseService _courseService;

    private string _courseNumberInput = string.Empty;
    public string CourseNumberInput
    {
        get => _courseNumberInput;
        set => SetProperty(ref _courseNumberInput, value);
    }

    private string _titleInput = string.Empty;
    public string TitleInput
    {
        get => _titleInput;
        set => SetProperty(ref _titleInput, value);
    }

    private string _startDateInput = string.Empty;
    public string StartDateInput
    {
        get => _startDateInput;
        set => SetProperty(ref _startDateInput, value);
    }

    private string _endDateInput = string.Empty;
    public string EndDateInput
    {
        get => _endDateInput;
        set => SetProperty(ref _endDateInput, value);
    }

    private int _lengthInput;
    public int LengthInput
    {
        get => _lengthInput;
        set => SetProperty(ref _lengthInput, value);
    }

    private string _saveMessage = string.Empty;
    public string SaveMessage
    {
        get => _saveMessage;
        private set => SetProperty(ref _saveMessage, value);
    }

    public AddCourseViewModel(ICourseService courseService)
    {
        _courseService = courseService;
        PageTitle = "Ny Kurs";
    }

    [RelayCommand]
    private async Task Save()
    {
        SaveMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(CourseNumberInput) ||
            string.IsNullOrWhiteSpace(TitleInput) ||
            string.IsNullOrWhiteSpace(StartDateInput) ||
            string.IsNullOrWhiteSpace(EndDateInput))
        {
            return;
        }

        try
        {
            Course course = new()
            {
                CourseNumber = CourseNumberInput.Trim(),
                Title = TitleInput.Trim(),
                StartDate = StartDateInput.Trim(),
                EndDate = EndDateInput.Trim(),
                Length = LengthInput
            };

            await _courseService.Add(course);
            SaveMessage = "Kursen har lagts till.";
            ClearData();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
        }
    }

    private void ClearData()
    {
        CourseNumberInput = string.Empty;
        TitleInput = string.Empty;
        StartDateInput = string.Empty;
        EndDateInput = string.Empty;
        LengthInput = 0;
    }
}
