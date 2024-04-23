using Npgsql;
using dao.DTOs;
using dao.Persistence.DAO;
using dao.Persistence.Utils;

namespace dao.Persistence.Mapping
{
    public class ContactDAO : IContactDAO
    {
        private readonly string connectionString;

        public ContactDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ContactDTO GetContactByID(int id)
        {
            ContactDTO contact = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(NpgsqlUtils.OpenConnection()))
            {
                string sql = "SELECT * FROM contacts WHERE Id = @Id";
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contact = NpgsqlUtils.GetContactDTO(reader);
                }
                return contact;
            }

        }

        public void AddContact(ContactDTO contact)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(NpgsqlUtils.OpenConnection()))
            {
                string sql = "INSERT INTO Contact (Firstname, LastName) VALUES (@Firstname, @LastName)";
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Firstname", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public void UpdateContact(ContactDTO contact)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(NpgsqlUtils.OpenConnection()))
            {
                string sql = "UPDATE contacts SET FirstName = @FirstName, LastName = @LastName WHERE Id = @Id";
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@Id", contact.Id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }



        public void DeleteContact(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(NpgsqlUtils.OpenConnection()))
            {
                string sql = "DELETE FROM contacts WHERE Id = @Id";
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public IEnumerable<ContactDTO> GetAllContacts()
        {
            List<ContactDTO> contacts = new List<ContactDTO>();

            using (NpgsqlConnection connection = new NpgsqlConnection(NpgsqlUtils.OpenConnection()))
            {
                string sql = "SELECT * FROM contacts";
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contacts.Add(NpgsqlUtils.GetContactDTO(reader));
                }
                return contacts;
            }
        }


       


        
    }
}
