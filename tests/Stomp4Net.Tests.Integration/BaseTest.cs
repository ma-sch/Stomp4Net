namespace Stomp4Net.Test
{
    using Serilog;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    public class BaseTest
    {
        private const string LogOutputTemplate = "[{Timestamp:yyyy-MM-dd - HH:mm:ss}] [{SourceContext:s}] [{Level:u3}] {Message:lj}{NewLine}{Exception}";

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTest"/> class.
        /// </summary>
        public BaseTest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTest"/> class.
        /// </summary>
        /// <param name="output">The <see cref="ITestOutputHelper"/> for this instance.</param>
        public BaseTest(ITestOutputHelper output)
        {
            this.Output = output as TestOutputHelper;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Xunit(output, outputTemplate: LogOutputTemplate)
                .WriteTo.Console(outputTemplate: LogOutputTemplate)
                .CreateLogger();
        }

        protected TestOutputHelper Output { get; }
    }
}
