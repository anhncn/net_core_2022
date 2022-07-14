namespace Domain.Model
{
    public class ResponseResultModel
    {
        public object Data { get; set; }

        private ResponseResultModel() { }

        public static ResponseResultModel Instance(object data = null)
        {
            return new ResponseResultModel() { Data = data };
        }
    }
}
