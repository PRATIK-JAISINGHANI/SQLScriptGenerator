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

        [HttpPost, ActionName("Connect")]
        public HttpResponseMessage Connect(ConnectRequest request)
        {
            var response = new ConnectResponse();
            ConnectInternal(request, response);
            return ConvertToJson<ConnectResponse>(response);
        }

        [HttpPost, ActionName("Execute")]
        public HttpResponseMessage Execute(ExecuteRequest request)
        {
            var response = new ExecuteResponse();
            ExecuteInternal(request, response);
            return ConvertToJson<ExecuteResponse>(response);
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

        private void ConnectInternal(ConnectRequest request, ConnectResponse response)
        {
            var sqlHelper = new SQLHelper();
            var connectionString = sqlHelper.BuildConnectionString(request.ServerName, 
                                                                   request.DatabaseName, 
                                                                   request.Username, 
                                                                   request.Password);
            //
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Connection String was not found.");
            //
            if(sqlHelper.CheckConnection(connectionString))
            {
                response.IsConnected = true;
                response.ConnectionString = connectionString;
            }
            else
                response.IsConnected = false;
        }

        private void ExecuteInternal(ExecuteRequest request, ExecuteResponse response)
        {
            var sqlHelper = new SQLHelper();
            sqlHelper.ExecuteQuery(request.ConnectionString, request.Query, request.IncludeIdentity);
            var Script = sqlHelper.GenerateScripts();
            if (Script != null)
            {
                response.IsActionSuccessful = true;
                response.Scripts = Script;
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
