using System;
using System.Threading.Tasks;

namespace SnackbarBlazor.Services
{
    public class SnackbarService : ISnackbarService
    {
        public event EventHandler Show;

        public async Task ShowAsync()
        {
            Show.Invoke(null, null);
        }
    }
}
