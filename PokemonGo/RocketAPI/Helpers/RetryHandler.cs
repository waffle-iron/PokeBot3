#region

using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace PokemonGo.RocketAPI.Helpers
{
    internal class RetryHandler : DelegatingHandler
    {
        private const int MaxRetries = 100000;

        public RetryHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            for (var i = 0; i <= MaxRetries; i++)
            {
                try
                {
                    var response = await base.SendAsync(request, cancellationToken);
                    if (response.StatusCode == HttpStatusCode.BadGateway)
                        throw new Exception(); //todo: proper implementation

                    return response;
                }
                catch (TaskCanceledException timeoutEx)
                {
                    if (i > 4)
                    {
                        Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Request to {request.RequestUri} timed out or was cancelled too many times. Throwing out the world!");
                        var fileName = System.Reflection.Assembly.GetEntryAssembly().Location;
                        System.Diagnostics.Process.Start(fileName);
                        Environment.Exit(1);
                    }
                    else
                    {
                        Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Request to {request.RequestUri} timed out or was cancelled. Retrying!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"Got an error: {ex.Message}");
                    Console.WriteLine(
                        $"[{DateTime.Now.ToString("HH:mm:ss")}] [#{i} of {MaxRetries}] retry request {request.RequestUri}");
                    if (i < MaxRetries)
                    {
                        await Task.Delay(1000, cancellationToken);
                        continue;
                    }
                    throw;
                }
            }
            return null;
        }
    }
}