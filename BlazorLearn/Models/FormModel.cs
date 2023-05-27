namespace BlazorLearn.Models
{
    public class StepFormModel
    {
        public string ReceiverAccountType { get; set; } = "ant-design@alipay.com";
        public string ReceiverAccount { get; set; } = "test@example.com";
        public string ReceiverName { get; set; } = "Alex";
        public string PayAccount { get; set; }
        public string Password { get; set; } = "500";
        public string Amount { get; set; } = "12345678";
        public string OrderInfo { get; set; }
        public string EaEmail { get; set; }
        public string EaPass { get; set; }
        public string EaBackCode { get; set; }
        public int OrderAmount { get; set; }
        public string OrderAmountYuan { get; set; }
        public string OrderPlatform { get; set; }
    }
    public class BasicFormModel
    {
        public string Title { get; set; }
        public string Client { get; set; }
        public string Invites { get; set; }
        public int Disclosure { get; set; }
        public int Weight { get; set; }
        public string Standard { get; set; }
        public string Goal { get; set; }
        public DateTime?[] DateRange { get; set; }

        public string OrderInfo { get; set; }
        public string EaEmail { get; set; }
        public string EaPass { get; set; }
        public string EaBackCode { get; set; }
        public string OrderAmount { get; set; }
        public string OrderPlatform { get; set; }
    }
}
