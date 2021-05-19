using System;
using System.Threading.Tasks;

namespace SnackbarBlazor.Services
{
    public interface ISnackbarService
    {
        Task ShowAsync();
        event EventHandler Show;
    }
}
