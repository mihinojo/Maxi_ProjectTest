using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaxiEmployeeAPI.IServices
{
    public interface IEmployeeService
    {
        Task<Employee> Add(Employee employee);
        Task<Employee> Update(Employee employee);
        Task<string> Delete(int employeeId);
        Task<Employee> Get(int employeeId);
        Task<List<Employee>> GetEmployees();

    }
}
