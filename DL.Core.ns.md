## DL.Core.ns 

 **DL.Core.ns**  ��һ�����ٿ�����һ����⣬�����EFCore����SQLSERVER���ݿ�����˷�װ��������SERVICE��������Զ�ע�룬�Ѿ����õ�ʹ��
ԭ��SQL������������ݿ�����
 
*** 
* DL.Core.Data
 
 ```
   >>���������ʹ��ԭ����SQL�������������
   **����SQLSERVER���ݿ�**
        ISqlServerDbContext service = new SqlServerDbContext();
       service.CreateDbConnection("");  //�������ݿ�����
       service.BeginTransation = false; //�Ƿ�������
       service.ExecuteNonQuery("", CommandType.Text); //�������޸ġ�ɾ�� ����
       service.GetDataSet("", CommandType.Text);// ��ȡ���ݵ��ڴ��
       service.GetDataTable("", CommandType.Text); // ��ȡ���ݵ�table
       // ע�⣺ ����ʵ������ֶνṹ����Ҫ�����ݿ���ֶ�һ��
       // �˲�������Ҫ��InsertEntityItems������һ������Ҫ�����Ƿ�������
       // ��Ҫʹ�ò������������  ��service.BeginTransation�� ��������
       service.InsertEntity("����ʵ��", "���ݱ�����");
       service.InsertEntityItems("����ʵ��", "���ݱ�����", "�Ƿ�������"); // ע�⣺ ����ʵ������ֶνṹ����Ҫ�����ݿ���ֶ�һ��
   **����MySQL���ݿ�**
   �����SQLSERVER���ݿ����ƣ�������������ʵ��򵥸�ʵ��д��

 ```

* DL.Core.ns

```
   >> ����������EFCore������һ�η�װ,����ģ�黯��˼���������
   >> ���а���ģ��ע�롢����ģʽ����ơ�������ݿ������Ĳ������¼��������Զ�ע�루����ע�룬�ӿڱ��ע�룩�Լ�����λ��
      IServiceCollection services = new ServiceCollection();
      // �����ܳ�ʼ��
	  // MyContext ���������ı���̳С�DbContextBase<MyContext>��
	  // �������ʵ�����ã�
      services.AddEngineDbContextPack<MyContext>();  //��ʼ�����ݿ������ģ����֧��3��
      services.AddEnginePack();// ģ��ע�룬�������õ��¼�������ִ�ע�룬���ߺ����ķ���ʵ�����ע��

	**����**
	public class UserTest:EntityBase
    {

    }
    public class MyContext : DbContextBase<MyContext>
    {
        public override string ConnectionString => "";

        public override void RegistConfiguration(ModelBuilder builder)
        {
           //IEntityTypeConfiguration
        }
    }
    public class UserConfiguration : ConfigurationBase<UserTest>
    {
        public override Type DbContextType => //����������

        public override void Configure(EntityTypeBuilder<UserTest> builder)
        {
            // ʵ������
        }
    }
  
```

* DL.Core.utility
 
```
 ����һ���������

```



