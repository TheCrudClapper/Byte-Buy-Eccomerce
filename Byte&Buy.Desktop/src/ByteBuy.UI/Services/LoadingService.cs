using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ByteBuy.UI.Services;

public interface ILoadingService
{
    Task ShowAsync(string? message = null);
    Task HideAsync();
}

public partial class LoadingService : ObservableObject, ILoadingService
{
    [ObservableProperty]
    private bool _isLoading;
    [ObservableProperty]
    private string? _message;
    private readonly object _lock = new object();
    private CancellationTokenSource? _showCts;
    private static readonly TimeSpan ShowDelay
        = TimeSpan.FromMilliseconds(300);
    private int _pendingOperations = 0;

    public Task ShowAsync(string? message = null)
    {
        CancellationTokenSource cts;

        lock (_lock)
        {
            _pendingOperations++;

            if (_pendingOperations > 1)
                return Task.CompletedTask;

            _showCts = cts = new CancellationTokenSource();
        }

        _ = ShowAfterDelayAsync(cts, cts.Token, message);

        return Task.CompletedTask;
    }

    private async Task ShowAfterDelayAsync(
        CancellationTokenSource cts,
        CancellationToken token,
        string? message)
    {
        try
        {
            await Task.Delay(ShowDelay, token);

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                lock (_lock)
                {
                    if (_pendingOperations > 0 &&
                        !token.IsCancellationRequested &&
                        ReferenceEquals(_showCts, cts))
                    {
                        Message = message;
                        IsLoading = true;
                    }
                }
            });
        }
        catch (OperationCanceledException)
        {
            // Operation ended before delay
            // We do nothing, just catch 
        }
    }

    public async Task HideAsync()
    {
        CancellationTokenSource? cts;

        lock (_lock)
        {
            if (_pendingOperations <= 0)
                return;

            _pendingOperations--;

            if (_pendingOperations > 0)
                return;

            cts = _showCts;
            _showCts = null;
        }

        cts?.Cancel();
        cts?.Dispose();

        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            lock (_lock)
            {
                if (_pendingOperations == 0)
                    IsLoading = false;
            }
        });
    }
}
