# Azure App Configuration �ϥαо�

Azure App Configuration �O�@�ӪA�ȡA�ΨӶ����޲z���ε{�����]�w�A�ר�O�b���ݡB�h���ҩηL�A�Ȭ[�c�U�D�`��ΡC�������U�A���Ħa�����]�w�P�{���X�A�åB����ʺA��s�t�m�C�H�U�O�@�ӸԲӪ� Azure App Configuration �ϥαоǡC

### Azure App Configuration ���򥻨B�J

#### 1. **�Ы� Azure App Configuration �귽**

�����A�z�ݭn�b Azure ���Ыؤ@�� App Configuration �귽�C

1. �n�� [Azure �J�f����](https://portal.azure.com/)�C
2. �I���u**Create a resource**�v�C
3. �j���u**App Configuration**�v�A�M���I���u**Create**�v�C
4. �b�Ыح������A��ܭq�\�B�귽�s�ըéR�W�귽�]�Ҧp�G`my-app-config`�^�C
5. �I���u**Review + Create**�v�A�ˬd�]�m�L�~��A�I���u**Create**�v�ӧ����ЫءC

#### 2. **�K�[�t�m�]�m�� Azure App Configuration**

�@�� App Configuration �귽�Ыا����A�z�i�H�K�[���ε{�����t�m�]�w�C

1. �b Azure �J�f�������i�J�z�Ыت� App Configuration �귽�C
2. �I���u**Configuration Explorer**�v�Ӷi��t�m�޲z�C
3. �I���u**+ Create**�v�ӷs�W�@�Ӱt�m�C
   - **Key**: �]�w���W�١]�Ҧp�G`AppSettings:MySetting`�^�C
   - **Value**: �]�w���ȡ]�Ҧp�G`Hello, Azure!`�^�C
   - **Label**: ���ҡA�Ω�Ϥ����P���ҩΪ����]�i�H�d�ũγ]�m�����Ҽ��Ҧp `Production` �� `Development`�^�C
4. �K�[������A�I���u**Apply**�v�ӫO�s�]�m�C

#### 3. **�b .NET ���Τ��ϥ� Azure App Configuration**

���U�ӡA�z�i�H�N Azure App Configuration ������ .NET ���Τ��C�����A�ݭn�w�˥��n�� NuGet �]�C

##### �w�� NuGet �]

�ϥΥH�U�R�O�w�� `Microsoft.Azure.AppConfiguration.AspNetCore` NuGet �]�G

```bash
dotnet add package Microsoft.Azure.AppConfiguration.AspNetCore
```

#### 4. **�t�m .NET ���ΥH�s�� Azure App Configuration**

�b�z�� `.NET` ���ε{���]�Ҧp ASP.NET Core�^���A�z�ݭn�b `Program.cs` �� `Startup.cs` ��󤤶i��]�m�A�����ε{���q Azure App Configuration ���[���]�w�C

##### �b `Program.cs` ���t�m Azure App Configuration

���} `Program.cs` ���A�ñN�H�U�N�X�K�[��t�m�����G

```csharp
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// �[�J Azure App Configuration ���
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect("<Your-App-Configuration-Connection-String>")
           .Select(KeyFilter.Any, LabelFilter.Null);
});

