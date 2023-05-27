using AntDesign;
using BlazorLearn.Entity;
using BlazorLearn.Models;
using BlazorLearn.Pages.Admin;
using BlazorLearn.Pages.Form;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace BlazorLearn.Pages.Form
{
    public partial class Step2
    {
        //private readonly StepFormModel _model = new StepFormModel();
        [Parameter]        
        public OrderEntity _model { get; set; }
        protected override Task OnInitializedAsync()
        {
            _model = StepForm._model;
            var _user = UserEntity.Where(x => x.UserName == Furion.App.User.FindFirstValue(ClaimTypes.Name)).First();            
            _model.UserId = _user.Id;
            _model.UserName = _user.Name;
            _model.OrderPrice= _user.price;
            _model.OrderAmountYuan = _model.OrderAmount * _model.OrderPrice/10;
            _model.OrderCost = _model.OrderPrice + "|" + _model.OrderAmountYuan;
            _model.OrderProcess = _model.Amount + "|"+_model.OrderAmount;
            _model.OrderRun = "暂停";
            _model.OrderRunColor = BootstrapBlazor.Components.Color.Danger;
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

        public void OnValidateForm()
        {
            
            _model.Save();
            StepForm.Refresh();
            //StepForm.RefreshData();
            StepForm.Next();
        }

        public void Preview()
        {
            StepForm.Prev();
        }

        
    }
}