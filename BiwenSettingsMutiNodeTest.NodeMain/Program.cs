using BiwenSettingsMutiNodeTest.Shared.Settings;
using Biwen.Settings;
using Biwen.Settings.Extensions.Configuration;
using BiwenSettingsMutiNodeTest.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
    o.UseStoreOfEFCore<StoreDbContext>();

    //主节点开启通知
    o.NotifyOptions.IsNotifyEnable = true;
    o.NotifyOptions.EndpointHosts =
    [
        "http://localhost:5041",
        "http://localhost:5042" //消费节点
    ];
});

//扩展支持到IConfiguration & IOptions
builder.Configuration.AddBiwenSettingConfiguration(builder.Services, true);

var app = builder.Build();

//主要节点无需配置通知路由
app.UseBiwenSettings("biwen-settings/api", false);

//拉取配置,可以直接注入T或者使用IOptions<T>
app.MapGet("/", (
    IOptions<GitHubSetting> setting, //启动后不会变更
    IOptionsSnapshot<GitHubSetting> snapshot, //变更后同步变更
    GitHubSetting setting2 //变更后同步变更
    ) =>
{
    return Results.Json(new
    {
        data1 = setting.Value,
        data2 = setting2,
        data3 = snapshot.Value
    });
});

app.Run();
