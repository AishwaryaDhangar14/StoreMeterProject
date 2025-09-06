using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Payroll.Models;
using System.Data.Entity.Core.EntityClient;

namespace Payroll.Models
{
    public class DBConnect : IDBConnect
    {

        SqlConnection con;
        Soft_Entity db = new Soft_Entity();
        
        public string getConnectionStr(string brcode="001")
        {
            string providerString = "";//sqlBuilder.ToString();
            return providerString;
        }

        public string getEntityConnectionStr()
        {
            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.Provider = "System.Data.SqlClient";
            entityBuilder.ProviderConnectionString = getConnectionStr("001");
            entityBuilder.Metadata = "res://*/ListDB_Model.csdl|res://*/ListDB_Model.ssdl|res://*/ListDB_Model.msl";

            return entityBuilder.ToString();
        }

        public SqlConnection CreateCon(string brcode)
        {
            con = new SqlConnection();
            con.ConnectionString = getConnectionStr("001");
            return con;
        }
        
        public bool ConnectCon(SqlConnection arg_con)
        {
            if (!arg_con.State.Equals(ConnectionState.Open))
            {
                try
                {
                    arg_con.Open();

                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DisConnectCon(SqlConnection arg_con)
        {
            if (arg_con.State.Equals(ConnectionState.Open))
            {
                try
                {
                    arg_con.Close();
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}