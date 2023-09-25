### 用于测试Biwen.Settings的集群变更功能

![image](https://github.com/vipwan/Biwen.Settings/assets/13956765/e27cbca0-9c3d-4851-8aa1-37d2ce1ac97d)


- 前置条件:使用EFCore的CodeFirst功能,创建数据库表

- 第一步:  修改数据库链接,同时启动NodeMain 和 Node1 
- 第二步:  访问:http://localhost:5041/biwen-settings 修改配置项
- 第三部:  访问:http://localhost:5042 查看配置项是否变更


### Q&A
- 是否支持除EFCoreStore外的其他仓储?
  答:只要仓储是远程分布式的,都可以的,不过目前只集成了EFCore,你可以实现自己的Store仓储满足自己需求,目前Biwen.Settings自带的仓储只有EFCoreStore支持,JsonStore由于是本地存储,因此不支持