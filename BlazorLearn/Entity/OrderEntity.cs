using System.ComponentModel;
using BootstrapBlazor.Components;
using FreeSql;
using FreeSql.DataAnnotations;

namespace BlazorLearn.Entity;


[Description("订单表")]
public class OrderEntity : BaseEntity<OrderEntity, int>
{
    [Description("订单Id")]
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Id { get; set; }
    [Description("订单号")]
    public string? OrderNo { get; set; }
    [Description("订单金额")]
    public decimal Amount { get; set; }
    [Description("订单状态")]
    public OrderStatus Status { get; set; }
    [Description("用户Id")]
    public int UserId { get; set; }
    [Description("用户名")]
    public string UserName { get; set; }
    [Description("用户")]
    [Navigate(nameof(UserId))]
    public UserEntity? User { get; set; }
    [Description("订单详情")]
    [Navigate(nameof(OrderDetailEntity.OrderId))]
    public List<OrderDetailEntity>? OrderDetails { get; set; }
    [Description("订单信息")]
    public string OrderInfo { get; set; }
    [Description("EA邮箱")]
    public string EaEmail { get; set; }
    [Description("EA密码")]
    public string EaPass { get; set; }
    [Description("EA俱乐部名")]
    public string EaClub { get; set; }
    [Description("EA备份码")]
    public string EaBackCode { get; set; }
    [Description("订单总量(K)")]
    public int OrderAmount { get; set; }
    [Description("订单金额(元)")]
    public int OrderAmountYuan { get; set; }
    [Description("订单平台")]
    public string OrderPlatform { get; set; }
    [Description("订单备注")]
    public string OrderRemark { get; set; }
    [Description("单价")]
    public int OrderPrice { get; set; } = 1;
    [Description("单价总价")]
    public string OrderCost { get; set; }
    [Description("装货进度")]
    public string OrderProcess { get; set; }

    [Description("运行")]
    public string OrderRun { get; set; } = "暂停";
    [Description("运行颜色")]
    public Color OrderRunColor { get; set; }=Color.Warning;

}



public enum OrderStatus
{
    [Description("等待开始")]
    等待开始,
    [Description("正在发货")]
    正在发货,
    [Description("已完成")]
    已完成,
    [Description("已关闭")]
    已关闭,
    [Description("正在修改")]
    正在修改,
    [Description("等待暂停")]
    等待暂停,
    [Description("正在暂停")]    
    正在暂停,
    [Description("暂停运行")]
    暂停,
    [Description("恢复运行")]
    恢复,
}

public class OrderDetailEntity
{
    [Description("订单详情Id")]
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Id { get; set; }
    [Description("订单Id")]
    public int OrderId { get; set; }
    [Description("订单")]
    [Navigate(nameof(OrderId))]
    public OrderEntity? Order { get; set; }
    [Description("商品Id")]
    public int ProductId { get; set; }    
    [Description("商品数量")]
    public int Quantity { get; set; }
    [Description("商品单价")]
    public decimal UnitPrice { get; set; }
}   

