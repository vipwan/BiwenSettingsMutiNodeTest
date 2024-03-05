﻿
namespace BiwenSettingsMutiNodeTest.Shared
{
    using Biwen.Settings.Domains;
    using Biwen.Settings.SettingManagers.EFCore;
    using Microsoft.EntityFrameworkCore;


    /// <summary>
    /// 公用一个数据库的情况下,可以使用同一个DbContext
    /// </summary>
    public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options), IBiwenSettingsDbContext
    {
        public DbSet<Setting> Settings { get; set; }
    }
}