// �]�m�A�ȩM����
builder.Services.AddRazorPages();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
```

�n���o Azure App Configuration ���s���r�Ŧ� (`Your-App-Configuration-Connection-String`)�A�z�ݭn�i�J Azure �J�f�����ç��z�Ыت� Azure App Configuration �귽�C�H�U�O����B�J�G

#### 5. **�]�m�s���r�Ŧ�**

�q Azure �J�f��������� App Configuration ���s���r�Ŧ�A�ñN�����Ψ�N�X���C

1. �b Azure �J�f�������i�J�z�Ыت� App Configuration �귽�C
2. �I���u**Access keys**�v�Ӭd�ݳs���r�Ŧ�C
3. �N�s���r�Ŧ�ƻs�A�M��b `Program.cs` ��󤤴��� `<Your-App-Configuration-Connection-String>` ����ڪ��s���r�Ŧ�C

#### 6. **�ϥ� Azure App Configuration ���]�m**

�z�i�H�b���ε{�Ǫ��N�X�������X�ݰt�m�C���]�z�b Azure App Configuration ���K�[�F�@�� `AppSettings:MySetting` ��A�z�i�H�q�L�H�U�覡Ū�����G

```csharp
var mySetting = builder.Configuration["AppSettings:MySetting"];
Console.WriteLine($"My setting value: {mySetting}");
```

�o�N�q Azure App Configuration ��Ū���Ӱt�m�]�m�ñN����ܦb����x���C

#### 7. **�ʺA�t�m��s**

Azure App Configuration ����ʺA�t�m��s�A�z�i�H�ҥι�ɰt�m��s�C�o�i�H�T�O���ε{���b�t�m�o���ܧ�ɦ۰ʧ�s�A�ӵL�ݭ��s���p���ε{���C

##### �K�[�ʺA��s���

�����A�b `Program.cs` ����s�t�m�N�X�A�[�J�ʺA��s����G

```csharp
builder.Services.AddAzureAppConfiguration();
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect("<Your-App-Configuration-Connection-String>")
           .Select(KeyFilter.Any, LabelFilter.Null)
           .ConfigureRefresh(refresh =>
           {
               refresh.Register("AppSettings:MySetting", refreshAll: true)
                      .SetCacheExpiration(TimeSpan.FromSeconds(30));
           });
});

// �ϥ� Azure App Configuration ������
app.UseAzureAppConfiguration();
```

�o�q�N�X�|�C 30 ��۰��ˬd `AppSettings:MySetting` �O�_����s�A�æ۰����Χ�s�����ε{�����C

#### 8. **���P���Ҫ��t�m**

�z�i�H�ϥ� **Label** �ӰϤ����P���Ҫ��t�m�C�Ҧp�A�z�i�H���uProduction�v�M�uDevelopment�v�t�m�K�[���P�����ҡA�æb���ε{�����ھڷ�e���ҿ�ܥ[�����T���t�m�C

�b `Program.cs` ���i�H�ھ����Ҩӥ[���a���������Ҫ��t�m�G

```csharp
var environment = builder.Environment.EnvironmentName;

builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect("<Your-App-Configuration-Connection-String>")
           .Select(KeyFilter.Any, environment);
});
```

�o�ˡA���ε{���|�۰ʥ[���������Ҽ��Ҫ��]�m�C

### �`��

Azure App Configuration ���ѤF�@���F���B�����ƪ��覡�Ӻ޲z���ε{���]�m�A�S�O�O�b���G���t�ΩΦh���ҳ��p���D�`���ΡC������²�ƤF�]�m�޲z�A�٤���ʺA�t�m��s�A�q�Ӵ����F���ε{�����i���@�ʩM�F���ʡC�ϥΥH�W�B�J�A�z�i�H���P�a�N Azure App Configuration ������z�� .NET ���Τ��A�æb�h�����Ҥ��޲z�z�����ΰt�m�C



---

## �p����o"<Your-App-Configuration-Connection-String>"

### 1. �n�� Azure �J�f����

�e�� [Azure �J�f����](https://portal.azure.com)�A�èϥαz���b��i��n���C

### 2. �M�� Azure App Configuration �귽

1. �b Azure �J�f�������A�I���������u**�Ҧ��귽**�v�]All Resources�^�Φb�j���椤��J `App Configuration`�A�M���ܱz�Ыت� App Configuration �귽�C
2. �i�J�귽�����C

### 3. ����s���r�Ŧ�

1. �b App Configuration �귽�������������A��쥪����椤���u**Access keys**�v�]�X�ݪ��_�^�C
2. �I���u**Access keys**�v�A�z�|�ݨ�h�ӳs���r�Ŧ�A�]�A�u**Primary connection string**�v�]�D�s���r�Ŧ�^�M�u**Secondary connection string**�v�]���s���r�Ŧ�^�C
3. �ƻs�u**Primary connection string**�v�Ρu**Secondary connection string**�v�X�X���̨�ӳ��i�H�ϥΡC

�o�N�O�z���ӥΦb���ε{������ `Your-App-Configuration-Connection-String`�C

### 4. �ϥγs���r�Ŧ�

�N�o�ӳs���r�Ŧ��߶K��z�����ε{�����A��p�G

```csharp
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect("<Your-App-Configuration-Connection-String>");
});
```

�Ϊ̱N���@�������ܼơB�K�_�޲z���覡�Ӧw���s�x�C