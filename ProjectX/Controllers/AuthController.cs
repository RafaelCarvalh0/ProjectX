using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectX.Models;
using ProjectX.Services.Client;
using System.Diagnostics;

namespace ProjectX.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ClientHelper _client = new ClientHelper();

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index");
                }

                ClientHelper _client = new();
                var json = await _client.CallWebService("Api/Auth/Login", ClientHelper.RequestType.POST, request);
                var retorno = new LoginResponse();

                if (json != null)
                {
                    retorno = JsonConvert.DeserializeObject<LoginResponse>(json.ToString());
                    ViewData["LoggedUser"] = "Logado";
                    ViewData["Alert"] = "Bem-Vindo";

                    return View("~/Views/Home/Index.cshtml", retorno);
                }
                else
                {
                    TempData["ErrorMessage"] = "Usuário e/ou senha inválidos";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST: Auth/Index (User: {0}, Password: {1})", request.User, request.Password);

                //ViewData["ErrorMessage"] = "Erro!";
                //ViewData["ErrorDetails"] = "Ocorreu um erro na resposta do serviço!";
            }

            return View("index");
        }

        [HttpGet]
        [Route("[action]/{user_id}")]
        public async Task<ActionResult> Logout(int user_id)
        {
            try
            {
                if (user_id > 0)
                {
                    ClientHelper _client = new();
                    var json = await _client.CallWebService($"Api/Auth/Logout/{user_id}", ClientHelper.RequestType.GET);
                    var retorno = new LoginResponse();

                    if (json != null)
                    {
                        ViewData["LoggedUser"] = "Deslogado";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST: Auth/Logout (User_id: {0})", user_id);

                //ViewData["ErrorMessage"] = "Erro!";
                //ViewData["ErrorDetails"] = "Ocorreu um erro na resposta do serviço!";
            }

            return View("index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(SigninRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Register");
                }

                var retorno = await _client.CallWebService("Api/Auth/Create", ClientHelper.RequestType.POST, request);

                if (retorno != null)
                {
                    return View("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Este e-mail já esta cadastrado, por favor tente outro!";
                    return View("Register");
                }
                //else
                //    return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST: Auth/Index (Id: {0}, Nome: {1})", request.Id, request.Name);

                //ViewData["ErrorMessage"] = "Erro!";
                //ViewData["ErrorDetails"] = "Ocorreu um erro na resposta do serviço!";
            }

            return View();
        }
    }
}
