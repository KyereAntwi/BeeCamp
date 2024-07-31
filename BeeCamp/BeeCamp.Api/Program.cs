Log.Logger = new LoggerConfiguration()
    .WriteTo.Console().CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
    .WriteTo.Console()
    .ReadFrom.Configuration(context.Configuration));

var app = builder.RegisterServices().RegisterPipeline();

app.UseSerilogRequestLogging();

app.Run();
