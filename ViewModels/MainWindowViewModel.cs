using Client.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IPageViewFactory _pageViewFactory;

    [ObservableProperty]
    private ViewModelBase _currentView = new();

    public MainWindowViewModel(IPageViewFactory pageViewFactory)
    {
        _pageViewFactory = pageViewFactory;
        NavigateTo(Enums.PageViewsEnum.Home);
    }

    private void NavigateTo(Enums.PageViewsEnum page, string? itemId = null)
    {
        CurrentView = itemId is null
            ? _pageViewFactory.LoadPageViewModel(page)
            : _pageViewFactory.LoadPageViewModel(page, itemId);
    }

    [RelayCommand]
    private void GoToHome() => NavigateTo(Enums.PageViewsEnum.Home);

    [RelayCommand]
    private void GoToCourses() => NavigateTo(Enums.PageViewsEnum.Courses);

    [RelayCommand]
    private void GoToStudents() => NavigateTo(Enums.PageViewsEnum.Students);

    [RelayCommand]
    private void GoToTeachers() => NavigateTo(Enums.PageViewsEnum.Teachers);

    [RelayCommand]
    private void GoToAddCourse() => NavigateTo(Enums.PageViewsEnum.AddCourse);

    [RelayCommand]
    private void GoToAddStudent() => NavigateTo(Enums.PageViewsEnum.AddStudent);

    [RelayCommand]
    private void GoToAddTeacher() => NavigateTo(Enums.PageViewsEnum.AddTeacher);

    [RelayCommand]
    private void GoToEditTeacher(string objectId) => NavigateTo(Enums.PageViewsEnum.EditTeacher, objectId);

    [RelayCommand]
    private void GoToEditCourse(string courseNumber) => NavigateTo(Enums.PageViewsEnum.EditCourse, courseNumber);
}
