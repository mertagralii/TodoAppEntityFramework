# TodoAppEntityFramework 🚀

## 📌 Proje Açıklaması

Acunmedya Akademi 11. Dönem Genişletilmiş Yazılım Uzmanlığı Eğitimi kapsamında temelden uzmanlığa geçiş sürecinde geliştirdiğim TodoApp uygulamasıdır.

Bu proje, **Entity Framework Core** kullanarak bir **Todo List** uygulaması geliştirmek için oluşturulmuştur. **CRUD (Create, Read, Update, Delete)** işlemleri Entity Framework üzerinden gerçekleştirilmiştir. 

Ayrıca Bu rehberde, Entity Framework Core kullanarak bir ASP.NET Core MVC projesi için temel CRUD (Create, Read, Update, Delete) işlemlerinin nasıl yapılacağını öğreneceksiniz.

---

## 🛠️ Gerekli NuGet Paketleri

Projemizde **Entity Framework Core** kullanabilmek için aşağıdaki **NuGet** paketlerini yüklememiz gerekiyor:

📌 **Gerekli NuGet Paketleri:**


- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.EntityFrameworkCore.SqlServer`


> **⚠️ NOT:** Tüm paketlerin **aynı sürümde** olması gerekiyor!

---

## 🔌 Veritabanı Bağlantısı

**appsettings.json** dosyanıza **veritabanı bağlantı dizesini** aşağıdaki gibi ekleyin:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=MERT;Database=DBEntityFrameWorkTodoList;Integrated Security=true;TrustServerCertificate=True"
}
```

Ardından **Program.cs** dosyanızda aşağıdaki kodu ekleyin:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

> **📌 NOT:** Bu kodu `builder.Services.AddControllersWithViews();` satırının altına yazın!

---

## 🏗️ Model ve DbContext Tanımlama

İlk olarak **Models** klasörü içerisine **Todo.cs** adlı bir model ekleyelim: (Veritabanı Tablosu olarak da düşünebilirsin.)

```csharp
namespace TodoAppEntityFramework.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string JobName { get; set; }
        public bool IsApproved { get; set; }
    }
}
```

Daha sonra **Data** klasörü içerisinde **AppDbContext.cs** adlı sınıfımızı oluşturalım:

```csharp
using Microsoft.EntityFrameworkCore;
using TodoAppEntityFramework.Models;

namespace TodoAppEntityFramework.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
```

---

## 🛠️ Migration ve Veritabanı Güncelleme

📌 **Terminal veya Package Manager Console’a** aşağıdaki komutları sırasıyla yazın:

```sh
# Entity Framework CLI aracını yükleyin

 dotnet tool install --global dotnet-ef

# İlk Migration'ı oluşturun

 dotnet ef migrations add InitialCreate

# Veritabanını güncelleyin

 dotnet ef database update
```

> **🔄 NOT:** Yeni bir sütun veya tablo eklediğinizde şu komutları çalıştırmalısınız:
>
> ```sh
> dotnet ef migrations add YeniMigrationAdi
> dotnet ef database update
> ```
>
> Son yaptığınız değişikliği geri almak isterseniz:
>
> ```sh
> dotnet ef migrations remove --force
> ```

---

## 📌 Dependency Injection

HomeController’da **Dependency Injection** işlemini şu şekilde gerçekleştirdik:

```csharp
private readonly AppDbContext _context;

public HomeController(AppDbContext context)
{
    _context = context;
}
```

---

## 📌 CRUD İşlemleri

### ✅ **Listeleme İşlemi**

```csharp
public IActionResult Index()
{
    var todolist = _context.Todos.ToList();
    return View(todolist);
}
```

### 🔍 **Detay Getirme**

```csharp
public IActionResult Details(int Id)
{
    var details = _context.Todos.Find(Id);
    if(details != null)
    {
        return View(details);
    }
    return RedirectToAction("Index");
}
```

