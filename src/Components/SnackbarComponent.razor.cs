using Microsoft.AspNetCore.Components;
using SnackbarBlazor.Services;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace SnackbarBlazor.Components
{
    public partial class SnackbarComponent : ComponentBase, IDisposable
    {
        [Parameter]
        public string Header { get; set; }

        [Parameter]
        public string Body { get; set; }

        [Parameter]
        public int Seconds { get; set; } = 5;

        [Inject]
        public ISnackbarService SnackbarService { get; set; }

        public bool IsVisible { get; set; }

        private Timer hideTimer;

        protected override void OnInitialized()
        {
            hideTimer = new Timer(TimeSpan.FromSeconds(Seconds).TotalMilliseconds);
            hideTimer.Elapsed += HideTimer_Elapsed;
            SnackbarService.Show += SnackbarService_Show;
        }

        private async void SnackbarService_Show(object sender, EventArgs e)
        {
            await Show();
        }

        private void HideTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (IsVisible)
            {
                _ = Hide();
            }
        }

        public async Task Show()
        {
            IsVisible = true;
            hideTimer.Enabled = true;
            hideTimer.Start();
            await InvokeAsync(StateHasChanged);
        }

        public async Task Hide()
        {
            IsVisible = false;
            hideTimer.Stop();
            hideTimer.Enabled = false;
            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            hideTimer.Stop();
            hideTimer.Enabled = false;
            hideTimer = null;
        }
    }
}
