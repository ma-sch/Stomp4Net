namespace Stomp4Net.Sample
{
    using System;
    using Serilog;

    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(outputTemplate:
                "[{Timestamp:yyyy-MM-dd - HH:mm:ss}] [{SourceContext:s}] [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

            Log.Information("Stomp4Net Example started");

            var stompSample = new StompSample();
            stompSample.StartServer();
            stompSample.StartClient();

            Console.ReadKey();
        }
    }
}
