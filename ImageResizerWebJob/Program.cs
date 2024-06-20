using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


var builder = new HostBuilder()
    .ConfigureWebJobs(b =>
    {
        b.AddAzureStorageBlobs();
        b.AddAzureStorageQueues();
    })
    .ConfigureLogging((context, b) =>
    {
        b.AddConsole();
        var instrumentationKey = context.Configuration["ApplicationInsights:InstrumentationKey"];
        if (!string.IsNullOrEmpty(instrumentationKey))
        {
            b.AddApplicationInsights(instrumentationKey);
        }
    })
    .Build();

using (builder)
{
    builder.Run();
}