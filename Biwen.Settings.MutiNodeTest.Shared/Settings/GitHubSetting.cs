
namespace BiwenSettingsMutiNodeTest.Shared.Settings
{

    using Biwen.Settings;
    using FluentValidation;

    /// <summary>
    /// 模拟的一个配置
    /// </summary>
    public class GitHubSetting : ValidationSettingBase<GitHubSetting>
    {
        public string AppId { get; set; } = "123456";
        public string AppSecret { get; set; } = "qwetyuiopasdfghjklzxcvbnm";

        public GitHubSetting()
        {
            RuleFor(x => x.AppId).NotEmpty().Length(5, 36);
            RuleFor(x => x.AppSecret).NotEmpty().Length(5, 128);
        }

    }
}