using Entities.Models;
using Entities.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Rabrune.Data;

namespace Data.Repositories
{
    public interface IHomeRepository
    {
        public Task<IEnumerable<HomePageResponse>> Get(int Id);
    }

    public class HomeRepository : IHomeRepository
    {
        private readonly ILogger _logger;
        private readonly IUniversal _repo;

        public HomeRepository(string connectionString)
        {
            _repo = new Universal(connectionString);

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .ClearProviders()
                    .AddConsole();
            });

            _logger = loggerFactory.CreateLogger<HomeRepository>();
        }

        public async Task<IEnumerable<HomePageResponse>> Get(int Id)
        {
            try
            {
                var retorno = new List<HomePageResponse>();

                var result = await _repo.ExecuteDataTable(
                command: "proc_home_get",
                type: CommandType.StoredProcedure,
                new SqlParameter(parameterName: "@p_id", value: Utils.DBNullParse(Id)) 
            );

                if (result.Rows.Count > 0)
                {
                    foreach (DataRow row in result.Rows)
                    {
                        retorno.Add(
                            Parse(row)
                        );
                    }

                    return retorno;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                //_logger.LogError($"LoginRequest", ex.Message, request);
                throw ex;
            }
        }

        private HomePageResponse Parse(DataRow row)
        {
            HomePageResponse model = new HomePageResponse();

            model.PostId = Utils.IntParse(row["Id"]);
            model.PostData = Utils.DateTimeTryParse(row["post_data"]);
            model.PostItem = Utils.StringTryParse(row["post_item"]);

            return model;
        }
    }
}
