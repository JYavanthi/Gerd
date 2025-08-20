namespace Gred.Data.Entities.Common
{
    public class CommonRsult
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
        public object Data { get; set; }

    public bool Success { get; set; }
  }
}
