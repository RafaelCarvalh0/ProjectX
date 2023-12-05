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
    public class AuthController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _repo;

        public AuthController(IOptions<Models.ConnectionStrings> config, ILogger<AuthController> logger, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _repo = new UserRepository(config.Value.AWS);
        }

        // Api/Auth/Login
        ///// <summary>
        ///// Verifica se existe um usuário na base de dados, baseado nos parâmetros da requisição
        ///// Retorna um boolean sobre o repsonse.
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        ///// 
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            try
            {
                var retorno = await _repo.GetLogin(request);

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

        // GET: api/Auth/Logout/user_id
        // Api/Auth/Logout
        ///// <summary>
        ///// Verifica se existe um usuário logado na base de dados, baseado no id da requisição
        ///// Não retorna nada.
        ///// </summary>
        ///// <param name="user_id"></param>
        ///// <returns></returns>
        ///// 
        [HttpGet]
        [Route("[action]/{user_id}")]
        public async Task<ActionResult<bool?>> Logout(int user_id)
        {
            try
            {
                var retorno = await _repo.GetLogout(user_id);

                if (retorno == null)
                {
                    return new EmptyResult();
                }

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                _logger.LogError($"User_id", user_id);

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
