using System;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class EditTeacherViewModel : ViewModelBase
{
    private readonly ITeacherService _teacherService;
    private readonly string _objectId;
    private string _teacherId = string.Empty;

    private string _firstName = string.Empty;
    public string FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }

    private string _lastName = string.Empty;
    public string LastName
    {
        get => _lastName;
        set => SetProperty(ref _lastName, value);
    }

    private string _email = string.Empty;
    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    private string _expertise = string.Empty;
    public string Expertise
    {
        get => _expertise;
        set => SetProperty(ref _expertise, value);
    }

    private string _saveMessage = string.Empty;
    public string SaveMessage
    {
        get => _saveMessage;
        private set => SetProperty(ref _saveMessage, value);
    }

    public EditTeacherViewModel(ITeacherService teacherService, string objectId)
    {
        _teacherService = teacherService;
        _objectId = objectId;
        PageTitle = "Redigera lärare";
        Init();
    }

    private async void Init()
    {
        try
        {
            var teacher = await _teacherService.Find(_objectId);
            _teacherId = teacher.Id;
            FirstName = teacher.FirstName;
            LastName = teacher.LastName;
            Email = teacher.Email;
            Expertise = teacher.Expertise;
            PageTitle = $"Redigera lärare: {teacher.FirstName} {teacher.LastName}";
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        SaveMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(_teacherId))
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(FirstName) ||
            string.IsNullOrWhiteSpace(LastName) ||
            string.IsNullOrWhiteSpace(Email) ||
            string.IsNullOrWhiteSpace(Expertise))
        {
            return;
        }

        try
        {
            await _teacherService.Update(new Teacher
            {
                Id = _teacherId,
                FirstName = FirstName.Trim(),
                LastName = LastName.Trim(),
                Email = Email.Trim(),
                Expertise = Expertise.Trim()
            }, _objectId);
            SaveMessage = "Lärarens uppgifter har sparats.";
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
        }
    }
}
