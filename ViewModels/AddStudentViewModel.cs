using System;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class AddStudentViewModel : ViewModelBase
{
    private readonly IStudentService _studentService;

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

    private string _saveMessage = string.Empty;
    public string SaveMessage
    {
        get => _saveMessage;
        private set => SetProperty(ref _saveMessage, value);
    }

    public AddStudentViewModel(IStudentService studentService)
    {
        _studentService = studentService;
        PageTitle = "Lägg till elev";
    }

    [RelayCommand]
    private async Task Save()
    {
        SaveMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(FirstName) ||
            string.IsNullOrWhiteSpace(LastName) ||
            string.IsNullOrWhiteSpace(Email))
        {
            return;
        }

        try
        {
            await _studentService.Add(new Student
            {
                FirstName = FirstName.Trim(),
                LastName = LastName.Trim(),
                Email = Email.Trim()
            });

            SaveMessage = "Eleven har lagts till.";
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
        }
    }
}
