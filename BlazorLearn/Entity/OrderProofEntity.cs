using FreeSql;
using FreeSql.DataAnnotations;
using System.ComponentModel;
using System.Data;
using System.Security.Principal;

[Description("订单证据表")]
public class OrderProofEntity : BaseEntity<OrderProofEntity, int>
{
    [Description("订单Id")]
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Id { get; set; }
    [Description("订单号")]
    public string? OrderNo { get; set; }
    [Description("完成凭证1")]
    [Column(DbType = "varchar(max)")]
    public string? OrderProof1 { get; set; }
    [Description("完成凭证2")]
    [Column(DbType = "varchar(max)")]
    public string? OrderProof2 { get; set; }
}