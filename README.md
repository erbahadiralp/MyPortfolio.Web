# MyPortfolio.Web

ASP.NET Core MVC ile geliştirilmiş kişisel portfolyo sitesi. İçerikler (hakkımda, deneyim, eğitim, projeler, sertifikalar, yetenekler) admin panelinden yönetiliyor.

## Teknolojiler

- ASP.NET Core 8 MVC
- PostgreSQL 15
- Entity Framework Core
- ASP.NET Core Identity
- Nginx (reverse proxy)
- Docker & Docker Compose

## Kurulum

### Docker ile

```bash
cd MyPortfolio.Web

# ortam dosyasını oluştur
cp production.env.example production.env

# production.env içini doldur:
#   DB_PASSWORD       -> veritabanı şifresi
#   ADMIN_PASSWORD    -> admin panel şifresi (min 8 karakter, büyük harf + rakam)
#   TURNSTILE_SECRET_KEY -> cloudflare turnstile key

# ayağa kaldır
docker compose up --build -d
```

### Docker'sız

.NET 8 SDK ve PostgreSQL kurulu olmalı.

```bash
# bağlantı bilgisi (environment variable veya appsettings)
ConnectionStrings__DefaultConnection=Host=localhost;Database=MyPortfolioDb;Username=postgres;Password=sifre

# ef tool yoksa kur
dotnet tool install --global dotnet-ef

# migration uygula
dotnet ef database update

# çalıştır
dotnet run
```

## Erişim

| | Docker | Manuel |
|---|---|---|
| Site | `http://localhost` | `http://localhost:5280` |
| Admin | `http://localhost/Account/Login` | `http://localhost:5280/Account/Login` |

Admin girişi: kullanıcı adı `admin`, şifre `ADMIN_PASSWORD` env variable'ındaki değer. Varsayılan `Admin2024!`.

## Yapı

```
MyPortfolio.Web/
├── Controllers/
├── Models/
├── Views/
│   ├── Home/            # ana sayfa + partial'lar
│   ├── Shared/          # layout
│   └── *Admin/          # admin paneli
├── ViewComponents/
├── Resources/           # tr-TR, en-US dil dosyaları
├── wwwroot/             # css, js, görseller
├── Migrations/
├── docker-compose.yml
├── Dockerfile
└── nginx.conf
```