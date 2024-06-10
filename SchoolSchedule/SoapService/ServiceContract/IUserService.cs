using SoapService.DataContract;
using System.ServiceModel;

namespace SoapService.ServiceContract
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        public string RegisterUser(User user);
    }
}