### ➕ **Ekleme İşlemi**

```csharp
[HttpPost]
public IActionResult AddTodo(Todo todo)
{
    _context.Todos.Add(todo);
    _context.SaveChanges();
    return RedirectToAction("Index");
}
```

### ❌ **Silme İşlemi**

```csharp
public IActionResult DeleteTodo(Todo todo)
{
    _context.Todos.Remove(todo);
    _context.SaveChanges();
    return RedirectToAction("Index");
}
```

### 🔄 **Güncelleme İşlemi**

```csharp
[HttpPost]
public IActionResult EditTodo(Todo todo)
{
    _context.Todos.Update(todo);
    _context.SaveChanges();
    return RedirectToAction("Index");
}
```

---


📌 **Projeyi çalıştırmak için** aşağıdaki adımları takip edin:

1️⃣ **Projeyi Bilgisayarınıza İndirin.**
2️⃣  **SQL Serverinize Aşağıdaki Veritabanı Scriptini ekleyin :** 
```sql
USE [DBEntityFrameWorkTodoList]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 19.02.2025 01:56:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Todos]    Script Date: 19.02.2025 01:56:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Todos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobName] [nvarchar](max) NOT NULL,
	[IsApproved] [bit] NOT NULL,
 CONSTRAINT [PK_Todos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250218164215_inital', N'8.0.13')
GO
SET IDENTITY_INSERT [dbo].[Todos] ON 
GO
INSERT [dbo].[Todos] ([Id], [JobName], [IsApproved]) VALUES (1, N'8.30''da uyan', 0)
GO
INSERT [dbo].[Todos] ([Id], [JobName], [IsApproved]) VALUES (2, N'Duş  Al', 0)
GO
INSERT [dbo].[Todos] ([Id], [JobName], [IsApproved]) VALUES (3, N'Kahvaltı Et', 0)
GO
INSERT [dbo].[Todos] ([Id], [JobName], [IsApproved]) VALUES (4, N'Plan Hazırla', 1)
GO
INSERT [dbo].[Todos] ([Id], [JobName], [IsApproved]) VALUES (5, N'Su İç', 1)
GO
INSERT [dbo].[Todos] ([Id], [JobName], [IsApproved]) VALUES (6, N'Kod Yaz', 1)
GO
INSERT [dbo].[Todos] ([Id], [JobName], [IsApproved]) VALUES (7, N'Okula git', 1)
GO
INSERT [dbo].[Todos] ([Id], [JobName], [IsApproved]) VALUES (8, N'Spora git', 1)
GO
INSERT [dbo].[Todos] ([Id], [JobName], [IsApproved]) VALUES (9, N'Yemeklerini ye', 1)
GO
INSERT [dbo].[Todos] ([Id], [JobName], [IsApproved]) VALUES (10, N'Ders Çalış', 1)
GO
INSERT [dbo].[Todos] ([Id], [JobName], [IsApproved]) VALUES (11, N'23.00''DA Uyu', 1)
GO
INSERT [dbo].[Todos] ([Id], [JobName], [IsApproved]) VALUES (15, N'test', 1)
GO
SET IDENTITY_INSERT [dbo].[Todos] OFF
GO

``` 
3️⃣ **Appsettings.Json dosyasındaki ConnectionString kısmındaki Veritabanı Adresinizi Kendi Veritabanı adresinize göre düzenleyin.** 
- **Projeyi Çalıştırın ✅**


## 🚀 Geliştirici Notları

- 📌 **Entity Framework Core** kullanılarak **veritabanı işlemleri** yönetilmiştir.
- 📌 **Dependency Injection** ile `AppDbContext` bağımlılığı **HomeController'a** enjekte edilmiştir.
- 📌 **Veritabanı güncellemeleri** `dotnet ef migrations` komutları ile yönetilmektedir.

📌 **Proje hakkında geri bildirimlerinizi paylaşmayı unutmayın!** 🎉

