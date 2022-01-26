using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class EmployeeRepository:RepositoryBase<Employee>,IEmployeeRepository
{
    private readonly RepositoryContext _context;

    public EmployeeRepository(RepositoryContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChanges)
    {
        return await FindByCondition(x => x.CompanyId.Equals(companyId), trackChanges).OrderBy(x=>x.Name).ToListAsync();
    }

    public async Task<Employee?> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
    {
        return await FindByCondition(x => x.CompanyId.Equals(companyId) && x.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
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