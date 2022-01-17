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
}