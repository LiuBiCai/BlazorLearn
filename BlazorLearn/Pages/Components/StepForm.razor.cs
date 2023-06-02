using BlazorLearn.Data;
using BlazorLearn.Entity;
using BlazorLearn.Models;
using BootstrapBlazor.Components;
using Furion.DataEncryption;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Security.Claims;
using System.Xml.Linq;

namespace BlazorLearn.Pages.Form
{
  
    public partial class StepForm
    {


        private Modal? ProofModal { get; set; }
        private string proof1 { get; set; }
        private string proof2 { get; set; }

        private Task<QueryData<OrderEntity>> OnQueryAsync(QueryPageOptions arg)
        {
            try
            {
                var _user = UserEntity.Where(x => x.UserName == Furion.App.User.FindFirstValue(ClaimTypes.Name)).First();
                if (_user == null)
                {
                    return null;
                }
                var orders = OrderEntity.Select.Where(x => x.UserId == _user.Id&&x.CreateTime>DateTime.Now.AddHours(-24)&&x.Status!=OrderStatus.修改前).OrderByDescending(x => x.CreateTime).Count(out var count)
                    .Page(arg.PageIndex, arg.PageItems).ToList();
                foreach(var order in orders)
                {
                    if (order.Status == OrderStatus.暂停)
                    {
                        order.OrderRun = "运行";
                        order.OrderRunColor = Color.Success;
                    }
                    else
                    {
                        order.OrderRunColor = Color.Warning;
                        order.OrderRun = "暂停";
                    }
                }
                return Task.FromResult(new QueryData<OrderEntity>()
                {
                    Items = orders,
                    TotalCount = (int)count
                });
            }
            catch (Exception ex)
            {
                
                return null;
            }
           
        }

        private Task<bool> OnSaveAsync(OrderEntity arg1, ItemChangedType arg2)
        {
            if (arg2 == ItemChangedType.Add)
            {
                
            }
            arg1.Save();
            return Task.FromResult<bool>(true);
        }
        private Task ShowModal(OrderEntity orderEntity)
        {
            _model = orderEntity;
            StateHasChanged();
            return Task.CompletedTask;
        }
       
        private Task ShowModal2(OrderEntity orderEntity)
        {
            if (orderEntity.OrderRun == "暂停")
            {
                if(orderEntity.Status== OrderStatus.正在发货)
                {
                    //orderEntity.OrderRun = "运行";
                    //orderEntity.OrderRunColor = Color.Success;
                    orderEntity.Status = OrderStatus.等待暂停;
                }
                  


                
            }
            else
            {
                if (orderEntity.Status == OrderStatus.暂停)
                {
                    //orderEntity.OrderRun = "运行";
                    //orderEntity.OrderRunColor = Color.Success;
                    orderEntity.Status = OrderStatus.恢复;
                   
                }

                
            }
            orderEntity.Save();

            StateHasChanged();
            return Task.CompletedTask;
        }
        private Task GetProof(OrderEntity orderEntity)
        {
            ProofModal.IsBackdrop = true;

            var proof=OrderProofEntity.Where(x => x.OrderNo == orderEntity.OrderNo).First();
            System.Console.WriteLine(proof.OrderProof1.Length + "," + proof.OrderProof2.Length);
            if (proof!=null)
            {
                proof1 = "data:image/png;base64," + proof.OrderProof1;
                proof2 = "data:image/png;base64," + proof.OrderProof2;
                
                ProofModal.Show();
            }
            
            return Task.CompletedTask;
        }

     



        private int _current;
        public OrderEntity _model = new OrderEntity();
        public void Refresh()
        {
            _model=new OrderEntity();
        }
        public void Next()
        {
            // todo: Not re-rendered
            _current += 1;
            StateHasChanged();
        }

        public void Prev()
        {
            // todo: Not re-rendered
            if (_current <= 0) return;
            _current -= 1;
            StateHasChanged();
        }

        public void First()
        {
            _current = 0;
            StateHasChanged();
        }

        //public List<OrderEntity> Data;
        public IDataService<OrderEntity> Data;
        private Table<OrderEntity>? Table { get; set; }

        public async Task RefreshData()
        {
            try
            {
                await InvokeAsync(Table.QueryAsync);

                // Data = OrderEntity.Select.Where(x => x.UserId == _user.Id&&(DateTime.Now- x.CreateTime).TotalHours<6).OrderByDescending(x => x.CreateTime).ToList();
                // Data. (IDataService<OrderEntity>)OrderEntity.Where(x => x.UserId == _user.Id).OrderByDescending(x => x.CreateTime).ToList();


            }
            catch(Exception ex)
            {

                timer.Dispose();
            }
            
            
              
        }

       
        private System.Threading.Timer? timer; // NOTE: THIS LINE OF CODE ADDED
        protected override async Task OnInitializedAsync()
        {
            // NOTE: THE FOLLOWING CODE ADDED
            timer = new System.Threading.Timer(async (object? stateInfo) =>
            {
                await RefreshData();
                await InvokeAsync(StateHasChanged);// NOTE: MUST CALL StateHasChanged() BECAUSE THIS IS TRIGGERED BY A TIMER INSTEAD OF A USER EVENT

            }, new System.Threading.AutoResetEvent(false), 2000, 30000); // fire every 2000 milliseconds
        }

        public void Dispose() => timer.Dispose();
    }

   
}