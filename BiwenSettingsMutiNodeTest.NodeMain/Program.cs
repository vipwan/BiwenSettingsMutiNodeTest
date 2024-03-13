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
    o.PermissionValidator = (ctx) => true;//ֱ�Ӹ����޸ĵ�Ȩ��.
    o.UseStoreOfEFCore(options =>
    {
        options.DbContextType = typeof(StoreDbContext);
    });

    //���ڵ㿪��֪ͨ
    o.NotifyOption.IsNotifyEnable = true;
    o.NotifyOption.EndpointHosts =
    [
        "http://localhost:5041",
        "http://localhost:5042" //���ѽڵ�
    ];
});


var app = builder.Build();

app.UseBiwenSettings();


//��Ҫ�ڵ���������֪ͨ·��
app.MapBiwenSettingApi("biwen-settings/api", false);


//��ȡ����
app.MapGet("/", (GitHubSetting setting) =>
{
    return Results.Json(setting);
});

app.Run();
