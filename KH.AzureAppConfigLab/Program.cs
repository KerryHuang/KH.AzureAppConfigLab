using Azure.Data.AppConfiguration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 添加 Azure App Configuration

var connectionString = builder.Configuration["AppConfig:ConnectionString"];

builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(connectionString)
           .Select(KeyFilter.Any, LabelFilter.Null)   // 選擇所有配置
           .ConfigureRefresh(refresh =>
           {
               refresh.Register("AppSettings:MySetting", refreshAll: true)
                      .SetRefreshInterval(TimeSpan.FromSeconds(300));
           });
});

// 注入 ConfigurationClient 實例
builder.Services.AddSingleton<ConfigurationClient>(provider =>
{
    return new ConfigurationClient(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
