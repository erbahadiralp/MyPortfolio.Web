# MyPortfolio.Web

ASP.NET Core MVC ve Docker kullanılarak geliştirilmiş kişisel portfolyo web sitesidir. İçerik (Hakkımda, Deneyimler, Eğitim, Yetenekler vb.) admin paneli üzerinden yönetilir.

## Teknolojiler

- ASP.NET Core 8 MVC
- Microsoft SQL Server
- Docker & Docker Compose

## Projeyi Çalıştırma

### 1. Docker ile (Önerilen)

Docker ve Docker Compose kurulu olmalıdır.

**Geliştirme Ortamı:**
```bash
docker-compose up -d
```

**Production Ortamı:**
```bash
# 1. Ortam dosyasını kopyala
cp production.env.example production.env

# 2. Dosyayı aç ve DB_PASSWORD değerini ayarla

# 3. Projeyi başlat
docker-compose -f docker-compose.prod.yml --env-file production.env up --build -d
```

### 2. Manuel Kurulum (Docker'sız)

**Gerekenler:**
- .NET 8 SDK
- Microsoft SQL Server
- Entity Framework CLI (`dotnet-ef`)

**Kurulum Adımları:**
1. `appsettings.Development.json` içindeki `ConnectionStrings.DefaultConnection` değerini kendi SQL Server adresine göre düzenle.

2. Entity Framework migration'larını uygula:
   ```bash
   # Gerekliyse EF aracını kur:
   dotnet tool install --global dotnet-ef
   
   # Veritabanını oluştur/güncelle:
   dotnet ef database update
   ```

3. Projeyi çalıştır:
   ```bash
   dotnet run
   ```

## Uygulamaya Erişim

**Ana Sayfa:**
- Docker: `http://localhost`
- Manuel: `http://localhost:5242` (veya terminalde belirtilen port)

**Admin Paneli:**
- Docker: `http://localhost/Account/Login`
- Manuel: `http://localhost:5242/Account/Login`

*Not: Admin giriş bilgileri `DbSeeder.cs` içinde tanımlıdır.*