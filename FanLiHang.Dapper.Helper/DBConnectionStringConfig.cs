using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FanLiHang.Dapper.Helper
{
    public class DBConnectionStringConfig : IDBConnectionStringConfig
    {
        private IConfiguration configurationRoot;
        public DBConnectionStringConfig(IConfiguration configurationRoot)
        {
            this.configurationRoot = configurationRoot;
        }

        public string CRMSQLConnectionString
        {
            get
            {
                return configurationRoot.GetConnectionString("default");
            }
        }

        public string GlantOMISSQLConnectionString
        {
            get
            {
                return configurationRoot.GetConnectionString("glant");
            }
        }
        public string TMDepotSQLConnectionString
        {
            get
            {
                return configurationRoot.GetConnectionString("tmdepot");
            }
        }
        public string TMBackDepotSQLConnectionString
        {
            get
            {
                return configurationRoot.GetConnectionString("backTmdepot");
            }
        }


        public string AmazonSQLConnectionString
        {
            get
            {
                return configurationRoot.GetConnectionString("amazon");
            }
        }

        public string TMProductSQLConnetiongString
        {
            get
            {
                return configurationRoot.GetConnectionString("bktmpro");
            }
        }

        public string DefaultConnectionString => configurationRoot.GetConnectionString("default");
    }
}
