/*using Amelia.PatientUnderstanding.Entity;
using Amelia.PatientUnderstanding.Helper;
using Amelia.PatientUnderstanding.Interface;
using Amelia.PatientUnderstanding.Model;*/
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Data;
using UserRegistration.Helper;
using UserRegistration.Interface;
using UserRegistration.Models;

namespace UserRegistration.DAL
{
    public class AmeliaDBDAL : IAmeliaDBDAL
    {
        private readonly ApplicationDbContext _ameliaDBContext;
        public AmeliaDBDAL(ApplicationDbContext ameliaDBContext)
        {
            _ameliaDBContext = ameliaDBContext;
        }

        public async Task<List<PageExpiration>> GetPageExpirationByToken(string token)
        {
            try
            {
                var response = _ameliaDBContext.LoadStoredProc("dbo.sp_GetPageExpirationByToken")
                     .WithSqlParam("@token", token)
                     .ExecuteStoredProc<PageExpiration>();

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> UpdatePageExpiration(PageExpiration pageExpiration, bool isSubmitted)
        {
            /* await Task.Delay(5000);*/
            try
            {
                
                _ameliaDBContext.LoadStoredProc("dbo.sp_UpdatePageExpiration")
                    .WithSqlParam("@IsOpened", pageExpiration.IsOpened)
                    .WithSqlParam("@IsExpired", pageExpiration.IsExpired)
                    .WithSqlParam("@SubmissionDate", isSubmitted ? pageExpiration.SubmissionDate : null)
                    .WithSqlParam("@Token", pageExpiration.token)
                    .ExecuteStoredProc<bool>();

                return true;
            }
            catch (Exception ex)
            {
               
                throw;
            }
        }

    }
}
