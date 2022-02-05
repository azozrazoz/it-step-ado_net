using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Practice.Repository
{
    public class ClientRepository
    {
        public static bool CheckClientById(string sqlConnection, int ID)
        {
            try
            {
                SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
                _sqlConnection.Open();

                var query = $"SELECT ID FROM [ClientEntity] WHERE ID = {ID}";

                var queryCommand = new SqlCommand(query, _sqlConnection);
                var result = queryCommand.ExecuteScalar();
                return (int)result > 0;
            }
            catch (Exception) { Console.WriteLine("Enter correct ID!"); return false; }
        }

        private static bool GetClientByIdForRestore(string sqlConnection, int ID)
        {
            SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
            _sqlConnection.Open();

            var query = $"SELECT IsDeleted FROM [ClientEntity] WHERE ID = {ID}";

            var queryCommand = new SqlCommand(query, _sqlConnection);
            string result = queryCommand.ExecuteScalar().ToString();
            return ConvertBoolean.Convert(result);
        }

        public static void GetAllClients(string sqlConnection, ref List<ClientEntity> clients)
        {
            SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
            _sqlConnection.Open();

            var query = "SELECT * FROM [ClientEntity]";

            var queryCommand = new SqlCommand(query, _sqlConnection);
            var cursor = queryCommand.ExecuteReader();
            while (cursor.Read())
            {
                clients.Add(new ClientEntity(cursor.GetInt32(0), cursor.GetString(1), 
                    cursor.GetDateTime(2), cursor.GetDateTime(3), cursor.GetDateTime(4), cursor.GetBoolean(5)));
            }
            cursor.Close();
        }

        public static void AddClient(string sqlConnection)
        {
            Console.WriteLine("Enter client id: ");
            int userId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter client full name: ");
            string userFName = Console.ReadLine();

            ClientEntity client = new ClientEntity(userId, userFName);
            
            SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
            _sqlConnection.Open();

            var query = "INSERT INTO [ClientEntity](" +
                "[ID], [FullName], [CreateOn], [UpdateOn], [DeleteOn], [IsDeleted]) " +
                $"VALUES ({client.ID}, N'{client.FullName}'," +
                $"'{client.CreateOn}', '{client.UpdateOn}', '{client.DeleteOn}', '{ConvertBoolean.Convert(client.IsDeleted)}')";

            var queryCommand = new SqlCommand(query, _sqlConnection);
            queryCommand.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        public static void DeleteClient(string sqlConnection, int ID)
        {
            SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
            _sqlConnection.Open();
            var query = $"UPDATE ClientEntity SET DeleteOn = '{DateTime.Now}', IsDeleted = 1 WHERE ID = {ID}";

            var queryCommand = new SqlCommand(query, _sqlConnection);
            queryCommand.ExecuteNonQuery();
            _sqlConnection.Close();
            Console.WriteLine("(Client is deleted!)");
        }

        public static void DirectDeletionClient(string sqlConnection, int ID)
        {
            SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
            _sqlConnection.Open();
            var query = $"DELETE FROM [ClientEntity] WHERE ID = {ID}";

            var queryCommand = new SqlCommand(query, _sqlConnection);
            queryCommand.ExecuteNonQuery();
            _sqlConnection.Close();
            Console.WriteLine("Client is deleted!");
        }

        public static void UpdateClientInfo(string sqlConnection, int ID)
        {
            if(!GetClientByIdForRestore(sqlConnection, ID))
            {
                Console.WriteLine("Enter new full name: ");
                string name = Console.ReadLine();
                SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
                _sqlConnection.Open();
                var query = $"UPDATE [ClientEntity] SET FullName = N'{name}', UpdateOn = '{DateTime.Now}'";

                var queryCommand = new SqlCommand(query, _sqlConnection);
                queryCommand.ExecuteNonQuery();
                _sqlConnection.Close();
                Console.WriteLine("Client updated!");
            }
            else
            {
                Console.WriteLine("User is deleted or does not exist.");
            }
        }

        public static void RestoreClient(string sqlConnection, int ID)
        {
            if (GetClientByIdForRestore(sqlConnection, ID))
            {
                SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
                _sqlConnection.Open();
                var query = $"UPDATE [ClientEntity] SET DeleteOn = CreateOn, IsDeleted = 0";

                var queryCommand = new SqlCommand(query, _sqlConnection);
                queryCommand.ExecuteNonQuery();
                _sqlConnection.Close();
                Console.WriteLine("Client restored!");
            }
            else
            {
                Console.WriteLine("The client has not been deleted or he has been restored!");
            }
            
        }
    }
}
