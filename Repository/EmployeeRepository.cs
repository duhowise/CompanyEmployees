using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryContext context) : base(context)
    {
    }


    public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters,
        bool trackChanges)
    {
        var employees= await FindByCondition(x => x.CompanyId.Equals(companyId) && (x.Age>=employeeParameters.MinAge&& x.Age<=employeeParameters.MaxAge), trackChanges)
                .OrderBy(x => x.Name)
                .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
                .Take(employeeParameters.PageSize)
                .ToListAsync();
        var count = await FindByCondition(x => x.CompanyId.Equals(companyId), trackChanges).CountAsync();

        return new PagedList<Employee>(employees, count, employeeParameters.PageNumber, employeeParameters.PageSize);
    }

    public async Task<Employee?> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
    {
        return await FindByCondition(x => x.CompanyId.Equals(companyId) && x.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }

    public void CreateEmployeeForCompany(Guid companyId, Employee employee)
    {
        employee.CompanyId = companyId;
        Create(employee);
    }


    public void DeleteEmployee(Employee employee)
    {
        Delete(employee);
    }
}