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





            error = "";

            //_model.Save();
            StepForm.Next();
            return Task.FromResult(true);
        }
    }
}