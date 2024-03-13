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

    o.UseCacheOfMemory();
    o.AutoFluentValidationOption.Enable = true;
    o.UseEncryption<Biwen.Settings.Encryption.EmptyEncryptionProvider>();
    o.ProjectId = "BiwenSettingsMutiNodeTest";
    o.PermissionValidator = (ctx) => true;//直接给予修改的权限.
    o.UseStoreOfEFCore(options =>
    {
        options.DbContextType = typeof(StoreDbContext);
    });

    //主节点开启通知
    o.NotifyOption.IsNotifyEnable = true;
    o.NotifyOption.EndpointHosts =
    [
        "http://localhost:5041",
        "http://localhost:5042" //消费节点
    ];
});


var app = builder.Build();

app.UseBiwenSettings();


//主要节点无需配置通知路由
app.MapBiwenSettingApi("biwen-settings/api", false);


//拉取配置
app.MapGet("/", (GitHubSetting setting) =>
{
    return Results.Json(setting);
});

app.Run();
