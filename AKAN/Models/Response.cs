namespace AKAN.Models
{
    public class Response
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public object Error { get; set; }

        public Response(bool success, object data, object error)
        {
            this.Success = success;
            this.Data = data;
            this.Error = error;
        }
    }
}
