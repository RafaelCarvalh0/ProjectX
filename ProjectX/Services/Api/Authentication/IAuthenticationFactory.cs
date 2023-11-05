using Microsoft.Azure.AppService.ApiApps.Service;
using ProjectX.Models.Authentication;
using System.Threading.Tasks;

namespace Rabrune.Services.Authentication {
    public interface IAuthenticationFactory
    {
        Task<TokenResult> Login(UserLogin user);
    }
}