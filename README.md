# TodoAppEntityFramework 🚀

## 📌 Proje Açıklaması

Bu proje, **Entity Framework Core** kullanarak bir **Todo List** uygulaması geliştirmek için oluşturulmuştur. **CRUD (Create, Read, Update, Delete)** işlemleri Entity Framework üzerinden gerçekleştirilmiştir. 

Ayrıca Bu rehberde, Entity Framework Core kullanarak bir ASP.NET Core MVC projesi için temel CRUD (Create, Read, Update, Delete) işlemlerinin nasıl yapılacağını öğreneceksiniz.

---

## 🛠️ Gerekli NuGet Paketleri

Projemizde **Entity Framework Core** kullanabilmek için aşağıdaki **NuGet** paketlerini yüklememiz gerekiyor:

📌 **Gerekli NuGet Paketleri:**

- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`

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

## 📌 Projeyi Çalıştırma

📌 **Projeyi çalıştırmak için** aşağıdaki adımları takip edin:

1️⃣ **Gerekli NuGet paketlerini yükleyin**

2️⃣ **Migration işlemlerini yapın**

```sh
dotnet ef migrations add InitialCreate
dotnet ef database update
```

3️⃣ **Projeyi başlatın**

```sh
dotnet run
```

---

## 🚀 Geliştirici Notları

- 📌 **Entity Framework Core** kullanılarak **veritabanı işlemleri** yönetilmiştir.
- 📌 **Dependency Injection** ile `AppDbContext` bağımlılığı **HomeController'a** enjekte edilmiştir.
- 📌 **Veritabanı güncellemeleri** `dotnet ef migrations` komutları ile yönetilmektedir.

📌 **Proje hakkında geri bildirimlerinizi paylaşmayı unutmayın!** 🎉

