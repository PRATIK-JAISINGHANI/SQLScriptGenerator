using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SQLScript.WebApis.Helper
{
    public class SQLHelper
    {
        #region Constants

        public string DEFAULT_CONNECTION_STRING = string.Format("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TrialDB-3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public string INSERT_QUERY = "If Not Exists (Select 1 From TABLE_NAME Where WHERE_CLAUSE) BEGIN Insert into TABLE_NAME (COLUMN_NAMES) values (VALUES) END #";
        public string INSERT_QUERY_BAKED = string.Empty;
        public DataTable shownData;
        public string tableName = string.Empty;
        public static List<String> numericDataTypes = new List<string>() { "SYSTEM.INT16", "SYSTEM.INT32", "SYSTEM.INT64", "SYSTEM.BOOLEAN" };
        public static List<String> dateTimeDataTypes = new List<string>() { "System.DateTime" };

        #endregion

        #region Public Methods

        public string BuildConnectionString(string serverName, string databaseName, string Username, string Password)
        {
            if (string.IsNullOrEmpty(serverName) || string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                return DEFAULT_CONNECTION_STRING;
            else
                return "Data Source=" + serverName +
                       ";Initial Catalog=" + databaseName +
                       ";Integrated Security=False;User ID=" + Username +
                       ";Password=" + Password +
                       ";Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public void ExecuteQuery(string connectionString, string sqlQuery, bool IncludeIdentity)
        {
            INSERT_QUERY_BAKED = string.Empty;
            if (string.IsNullOrEmpty(sqlQuery))
            {
             throw new Exception("Please insert the query to execute.");
            }
            //
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    SetTableName(sqlQuery);
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        List<string> columnsList = new List<string>();
                        //
                        var dt = dataReader.GetSchemaTable();

                        foreach (DataRow column in dt.Rows)
                            columnsList.Add(column[0].ToString());
                        //
                        if (columnsList.Contains("VersionNo"))
                            columnsList.Remove("VersionNo");
                        //
                        if (!IncludeIdentity && columnsList.Contains("Id"))
                            columnsList.Remove("Id");

                        //
                        string query = string.Empty;
                        //
                        if (sqlQuery.ToUpper().Contains("SELECT * FROM"))
                        {
                            foreach (var item in columnsList)
                                if (string.IsNullOrEmpty(query))
                                    query = item;
                                else
                                    query = query + "," + item;

                            sqlQuery = sqlQuery.Replace("*", query);
                        }

                        dataReader.Close();
                        //
                        using (SqlCommand commandFinal = new SqlCommand(sqlQuery, connection))
                        {
                            commandFinal.CommandType = CommandType.Text;
                            using (SqlDataReader dataReaderFinal = commandFinal.ExecuteReader())
                            {
                                var dt2 = new DataTable();
                                dt2.Load(dataReaderFinal);
                                shownData = dt2;
                            }
                        }
                    }
                }
            }

        }

        public Array GenerateScripts()
        {
            if (shownData.Rows.Count == 0)
                throw new Exception("No data present in grid to generate scripts");
            //
            var INSERT_QUERY_RAW = INSERT_QUERY;
            List<string> columns = new List<string>();
            Dictionary<string, string> columnWithDataTypes = new Dictionary<string, string>();
            //
            foreach (DataColumn column in shownData.Columns)
            {
                columns.Add(column.ColumnName);
                columnWithDataTypes.Add(column.ColumnName, column.DataType.ToString());
            }
            //
            string columnNames = string.Empty;
            string columnValues = string.Empty;
            //
            foreach (DataColumn column in shownData.Columns)
                if (string.IsNullOrEmpty(columnNames))
                    columnNames = column.ColumnName;
                else
                    columnNames = columnNames + "," + column.ColumnName;

            INSERT_QUERY_RAW = INSERT_QUERY_RAW.Replace("TABLE_NAME", tableName);
            INSERT_QUERY_RAW = INSERT_QUERY_RAW.Replace("COLUMN_NAMES", columnNames);

            if (string.IsNullOrEmpty(INSERT_QUERY_BAKED))
                INSERT_QUERY_BAKED = INSERT_QUERY_RAW;

            foreach (DataRow dr in shownData.Rows)
            {
                columnValues = string.Empty;
                foreach (var item in columns)
                {
                    if (string.IsNullOrEmpty(columnValues))
                    {
                        if (numericDataTypes.Contains(columnWithDataTypes[item.ToString()].ToUpper()))
                            columnValues = dr[item.ToString()].ToString();
                        else
                        {
                            if (dateTimeDataTypes.Contains(columnWithDataTypes[item.ToString()]) && !string.IsNullOrEmpty(dr[item.ToString()].ToString()))
                                columnValues = string.Concat("'", Convert.ToDateTime(dr[item.ToString()]).ToString("yyyy-MM-dd HH:mm:ss"), "'");
                            else
                                columnValues = string.Concat("'", dr[item.ToString()].ToString(), "'");
                        }
                    }
                    else
                    {
                        if (numericDataTypes.Contains(columnWithDataTypes[item.ToString()].ToUpper()))
                            columnValues = columnValues + "," + dr[item.ToString()].ToString();
                        else
                        {
                            if (dateTimeDataTypes.Contains(columnWithDataTypes[item.ToString()]) && !string.IsNullOrEmpty(dr[item.ToString()].ToString()))
                                columnValues = columnValues + "," + string.Concat("'", Convert.ToDateTime(dr[item.ToString()]).ToString("yyyy-MM-dd HH:mm:ss"), "'");
                            else
                                columnValues = columnValues + "," + string.Concat("'", dr[item.ToString()].ToString(), "'");
                        }
                    }
                }

                if (columnValues.Contains("True"))
                    columnValues = columnValues.Replace("True", "1");

                if (columnValues.Contains("False"))
                    columnValues = columnValues.Replace("False", "0");

                if (columnValues.Contains("''"))
                    columnValues = columnValues.Replace("''", "NULL");

                if (columnValues.ToUpper().Contains(",,"))
                    columnValues = columnValues.Replace(",,", ",NULL,");

                if (INSERT_QUERY_RAW.Contains("VALUES"))
                    INSERT_QUERY_RAW = INSERT_QUERY_RAW.Replace("VALUES", columnValues);
                else
                    INSERT_QUERY_RAW = INSERT_QUERY_RAW + "#" + INSERT_QUERY_BAKED.Replace("VALUES", columnValues);

                if (INSERT_QUERY_RAW.Contains("WHERE_CLAUSE"))
                    INSERT_QUERY_RAW = INSERT_QUERY_RAW.Replace("WHERE_CLAUSE", GenerateWhereClause(columnWithDataTypes, columnNames, columnValues));
            }
            return ConvertToArray(INSERT_QUERY_RAW);
        }

        private void SetTableName(string sqlQuery)
        {
            sqlQuery = sqlQuery.Remove(0, sqlQuery.ToUpper().IndexOf("FROM") - 1);
            sqlQuery = sqlQuery.ToUpper().Replace("FROM", "");
            sqlQuery = sqlQuery.TrimStart();
            if (sqlQuery.Contains(" "))
                sqlQuery = sqlQuery.Remove(sqlQuery.IndexOf(" "));
            var space = " ";
            foreach (char c in sqlQuery)
                sqlQuery = sqlQuery.Replace(space, "");

            tableName = sqlQuery;
        }

        private string GenerateWhereClause(Dictionary<string, string> columnsWithDataTypes, string columnNames, string columnValues)
        {
            var columnNamesList = columnNames.Split(',').ToList<string>();
            var columnValuesList = columnValues.Split(',').ToList<string>();
            var whereClause = string.Empty;
            if (columnNamesList.Count == columnValuesList.Count)
            {

                for (int i = 0; i < columnValuesList.Count; i++)
                {
                    if (string.IsNullOrEmpty(whereClause))
                    {
                        if (!dateTimeDataTypes.Contains(columnsWithDataTypes[columnNamesList[i]]))
                        {
                            if (columnValuesList[i] != "NULL")
                                whereClause = columnNamesList[i] + " = " + columnValuesList[i];
                            else
                                whereClause = columnNamesList[i] + " IS " + columnValuesList[i];
                        }
                    }
                    else
                    {
                        if (!dateTimeDataTypes.Contains(columnsWithDataTypes[columnNamesList[i]]))
                        {
                            if (columnValuesList[i] != "NULL")
                                whereClause = whereClause + " And " + columnNamesList[i] + " = " + columnValuesList[i];
                            else
                                whereClause = whereClause + " And " + columnNamesList[i] + " IS " + columnValuesList[i];
                        }
                    }
                }
            }
            return whereClause;
        }

        private Array ConvertToArray(string generatedScripts)
        {
            return generatedScripts.Split('#').ToArray<string>();
        }
        #endregion 
    }
}