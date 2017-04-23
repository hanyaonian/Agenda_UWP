###现操期中项目

+ 用github进行版本控制和团队开发:
  * 使用方法：
     1. 安装git
     2. 在github中搜索 “xiancao_project" 找到我们的project，然后fork到自己的本地仓库
     3. 到自己的本地仓库中复制仓库地址
     4. 打开vs，在团队资源管理器中克隆你fork了的仓库 [tips](https://msdn.microsoft.com/zh-cn/library/hh850445.aspx#remote_3rd_party_connect_clone)
     5. 平时的提交操作和git差不多， “更改” 中提交，然后在 “同步” 中可以推送。 
     [终极tips](https://msdn.microsoft.com/en-us/library/vs/alm/code/git/get-started)
     

+ todo：
  1. LoginPage (登陆界面， 包含 账户密码)
  2. SignUpPage （注册界面，账户密码）
  3. AddTaskPage (发布任务页面， 时间，标题，描述)
  4. TaskContentPage 任务详情页面 （包含时间，标题，描述，后期考虑加入评论）
  4. MainPage 整个应用的框架,待完善 (包含左侧导航栏的那个页面)
  5. 页面之间的跳转：
    - LoginPage 登录成功后跳转到到 HomePage， 登陆失败则直接提示
    - LoginPage 点击注册跳转后到 SignUpPage
    - SignUpPage 注册成功后跳回 LoginPage ， 如果有用户重名则在 当前页面直接提示错误
    - MainPage页面点击菜单的‘发布任务’跳转到 AddTaskPage
    - MainPage页面点击菜单的‘任务列表’跳转到 TaskListPage
    - TaskListPage 中点击任务跳转到相应任务的 TaskContentPage
    - HomePage注销登录到LoginPage

