using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaxiEmployeeAPI.IServices
{
    public interface IBeneficiaryService
    {
        Task<Beneficiary> Add(Beneficiary Beneficiary);
        Task<Beneficiary> Update(Beneficiary Beneficiary);
        Task<string> Delete(int BeneficiaryId);
        Task<Beneficiary> Get(int BeneficiaryId);
        Task<List<Beneficiary>> GetBeneficiariesByEmployeeId(int EmployeeId);

    }
}
