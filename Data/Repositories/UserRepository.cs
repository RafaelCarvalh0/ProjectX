using Entities.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Repositories
{
    public interface IUserRepository
    {
        public Task<bool?> Create(SigninRequest request);
        public Task<LoginResponse> Get(LoginRequest request);
        //public Task<bool> Update(LoginRequest request);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ILogger _logger;
        private readonly IUniversal _repo;

        public UserRepository(string connectionString)
        {
            _repo = new Universal(connectionString);

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .ClearProviders()
                    .AddConsole();
            });

            _logger = loggerFactory.CreateLogger<UserRepository>();
        }

        public UserRepository()
        {
            //_repo = new();
        }

        public async Task<bool?> Create(SigninRequest request)
        {
            try
            {
                var result = await _repo.ExecuteNonQuery(
                command: "proc_adicionar_usuario",
                type: CommandType.StoredProcedure,
                new SqlParameter() { ParameterName = "@p_id", Value = request.Id, SqlDbType = SqlDbType.Int },
                new SqlParameter() { ParameterName = "@p_name", Value = request.Name, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@p_account", Value = request.User, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@p_password", Value = request.Password, SqlDbType = SqlDbType.VarChar }
            );

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"LoginRequest", ex.Message, request);
            }
            return null;
        }

        public async Task<LoginResponse> Get(LoginRequest request)
        {
            try
            {
                var model = new LoginResponse();

                var result = await _repo.ExecuteDataTable(
                command: "proc_login",
                type: CommandType.StoredProcedure,
                new SqlParameter() { ParameterName = "@p_account", Value = request.User, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@p_password", Value = request.Password, SqlDbType = SqlDbType.VarChar }
            );

                if (result.Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(result.Rows[0].ItemArray[0].ToString());
                    return model;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"LoginRequest", ex.Message, request);
            }
            return null;
        }

        //public MenuModel Add(MenuModel request)
        //{
        //    MenuModel model = null;

        //    var result = _repo.ExecuteScalar(
        //        command: "proc_menu_post",
        //        type: CommandType.StoredProcedure,
        //        new SqlParameter() { ParameterName = "@p_mnu_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int },
        //        new SqlParameter() { ParameterName = "@p_mnu_descricao", Value = request.descricao, SqlDbType = SqlDbType.VarChar },
        //        new SqlParameter() { ParameterName = "@p_mnu_perfil", Value = request.perfil, SqlDbType = SqlDbType.VarChar }
        //        //new SqlParameter() { ParameterName = "@p_mnu_fixarMenu", Value = request.fixarMenu, SqlDbType = SqlDbType.VarChar }
        //    );

        //    model = Get(Convert.ToInt32(result));

        //    return model;
        //}
    }
}
