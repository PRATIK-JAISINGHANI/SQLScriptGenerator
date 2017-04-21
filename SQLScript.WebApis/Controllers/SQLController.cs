using Newtonsoft.Json;
using SQLQueryGenerator.DataContracts;
using SQLScript.WebApis.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SQLScript.WebApis.Controllers
{
    public class SQLController : ApiController
    {
        #region Public Methods

        [HttpPost,ActionName("GenerateScripts")]
        public HttpResponseMessage GenerateScripts(SQLScriptRequest request)
        {
            var response = new SQLScriptResponse();
            GenerateScriptsInternal(request, response);
            return ConvertToJson<SQLScriptResponse>(response);
        }

        #endregion

        #region Helper Methods

        private void GenerateScriptsInternal(SQLScriptRequest request, SQLScriptResponse response)
        {
            var obj = new SQLHelper();
            var connectionString = obj.BuildConnectionString
                                    (request.ServerName,
                                     request.DatabaseName,
                                     request.Username,
                                     request.Password);
            //
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Connection string was not found.");
            //
            obj.ExecuteQuery(connectionString, request.Query, request.IncludeIdentity);
            var Script = obj.GenerateScripts();
            if (Script != null)
            {
                response.IsActionSuccessful = true;
                response.Script = Script;
            }
            else
                response.IsActionSuccessful = false;
        }

        protected HttpResponseMessage ConvertToJson<T>(T response)
        {
            var result = JsonConvert.SerializeObject(response);
            return new HttpResponseMessage() { Content = new StringContent(result, System.Text.Encoding.UTF8, "application/json") };
        }
        #endregion
    }
}
