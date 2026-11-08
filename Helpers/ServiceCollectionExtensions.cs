using Client.Interfaces;
using Client.Factories;
using Client.Services;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Helpers;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection collection)
    {
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddSingleton<IPageViewFactory, PageViewFactory>();
        collection.AddTransient(typeof(IHttpService<>), typeof(HttpService<>));
        collection.AddTransient<HomeViewModel>();
        collection.AddTransient<CoursesViewModel>();
        collection.AddTransient<StudentsViewModel>();
        collection.AddTransient<AddCourseViewModel>();
        collection.AddTransient<AddStudentViewModel>();
        collection.AddTransient<TeachersViewModel>();
        collection.AddTransient<AddTeacherViewModel>();
        collection.AddTransient<ICourseService, CourseServices>();
        collection.AddTransient<IStudentService, StudentServices>();
        collection.AddTransient<ITeacherService, TeacherServices>();
    }
}
