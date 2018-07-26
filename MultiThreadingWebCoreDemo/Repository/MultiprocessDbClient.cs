using MultiThreadingWebCoreDemo.Models;
using MultiThreadingWebCoreDemo.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MultiThreadingWebCoreDemo.Translators;

namespace MultiThreadingWebCoreDemo.Repository
{
    public class MultiprocessDbClient
    {
        public int InsertMultiprocessStatus(string connString, string userId, string module, int totalRecords)
        {
            var outParam = new SqlParameter("@ReturnId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@UserId",userId),
                new SqlParameter("@Module",module),
                new SqlParameter("@TotalRecords",totalRecords),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "InsertMultiprocessStatus", param);

            return (int)outParam.Value;
        }

        public void UpdateMultiprocessStatus(string connString, int id, decimal percentage, bool isCompleted,
            int failRecords, int successRecords)
        {
            SqlParameter[] param = {
                new SqlParameter("@Id",id),
                new SqlParameter("@Percentage",percentage),
                new SqlParameter("@IsCompleted",isCompleted),
                new SqlParameter("@FailedRecords",failRecords),
                new SqlParameter("@SuccessRecords",successRecords),
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "UpdateMultiprocessStatus", param);
        }

        public MultiprocessModel GetMultiprocessStatus(string connString ,string userId,string moduleName)
        {
            SqlParameter[] param = {
                new SqlParameter("@UserId",userId),
                new SqlParameter("@Module",moduleName)
            };
            return SqlHelper.ExtecuteProcedureReturnData<MultiprocessModel>(connString,
               "GetMultiprocessStatus", r => r.TranslateAsMultiprocess(), param);
        }
    }
}
