using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetMusicApi.Services;

public class TimedTokenService : IHostedService, IDisposable
{
    private readonly ILogger<TimedTokenService> _logger;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;
    private Timer _timer;
    
    public TimedTokenService(ILogger<TimedTokenService> logger,
        IHttpClientFactory clientFactory, 
        IConfiguration configuration)
    {
        _logger = logger;
        _clientFactory = clientFactory;
        _configuration = configuration;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timed token service is running");

        _timer = new Timer(Generate, null, TimeSpan.Zero, TimeSpan.FromMinutes(45));

        return Task.CompletedTask;
    }

    private void Generate(object? state)
    {
        // ...
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timed token service is stopping");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}