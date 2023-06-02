using AntDesign;
using BlazorLearn.Entity;
using BlazorLearn.Models;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace BlazorLearn.Pages.Form
{
    public partial class Step1
    {

        //private readonly StepFormModel _model = new StepFormModel();
        [Parameter]
        public OrderEntity _model { get; set; }
        private string error = "";
        protected override Task OnInitializedAsync()
        {
            _model = StepForm._model;
            return base.OnInitializedAsync();
        }


        private readonly FormItemLayout _formLayout = new FormItemLayout
        {
            WrapperCol = new ColLayoutParam
            {
                Xs = new EmbeddedProperty { Span = 24, Offset = 0 },
                Sm = new EmbeddedProperty { Span = 19, Offset = 5 },
            }
        };

        [CascadingParameter] public StepForm StepForm { get; set; }

       

        private Task<bool> OnValidateForm()
        {
            var _user = UserEntity.Where(x => x.UserName == Furion.App.User.FindFirstValue(ClaimTypes.Name)).First();
            if (_user == null)
            {
                return Task.FromResult(false);
            }
            if(_user.Id == 1) 
            {
                if (string.IsNullOrEmpty(_model.EaEmail)&& _model.OrderAmount != 0)
                {
                    UserEntity.Where(x=>x.Id>0).ToList().ForEach(x =>
                    {
                        x.price = _model.OrderAmount;
                        x.Save();
                    });
                }
                 
            }
            _model.Id = 0;
        
            if (string.IsNullOrEmpty(_model.EaEmail))
            {
               error = "EA账号不可为空";
                return Task.FromResult(false);
            }
            if (!_model.EaEmail.Contains("@")||!_model.EaEmail.Contains("."))
            {
                error = "EA账号不符合格式";
                return Task.FromResult(false);
            }
            _model.EaEmail = _model.EaEmail.ToLower();
            if (string.IsNullOrEmpty(_model.EaPass))
            {
                error = "EA密码不可为空";
                return Task.FromResult(false);
            }
                   
            if(_model.OrderAmount==0)
            {
                error = "订单量不可为0";
                return Task.FromResult(false);
            }
            var eaCodes = _model.EaBackCode.Split(";");
            foreach(var code in eaCodes)
            {
                if (string.IsNullOrEmpty(code))
                {
                    error = "EA备份码不可为空";
                    return Task.FromResult(false);
                }
                if (code.Length<6||code.Length>16)
                {
                    error = "EA备份码不符合格式";
                    return Task.FromResult(false);
                }
            }

            _model.CreateTime= DateTime.Now;
            _model.OrderIdBefore = 0;
            _model.OrderNo ="";
            _model.OrderRemark = string.Empty;
            _model.Amount = 0;
            _model.Status = 0;
          
          
            error = "";

            //_model.Save();
            StepForm.Next();
            return Task.FromResult(true);
        }
        private Task<bool> ChangeOrder()
        {
            if(_model.Status==OrderStatus.等待修改)
            {
                error = "订单修改中";
                return Task.FromResult(false);
            }




            var order = OrderEntity.Where(x => x.Id == _model.Id).First();
            if (order == null)
            {
                error = "订单不存在";
                return Task.FromResult(false);
            }
            if(order.EaEmail!=_model.EaEmail)
            {
                error = "ea账号不可更改 请重新下单";
                return Task.FromResult(false);
            }


            order.Status= OrderStatus.修改前;
            order.OrderRemark = "";
            order.SaveAsync();
           
            _model.OrderIdBefore = order.Id;
            _model.Id = 0;
            if (string.IsNullOrEmpty(_model.EaEmail))
            {
                error = "EA账号不可为空";
                return Task.FromResult(false);
            }
            if (!_model.EaEmail.Contains("@") || !_model.EaEmail.Contains("."))
            {
                error = "EA账号不符合格式";
                return Task.FromResult(false);
            }

            _model.EaEmail = _model.EaEmail.ToLower();
            if (string.IsNullOrEmpty(_model.EaPass))
            {
                error = "EA密码不可为空";
                return Task.FromResult(false);
            }

            if (_model.OrderAmount == 0)
            {
                error = "订单量不可为0";
                return Task.FromResult(false);
            }
            var eaCodes = _model.EaBackCode.Split(";");
            foreach (var code in eaCodes)
            {
                if (string.IsNullOrEmpty(code))
                {
                    error = "EA备份码不可为空";
                    return Task.FromResult(false);
                }
                if (code.Length < 6 || code.Length > 16)
                {
                    error = "EA备份码不符合格式";
                    return Task.FromResult(false);
                }
            }

         


            error = "";
            _model.Status = OrderStatus.等待修改;
            _model.OrderAmountYuan = _model.OrderAmount * _model.OrderPrice / 10;
            _model.OrderCost = _model.OrderPrice + "|" + _model.OrderAmountYuan;
            _model.OrderProcess = _model.Amount + "|" + _model.OrderAmount;
            _model.Save();

            return Task.FromResult(true);
        }
    }
}