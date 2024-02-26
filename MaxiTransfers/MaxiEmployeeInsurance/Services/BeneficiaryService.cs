using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiEmployeeInsurance.Services
{
    public class BeneficiaryService
    {
        private readonly String _ApiBaseAdress;
        private readonly String _Url;

        public BeneficiaryService()
        {
            _ApiBaseAdress = ConfigurationManager.AppSettings["ApiBaseAdress"];
            _Url = "api/Beneficiary";
        }

        public async Task<List<Beneficiary>?> GetBeneficiariesAsync(int Id)
        {
            var apiService = new AgentService<List<Beneficiary>, Response>();
            string url = _Url + "/" + Id.ToString();
            AgentParameters<List<Beneficiary>> aparameters = new AgentParameters<List<Beneficiary>>(_ApiBaseAdress, url, Method.Get, MediaTypes.applicationJson);
            await apiService.GetAsync(aparameters);
            var resp = apiService.resp;
            return resp.Result != null ? JsonConvert.DeserializeObject<List<Beneficiary>>(resp.Result.ToString()) : new List<Beneficiary>();
        }

        public async Task<Response> DeleteBeneficiaryAsync(int Id)
        {
            var apiService = new AgentService<string, Response>();
            string url = _Url + "/" + Id.ToString();
            AgentParameters<string> aparameters = new AgentParameters<string>(_ApiBaseAdress, url, Method.Get, MediaTypes.applicationJson);
            await apiService.DeleteAsync(aparameters);
            return apiService.resp;
        }

        public async Task<Response> UpdateBeneficiaryAsync(Beneficiary ben)
        {
            var apiService = new AgentService<Beneficiary, Response>();
            AgentParameters<Beneficiary> aparameters = new AgentParameters<Beneficiary>(_ApiBaseAdress, _Url, Method.Get, MediaTypes.applicationJson, ben);
            await apiService.PutAsync(aparameters);
            return apiService.resp;
        }

        public async Task<Response> AddBeneficiaryAsync(Beneficiary ben)
        {
            var apiService = new AgentService<Beneficiary, Response>();
            AgentParameters<Beneficiary> aparameters = new AgentParameters<Beneficiary>(_ApiBaseAdress, _Url, Method.Get, MediaTypes.applicationJson, ben);
            await apiService.PostAsync(aparameters);
            return apiService.resp;
        }
    }
}
