using Microsoft.AspNetCore.Components;
using SnackbarBlazor.Services;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace SnackbarBlazor.Components
{
    public partial class SnackbarComponent : ComponentBase
    {
        [Parameter]
        public string Header { get; set; }

        [Parameter]
        public string Body { get; set; }

        [Inject]
        public ISnackbarService SnackbarService { get; set; }

        public bool IsVisible { get; set; }

        private Timer hideTimer;

        protected override void OnInitialized()
        {
            hideTimer = new Timer(TimeSpan.FromSeconds(10).TotalMilliseconds);
            hideTimer.Elapsed += HideTimer_Elapsed;
            hideTimer.Enabled = true;
            SnackbarService.Show += SnackbarService_Show;
        }

        private async void SnackbarService_Show(object sender, EventArgs e)
        {
            await Show();
        }

        private async void HideTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (IsVisible)
            {
                await Hide();
            }
        }

        public async Task Show()
        {
            IsVisible = true;
            hideTimer.Start();
            await InvokeAsync(StateHasChanged);
        }

        public async Task Hide()
        {
            IsVisible = false;
            await InvokeAsync(StateHasChanged);
        }
    }
}
