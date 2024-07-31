namespace BeeCamp.Api;

public static class Setup
{
    public static WebApplication RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddMediatR(req =>
        {
            req.RegisterServicesFromAssemblyContaining<Program>();
        });

        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();

        builder.Services.AddHttpContextAccessor();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        //superbase
        builder.Services.AddScoped<Client>(_ =>
            new Client(
                builder.Configuration["projectUrl"] ?? string.Empty, 
                builder.Configuration["publicKey"] ?? string.Empty,
                new SupabaseOptions
                {
                    AutoRefreshToken = true,
                    AutoConnectRealtime = true
                }));
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Open", b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        builder.Services.AddCarter();
        
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        
        return builder.Build();
    }

    public static WebApplication RegisterPipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseExceptionHandler();

        app.UseHttpsRedirection();
        
        app.UseAuthentication();

        app.UseCors("Open");

        app.UseAuthorization();

        app.MapCarter();

        return app;
    }
}