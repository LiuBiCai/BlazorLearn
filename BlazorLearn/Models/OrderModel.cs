namespace BlazorLearn.Models
{
    public class OrderModels
    {
        public OrderModel[] OrderList { get; set; }
    }

    public class OrderModel
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Num { get; set; }
        public string Amount { get; set; }
       
        public string Time { get; set; }
        public string Rate { get; set; }
        public string Status { get; set; }
        public string Operator { get; set; }
        public string Cost { get; set; }
        public string OrderInfo { get; set; }
        public string EaEmail { get; set; }
        public string EaPass { get; set; }
        public string EaBackCode { get; set; }
        public int OrderAmount { get; set; }
        public string OrderAmountYuan { get; set; }
        public string OrderPlatform { get; set; }
    }
}
