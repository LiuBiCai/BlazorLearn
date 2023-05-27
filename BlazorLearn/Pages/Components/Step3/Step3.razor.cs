using BlazorLearn.Entity;
using BlazorLearn.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorLearn.Pages.Form
{
    public partial class Step3
    {
        //private readonly StepFormModel _model = new StepFormModel();
        private OrderEntity _model = new OrderEntity();
        protected override Task OnInitializedAsync()
        {
            _model = StepForm._model;
            return base.OnInitializedAsync();
        }
        [CascadingParameter] public StepForm StepForm { get; set; }
        public void OnFinish()
        {
            StepForm.First();
        }
    }
}