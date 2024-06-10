using System.Runtime.Serialization;

namespace SoapService.DataContract
{

    [DataContract]
    public class User
    {
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
    }
}
