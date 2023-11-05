using System.Threading.Tasks;

namespace ProjectX.Services.Api.Application
{
    public interface IApplicationFactory
    {
        Task<dynamic> CallWebService(string endPoint, RequestTypeEnum requestTypeEnum, object obj = default(object));
    }
}
