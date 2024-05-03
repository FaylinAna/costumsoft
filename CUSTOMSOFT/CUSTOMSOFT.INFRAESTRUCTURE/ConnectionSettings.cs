using CUSTOMSOFT.INFRAESTRUCTURE.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CUSTOMSOFT.INFRAESTRUCTURE
{
    public class ConnectionSettings 
    {
        private readonly IConfiguration _configuration;
     
        public ConnectionSettings(IConfiguration configuration)
        {
            _configuration = configuration;
          
        }
      
        public NpgsqlConnection OpenSQLConnectionAsync()
        {
            var conection = new NpgsqlConnection(_configuration.GetConnectionString("SQLConnection"));
                conection.Open();
                
            return conection;
        }
        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("SQLConnection");
        }
    }

}







