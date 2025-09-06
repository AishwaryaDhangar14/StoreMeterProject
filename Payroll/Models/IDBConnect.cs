using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public interface IDBConnect
    {
        string getConnectionStr(string brcode);
        string getEntityConnectionStr();
        SqlConnection CreateCon(string brcode);
        bool ConnectCon(SqlConnection con);
        bool DisConnectCon(SqlConnection con);
    }
}
