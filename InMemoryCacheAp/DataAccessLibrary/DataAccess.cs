using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class DataAccess
    {
        private readonly IMemoryCache _memoryCache;

        public DataAccess(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;

        }

        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> employees = new();

            employees.Add(new EmployeeModel { FirstName = "Jhone", LastName = "Doe" });
            employees.Add(new EmployeeModel { FirstName = "Robert", LastName = "Martin" });
            employees.Add(new EmployeeModel { FirstName = "Angela", LastName = "Martin" });
            employees.Append(new EmployeeModel { FirstName = "Sean", LastName = "Medas" });

            Thread.Sleep(3000);

            return employees;
        }


        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            List<EmployeeModel> employees = new();

            employees.Add(new EmployeeModel { FirstName = "Jhone", LastName = "Doe" });
            employees.Add(new EmployeeModel { FirstName = "Robert", LastName = "Martin" });
            employees.Add(new EmployeeModel { FirstName = "Angela", LastName = "Martin" });
            employees.Append(new EmployeeModel { FirstName = "Sean", LastName = "Medas" });

            await Task.Delay(3000);

            return employees;
        }

        public async Task<List<EmployeeModel>> GetEmployeesCache()
        {
            List<EmployeeModel> employees;

            //get the data if it is exist in cache
            employees = _memoryCache.Get<List<EmployeeModel>>("employees");

            //cache the data if not exist
            if(employees is null)
            {
                employees = new();

                employees.Add(new EmployeeModel { FirstName = "Jhone", LastName = "Doe" });
                employees.Add(new EmployeeModel { FirstName = "Robert", LastName = "Martin" });
                employees.Add(new EmployeeModel { FirstName = "Angela", LastName = "Martin" });
                employees.Append(new EmployeeModel { FirstName = "Sean", LastName = "Medas" });

                await Task.Delay(3000);

                //cache into memory
                _memoryCache.Set("employees", employees, TimeSpan.FromMinutes(1));
            }

            return employees;
        }
    }
}
