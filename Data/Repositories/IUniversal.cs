using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System;
using System.Threading.Tasks;

namespace Entities.Repositories
{
    public interface IUniversal
    {
        Task<int> ExecuteNonQuery(string command, CommandType type = CommandType.Text, params SqlParameter[] parameters);
        Task<object> ExecuteScalar(string command, CommandType type = CommandType.Text, params SqlParameter[] parameters);
        Task<DataTable> ExecuteDataTable(string command, CommandType type = CommandType.Text, params SqlParameter[] parameters);
        Task<DataRow> ExecuteDataRow(string command, CommandType type = CommandType.Text, params SqlParameter[] parameters);

        Task<int> ExecuteNonQuery(string command, CommandType type = CommandType.Text, int timeout = 30, params SqlParameter[] parameters);
        Task<object> ExecuteScalar(string command, CommandType type = CommandType.Text, int timeout = 30, params SqlParameter[] parameters);
        
        Task<DataTable> ExecuteDataTable(string command, CommandType type = CommandType.Text, int timeout = 30, params SqlParameter[] parameters);
        Task<DataRow> ExecuteDataRow(string command, CommandType type = CommandType.Text, int timeout = 30, params SqlParameter[] parameters);
    }

    public class Universal : IUniversal
    {
        private bool isDisposed = false;
        private SqlConnection Connection { get; set; }

        //public string _connectionString = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;

        public const int defaultTimeout = 30;

        public Universal(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }

        public Universal()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL"].ConnectionString);
        }

        public Universal(string server, string database, string password, string user)
        {
            var sb = new SqlConnectionStringBuilder();

            sb.DataSource = server;         // Nome ou o endereço de rede da instância do SQL Server para conexão.
            sb.InitialCatalog = database;   // Nome do banco de dados associado à conexão.
            sb.UserID = user;               // ID de usuário a ser usada ao conectar-se ao SQL Server.
            sb.Password = password;         // Senha para a conta do SQL Server.

            Connection = new SqlConnection(sb.ConnectionString);
        }

        public Universal(string connectionString, string database)
        {
            var sb = new SqlConnectionStringBuilder(connectionString) { InitialCatalog = database };

            Connection = new SqlConnection(sb.ConnectionString);
        }

        ~Universal()
        {
            Dispose(false);
        }

        private async void AbrirConexao()
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
        }

        private async void FecharConexao()
        {
            if (Connection.State == ConnectionState.Open)
                Connection.Close();
        }

        public async Task<int> ExecuteNonQuery(string command, CommandType type = CommandType.Text, params SqlParameter[] parameters)
        {
            return await ExecuteNonQuery(command, type, defaultTimeout, parameters);
        }

        public async Task<object> ExecuteScalar(string command, CommandType type = CommandType.Text, params SqlParameter[] parameters)
        {
            return ExecuteScalar(command, type, defaultTimeout, parameters);
        }

        public async Task<DataTable> ExecuteDataTable(string command, CommandType type = CommandType.Text, params SqlParameter[] parameters)
        {
            return await ExecuteDataTable(command, type, defaultTimeout, parameters);
        }

        public async Task<DataRow> ExecuteDataRow(string command, CommandType type = CommandType.Text, params SqlParameter[] parameters)
        {
            return await ExecuteDataRow(command, type, defaultTimeout, parameters);
        }

        public async Task<int> ExecuteNonQuery(string command, CommandType type = CommandType.Text, int timeout = 30, params SqlParameter[] parameters)
        {
            try
            {
                AbrirConexao();

                using var cmd = new SqlCommand(command, Connection) { CommandType = type, CommandTimeout = timeout };

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public async Task<object> ExecuteScalar(string command, CommandType type = CommandType.Text, int timeout = 30, params SqlParameter[] parameters)
        {
            try
            {
                AbrirConexao();

                using var cmd = new SqlCommand(command, Connection) { CommandType = type, CommandTimeout = timeout };

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public async Task<DataTable> ExecuteDataTable(string command, CommandType type = CommandType.Text, int timeout = 30, params SqlParameter[] parameters)
        {
            try
            {
                AbrirConexao();

                using var cmd = new SqlCommand(command, Connection) { CommandType = type, CommandTimeout = timeout };

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                var dt = new DataTable();

                using var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public async Task<DataRow> ExecuteDataRow(string command, CommandType type = CommandType.Text, int timeout = 30, params SqlParameter[] parameters)
        {
            try
            {
                AbrirConexao();

                using var cmd = new SqlCommand(command, Connection) { CommandType = type, CommandTimeout = timeout };

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                var dt = new DataTable();

                using var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                return dt.Rows[0];
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                // Limpa recursos gerenciados

                Connection?.Dispose();
                Connection = null;
            }

            // Limpa recursos não gerenciados (se houver)

            isDisposed = true;
        }
    }
}
