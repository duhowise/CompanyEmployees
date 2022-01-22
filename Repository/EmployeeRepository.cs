using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class EmployeeRepository:RepositoryBase<Employee>,IEmployeeRepository
{
    private readonly RepositoryContext _context;

    public EmployeeRepository(RepositoryContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges)
    {
        return FindByCondition(x => x.CompanyId.Equals(companyId), trackChanges).OrderBy(x=>x.Name);
    }

    public Employee? GetEmployee(Guid companyId, Guid id, bool trackChanges)
    {
        return FindByCondition(x => x.CompanyId.Equals(companyId) && x.Id.Equals(id), trackChanges).SingleOrDefault();
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