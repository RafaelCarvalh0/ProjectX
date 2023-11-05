using Entities.Models;
using Entities.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _repo;

        public LoginController(IOptions<Models.ConnectionStrings> config, ILogger<LoginController> logger, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _repo = new UserRepository(config.Value.AWS);
        }

        // Api/Login/Get
        ///// <summary>
        ///// Verifica se existe um usuário na base de dados, baseado nos parâmetros da requisição
        ///// Retorna um boolean sobre o repsonse.
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        ///// 
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<bool>> Get(LoginRequest request)
        {
            try
            {
                var retorno = await _repo.Get(request);

                if(retorno == null)
                {
                    return new EmptyResult();
                }

                return retorno; 
            }
            catch (Exception ex)
            {
                _logger.LogError($"LoginRequest", request);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Auth/Create
        /// <summary>
        /// Insere um usuário com base nos dados informados na requisição.
        /// </summary>
        /// <param name="request">Filtros</param>
        /// <returns>Não retorna nada</returns>
        /// <response code="200">Operação realizada com exito</response>
        /// <response code="500">Ocorreu um erro interno</response>
        /// <response code="401">Usuário não autenticado</response> 
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<bool>> Create(SigninRequest request)
        {
            try
            {
                var retornos = await _repo.Create(request);

                if (retornos == null)
                {
                    return new EmptyResult();
                }

                return Ok(retornos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[HttpPost] Get(request.Id: {request.Id})",
                    request.Id);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[HttpPut]
        //[Route("[action]")]
        //public async Task<bool> Update(LoginRequest request)
        //{
        //    try
        //    {
        //        //var alunos = _repo.GetAlunos();
        //        var retorno = _repo.GetAlunos(); //_repo.Create(request);

        //        if (retorno != null)
        //        {
        //            return await Task.FromResult(true);
        //        }

        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        // POST: api/Auth/Login
        /// <summary>
        /// Verifica se um usuário possui cadastro na base de dados.
        /// </summary>
        /// <param name="request">Filtros</param>
        /// <returns>Não retorna nada</returns>
        /// <response code="200">Operação realizada com exito</response>
        /// <response code="500">Ocorreu um erro interno</response>
        /// <response code="401">Usuário não autenticado</response> 
        //[HttpPost]
        //[Route("[action]")]
        //public async Task<ActionResult<bool>> Login(LoginRequest request)
        //{
        //    try
        //    {
        //        var retornos = await _repo.Create(request);

        //        if (retornos == null)
        //        {
        //            return new EmptyResult();
        //        }

        //        return Ok(retornos);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "[HttpPost] Get(request.Id: {request.Id})",
        //            request.Id);

        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
    }
}
