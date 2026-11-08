using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    private string _pageTitle = "";
}
