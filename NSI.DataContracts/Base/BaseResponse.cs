using NSI.Common.Enumerations;

namespace NSI.DataContracts.Base
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public ResponseStatus Success { get; set; }
    }
}
