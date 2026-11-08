using System;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class AddTeacherViewModel : ViewModelBase
{
    private readonly ITeacherService _teacherService;

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

    public AddTeacherViewModel(ITeacherService teacherService)
    {
        _teacherService = teacherService;
        PageTitle = "Lägg till lärare";
    }

    [RelayCommand]
    private async Task Save()
    {
        SaveMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(FirstName) ||
            string.IsNullOrWhiteSpace(LastName) ||
            string.IsNullOrWhiteSpace(Email) ||
            string.IsNullOrWhiteSpace(Expertise))
        {
            return;
        }

        try
        {
            await _teacherService.Add(new Teacher
            {
                FirstName = FirstName.Trim(),
                LastName = LastName.Trim(),
                Email = Email.Trim(),
                Expertise = Expertise.Trim()
            });

            SaveMessage = "Läraren har lagts till.";
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Expertise = string.Empty;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
        }
    }
}
