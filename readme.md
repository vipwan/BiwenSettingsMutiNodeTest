### 用于测试Biwen.Settings的集群变更功能

- 前置条件:使用EFCore的CodeFirst功能,创建数据库表

- 第一步:  修改数据库链接,同时启动NodeMain 和 Node1 
- 第二步:  访问:http://localhost:5041/biwen-settings 修改配置项
- 第三部:  访问:http://localhost:5042 查看配置项是否变更

