using MultiThreadingWebCoreDemo.Models;
using MultiThreadingWebCoreDemo.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreadingWebCoreDemo.Translators
{
    public static class MultiprocessTranslator
    {
        public static MultiprocessModel TranslateAsMultiprocess(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new MultiprocessModel();
            if (reader.IsColumnExists("Id"))
                item.Id = SqlHelper.GetNullableInt32(reader, "Id");

            if (reader.IsColumnExists("UserId"))
                item.UserId = SqlHelper.GetNullableString(reader, "UserId");

            if (reader.IsColumnExists("Module"))
                item.Module = SqlHelper.GetNullableString(reader, "Module");

            if (reader.IsColumnExists("TotalRecords"))
                item.TotalRecords = SqlHelper.GetNullableInt32(reader, "TotalRecords");

            if (reader.IsColumnExists("FailedRecords"))
                item.FailedRecords = SqlHelper.GetNullableInt32(reader, "FailedRecords");

            if (reader.IsColumnExists("SuccessRecords"))
                item.SuccessRecords = SqlHelper.GetNullableInt32(reader, "SuccessRecords");

            if (reader.IsColumnExists("Percentage"))
                item.Percentage = SqlHelper.GetNullableDecimal(reader, "Percentage");

            if (reader.IsColumnExists("IsCompleted"))
                item.IsCompleted = SqlHelper.GetBoolean(reader, "IsCompleted");

            return item;
        }
    }
}
