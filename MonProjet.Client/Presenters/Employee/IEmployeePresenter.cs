using MonProjet.Client.Models.Employee;
using MonProjet.Client.Presenters.Common;
using MonProjet.Client.View.Employee;

namespace MonProjet.Client.Presenters.Employee
{
    public interface IEmployeePresenter : IPresenter<IEmployeeView>
    {
        EmployeeViewModel ViewModel { get; }
        Task InitializeAsync();
        Task LoadEmployeesAsync();
    }
}
