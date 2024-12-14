using Biwen.Settings;
using System.ComponentModel.DataAnnotations;

namespace BiwenSettingsMutiNodeTest.Shared.Settings;

/// <summary>
/// 模拟的一个配置
/// </summary>
public class GitHubSetting : ValidationSettingBase<GitHubSetting>
{
    [Required, StringLength(36, MinimumLength = 6)]
    public string AppId { get; set; } = "123456";

    [Required, StringLength(128, MinimumLength = 6)]
    public string AppSecret { get; set; } = "qwetyuiopasdfghjklzxcvbnm";

}