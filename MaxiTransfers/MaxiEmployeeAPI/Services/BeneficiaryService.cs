using Dapper;
using MaxiEmployeeAPI.Common;
using MaxiEmployeeAPI.IServices;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MaxiEmployeeAPI.Services
{
    public class BeneficiaryService : IBeneficiaryService
    {
        Beneficiary _beneficiary = new Beneficiary();
        List<Beneficiary> _beneficiaries = new List<Beneficiary>();
        public async Task<Beneficiary> Add(Beneficiary Beneficiary)
        {
            _beneficiary = new Beneficiary();
            try
            {
                using (IDbConnection con = new SqlConnection(Global.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@FirstName", Beneficiary.FirstName);
                    parameters.Add("@LastName", Beneficiary.LastName);
                    parameters.Add("@DateOfBirth", Beneficiary.DateOfBirth);
                    parameters.Add("@EmployeeId", Beneficiary.EmployeeId);
                    parameters.Add("@Curp", Beneficiary.Curp);
                    parameters.Add("@Ssn", Beneficiary.Ssn);
                    parameters.Add("@PhoneNumber", Beneficiary.PhoneNumber);
                    parameters.Add("@Nationality", Beneficiary.Nationality);
                    //parameters.Add("@ParticipationPercentage", Beneficiary.ParticipationPercentage);
                    var _beneficiaries = await con.QueryAsync<Beneficiary>("dbo.spr_AddBeneficiary", parameters, commandType: CommandType.StoredProcedure);
                    if (_beneficiaries != null && _beneficiaries.Count() > 0)
                    {
                        _beneficiary = _beneficiaries.FirstOrDefault();
                    }
                }
            }
            catch(Exception ex)
            {
                _beneficiary.Message = ex.Message;
            }
            return _beneficiary;
        }

        public async Task<string> Delete(int BeneficiaryId)
        {
            string message = string.Empty;
            try
            {
                using (IDbConnection con = new SqlConnection(Global.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", BeneficiaryId);
                    var _beneficiaries = await con.QueryAsync<Beneficiary>("dbo.spr_DeleteBeneficiary", parameters, commandType: CommandType.StoredProcedure);
                    if (_beneficiaries != null && _beneficiaries.Count() > 0)
                    {
                        _beneficiary = _beneficiaries.FirstOrDefault();
                    }
                }
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public async Task<Beneficiary> Get(int BeneficiaryId)
        {
            _beneficiary = new Beneficiary();
            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", _beneficiary.Id);
                var _beneficiaries = await con.QueryAsync<Beneficiary>("dbo.spr_GetBeneficiary", parameters, commandType: CommandType.StoredProcedure);
                if (_beneficiaries != null && _beneficiaries.Count() > 0)
                {
                    _beneficiary = _beneficiaries.FirstOrDefault();
                }
            }
            return _beneficiary;
        }

        public async Task<List<Beneficiary>> GetBeneficiariesByEmployeeId(int EmployeeId)
        {
            _beneficiaries = new List<Beneficiary>();
            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeId", EmployeeId);
                var _benefs = (await con.QueryAsync<Beneficiary>("dbo.spr_GetBeneficiaries", parameters, commandType: CommandType.StoredProcedure)).ToList();
                if (_benefs != null && _benefs.Count() > 0)
                {
                    _beneficiaries = _benefs;
                }
            }
            return _beneficiaries;
        }

        public async Task<Beneficiary> Update(Beneficiary Beneficiary)
        {
            _beneficiary = new Beneficiary();
            try
            {
                using (IDbConnection con = new SqlConnection(Global.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", Beneficiary.Id);
                    parameters.Add("@FirstName", Beneficiary.FirstName);
                    parameters.Add("@LastName", Beneficiary.LastName);
                    parameters.Add("@DateOfBirth", Beneficiary.DateOfBirth);
                    parameters.Add("@EmployeeId", Beneficiary.EmployeeId);
                    parameters.Add("@Curp", Beneficiary.Curp);
                    parameters.Add("@Ssn", Beneficiary.Ssn);
                    parameters.Add("@PhoneNumber", Beneficiary.PhoneNumber);
                    parameters.Add("@Nationality", Beneficiary.Nationality);
                    var _beneficiaries = await con.QueryAsync<Beneficiary>("dbo.spr_UpdateBeneficiary", parameters, commandType: CommandType.StoredProcedure);
                    if (_beneficiaries != null && _beneficiaries.Count() > 0)
                    {
                        _beneficiary = _beneficiaries.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                _beneficiary.Message = ex.Message;
            }
            return _beneficiary;
        }

        
    }
}
