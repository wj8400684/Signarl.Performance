namespace RpcServer.Abp;

internal sealed class PackageHostServer : BackgroundService
{
    internal static int PackageCount;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var lastCount = PackageCount;
            await Task.Delay(1000);
            var nowCount = PackageCount;

            await Console.Out.WriteLineAsync($"session count : {1} | package : {nowCount-lastCount}/s");
        }
    }
}
