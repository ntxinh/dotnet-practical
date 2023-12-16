using System.Runtime.Serialization;

namespace AST.Core.Common.ResponseBuilder
{
    [DataContract]
    public class ApiResponse<T>
    {
        [DataMember]
        public string Version { get; set; }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember]
        public string RequestId { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public T Result { get; set; }

        [DataMember]
        public string DebugMessage { get; set; }
    }
}
