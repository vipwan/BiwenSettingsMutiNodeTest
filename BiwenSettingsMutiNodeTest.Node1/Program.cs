using BiwenSettingsMutiNodeTest.Shared.Settings;
using Biwen.Settings;
using BiwenSettingsMutiNodeTest.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseInMemoryDatabase("BiwenSettingsMutiNodeTest");
});


builder.Services.AddBiwenSettings(o =>
{

    //~/biwen-settings
    o.Route = "biwen-settings";

    o.MapNotifyEndpoint = true;
    o.ApiPrefix = "biwen-settings/api";


    o.UseCacheOfMemory();
    o.AutoFluentValidationOption.Enable = true;
    o.UseEncryption<Biwen.Settings.Encryption.EmptyEncryptionProvider>();
    o.ProjectId = "BiwenSettingsMutiNodeTest";
    o.PermissionValidator = (ctx) => Task.FromResult(false);//子节点无需修改的权限.
    o.UseStoreOfEFCore<StoreDbContext>();
});


var app = builder.Build();

//消费节点需配置通知路由
app.UseBiwenSettings();


//拉取配置
app.MapGet("/", (GitHubSetting setting) =>
{
    return Results.Json(setting);
});

app.Run();
