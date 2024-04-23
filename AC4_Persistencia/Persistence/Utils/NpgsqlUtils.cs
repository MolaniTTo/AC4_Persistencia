using Microsoft.Extensions.Configuration;
using Npgsql;
using dao.DTOs;

namespace dao.Persistence.Utils
{
    public class NpgsqlUtils
    {
        public static string OpenConnection()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile(@"appsettings.json", optional:false, reloadOnChange:true)
                .Build();
            return config.GetConnectionString("ElephantConn");
        }

        public static ContactDTO GetContactDTO(NpgsqlDataReader reader)
        {
            ContactDTO contact = new ContactDTO
            {
                Id = reader.GetInt32(0),
                FirstName = reader.IsDBNull(1) ? null : reader.GetString(1),
                LastName = reader.IsDBNull(2) ? null : reader.GetString(2)
            };
            return contact;
        }
    }
}
