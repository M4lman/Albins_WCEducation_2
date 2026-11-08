using System;
using Client.Enums;
using Client.Interfaces;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Factories;

public class PageViewFactory : IPageViewFactory
{
    private readonly IServiceProvider _services;

    public PageViewFactory(IServiceProvider services)
    {
        _services = services;
    }

    public ViewModelBase LoadPageViewModel(PageViewsEnum page)
    {
        return page switch
        {
            PageViewsEnum.Home => _services.GetRequiredService<HomeViewModel>(),
            PageViewsEnum.Courses => _services.GetRequiredService<CoursesViewModel>(),
            PageViewsEnum.Students => _services.GetRequiredService<StudentsViewModel>(),
            PageViewsEnum.AddStudent => _services.GetRequiredService<AddStudentViewModel>(),
            PageViewsEnum.Teachers => _services.GetRequiredService<TeachersViewModel>(),
            PageViewsEnum.AddTeacher => _services.GetRequiredService<AddTeacherViewModel>(),
            PageViewsEnum.AddCourse => _services.GetRequiredService<AddCourseViewModel>(),
            _ => throw new InvalidOperationException("Det fanns inte det du ville ha"),
        };
    }

    public ViewModelBase LoadPageViewModel(PageViewsEnum page, string itemNumber)
    {
        return page switch
        {
            PageViewsEnum.EditCourse => ActivatorUtilities.CreateInstance<EditCourseViewModel>(_services, itemNumber),
            PageViewsEnum.EditTeacher => ActivatorUtilities.CreateInstance<EditTeacherViewModel>(_services, itemNumber),
            _ => throw new InvalidOperationException("Det fanns inte det du ville ha")
        };
    }
}
