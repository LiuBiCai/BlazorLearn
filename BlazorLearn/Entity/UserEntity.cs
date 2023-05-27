using System.ComponentModel;
using FreeSql;
using FreeSql.DataAnnotations;

namespace BlazorLearn.Entity;

[Description("用户信息表")]
public class UserEntity : BaseEntity<UserEntity, int>
{
    [Description("用户Id")]
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Id { get; set; }
    [Description("用户名")]
    public string? UserName { get; set; }

    [Description("密码")]
    public string? Password { get; set; }

    [Description("用户姓名")]
    public string? Name { get; set; }

    [Description("角色Id")]
    public int RoleId { get; set; }

    [Description("角色")]
    [Navigate(nameof(RoleId))]
    public RoleEntity? Role { get; set; }

    [Description("价格")]
    public int price { get; set; }

    [Description("已支付")]
    public int payed { get; set; }



}