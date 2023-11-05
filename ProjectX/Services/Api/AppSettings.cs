using Rabrune.Services.Authentication;
using System.Security.Claims;

namespace ProjectX.Services.Api
{
    public class AppSettings
    {
        //aqui
        public static Uri Caminho => new Uri("https://localhost:7100/");
        //public static Uri Caminho => new Uri("http://rabrune.brazilsouth.cloudapp.azure.com:5001/");
        public static IAuthenticationFactory Aut { get; set; }
        public static string Acesso { get; set; }
        public static ClaimsIdentity Diretivas { get; set; }
        public static string Validar { get { return "dsadmin@Rabrune.local"; } }
        public static string EnderecoEnvio { get { return "http://localhost:49500"; } }
        //public static string EnderecoEnvio { get { return "http://rabrune.brazilsouth.cloudapp.azure.com:5001/"; } }
        public static string EnderecoPublico { get { return "http://localhost:49500"; } }
        //public static string EnderecoPublico { get { return "http://rabrune.brazilsouth.cloudapp.azure.com:5001/"; } }
        public static int? CodigoUsuario { get; set; }
        public DateTime HoraSistema { get; set; }
        public static string NomeUsuario { get; set; }
    }
}
