using FreeSql;
using FreeSql.DataAnnotations;
using System.ComponentModel;
using System.Security.Principal;

namespace BlazorLearn.Entity
{
    [Description("支付记录表")]
    public class PayLogEnity : BaseEntity<PayLogEnity, int>
    {
        [Description("支付Id")]
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        [Description("用户名")]
        public string? UserName { get; set; }

        [Description("金额")]
        public double? coin { get; set; }


    }
}
