### 概述

DL.Core.ns 是一个基于.NETCore平台下的一个快速开发工具，它能很快速方便的让我们
使用EFCore来访问数据库

### 引入DL.Core.ns
```
      IServiceCollection services = new ServiceCollection();
      ///引入DL.Core.ns 的扩展
      ///1.如果需要操作数据库，直接使用 AddEngineDbContextPack<你的EF数据库上下文>
      ///2.此上下文可以支持多个,最多支持3个数据库上下文
       services.AddEngineDbContextPack<MyContext>();
       ///3.初始化模块注入
        services.AddEnginePack();

```
### 服务类自动注入

#### 特性注入

```
1.特性注入需要用户在服务实现里面进行特性的标记
  //AttbuiteDependency(ServiceLifetime.Scoped) 同样有三种注入方式
   [AttbuiteDependency(ServiceLifetime.Scoped)]
   public class UserService : IUserService
  {
  }
```

#### 接口注入

```
 1.作用域模式
 【IScopeDependcy】系统将自动采用作用域注入的方式注入
 public interface IUserService : IScopeDependcy
 {
        void AddTeacher();
 }
 2.瞬时模式
  【ITransientDependcy】系统将自动采用瞬时模式注入的方式注入
  public interface IUserService : ITransientDependcy
 {
        void AddTeacher();
 }
 3.单例模式
  【ISingletonDependcy】系统将自动采用单例模式注入的方式注入
  public interface IUserService : ISingletonDependcy
 {
        void AddTeacher();
 }

 ```