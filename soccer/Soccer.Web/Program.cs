using Hangfire;
using Hangfire.SqlServer;
using NLog;
using NLog.Web;
using Soccer.Common.Utils;
using Soccer.Service.Interface;
using Soccer.Repository.Interface;
using Soccer.Repository.Implementaion;
using Soccer.Service.Implementation;
using Hangfire.Dashboard.BasicAuthorization;

// Early init of NLog to allow startup and exception logging, before host is built
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    // Add hangfire to the container.
    builder.Services.AddHangfire(configuration => configuration
           .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
           .UseSimpleAssemblyNameTypeSerializer()
           .UseRecommendedSerializerSettings()
           .UseSqlServerStorage(builder.Configuration.GetConnectionString("matchResultDb"), new SqlServerStorageOptions
           {
               CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
               SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
               QueuePollInterval = TimeSpan.Zero,
               UseRecommendedIsolationLevel = true,
               DisableGlobalLocks = true
           }));
    builder.Services.AddHangfireServer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<IMatchResultRepository, MatchResultRepository>();
    builder.Services.AddScoped<IMatchResultService, MatchResultService>();
    builder.Services.AddScoped<IMatchResultBuilder, MatchResultBuilder>();
    builder.Services.AddSingleton<BaseRepository>();
    builder.Services.AddScoped<ISBOMatchResultRepository, SBOMatchResultRepository>();
    builder.Services.AddScoped<ISBOMatchResultService, SBOMatchResultService>();
    builder.Services.AddScoped<ISBOMatchResultBuilder, SBOMatchResultBuilder>();
    builder.Services.AddScoped<ICookiesRepository, CookiesRepository>();
    builder.Services.AddScoped<ICookiesService, CookiesService>();


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    // use swagger
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseHangfireDashboard("/dashboard", new DashboardOptions
    {
        Authorization = new[] {new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
        {
            RequireSsl = false,
            SslRedirect = false,
            LoginCaseSensitive = true,
            Users = new[]
            {
                new BasicAuthAuthorizationUser
                {
                    Login = builder.Configuration["Hangfire:UserName"],
                    PasswordClear = builder.Configuration["Hangfire:Password"]
                }
            }
        })},
        IsReadOnlyFunc = (context) => false
    });

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // schedule job
    RecurringJob.AddOrUpdate<BTISportCrawlerScheduler>(x => x.UpdateResultDetailHistoryTable(), "*/03 * * * *");
    RecurringJob.AddOrUpdate<SBOSportCrawlerScheduler>(x => x.UpdateResultDetailHistoryTable(), "*/03 * * * *");
    RecurringJob.AddOrUpdate<SBOCookiesUpdateScheduler>(x => x.UpdateSBOCookies(), "0 0 * * *");  //update at 12:AM

    app.Run();
}
catch(Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}