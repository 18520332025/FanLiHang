using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FanLiHang.Dapper.Helper
{
    public interface IDBConnectionStringConfig
    {
        string DefaultConnectionString
        {
            get;
        }
        
        string CRMSQLConnectionString
        {
            get;
        }

        string GlantOMISSQLConnectionString
        {
            get;
        }
        string TMDepotSQLConnectionString
        {
            get;
        }
        string TMBackDepotSQLConnectionString
        {
            get;
        }


        string AmazonSQLConnectionString
        {
            get;
        }

        string TMProductSQLConnetiongString
        {
            get;
        }

    }
}
