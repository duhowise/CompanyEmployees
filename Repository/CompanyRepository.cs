using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class CompanyRepository:RepositoryBase<Company>,ICompanyRepository
{
    public CompanyRepository(RepositoryContext context) : base(context)
    {
    }

    public IEnumerable<Company> GetAllCompanies(bool trackChanges)
    {
        return FindAll(trackChanges).OrderBy(c => c.Name).ToList();
    }

    public Company? GetCompany(Guid companyId, bool trackChanges)
    {
        return FindByCondition(x => x.Id.Equals(companyId), trackChanges).SingleOrDefault();
    }

    public void CreateCompany(Company company)
    {
        Create(company);
    }
}