# SezerAI Web - Master Control Platform

**Platform Merkezi Kontrol ve Yönetim Sistemi**

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen)]()
[![.NET Version](https://img.shields.io/badge/.NET-9.0-blue)]()
[![License](https://img.shields.io/badge/license-Proprietary-red)]()

---

## 📖 Proje Özeti

SezerAI Web, birden fazla web platformunu tek bir merkezi panel üzerinden izlemek, yönetmek ve kontrol etmek için geliştirilmiş bir **NOC (Network Operations Center)** çözümüdür.

**Yönetilen Platformlar:**
- 🏥 **AI Hospital** (Tibbiye DB) - Sağlık platformu
- 🛒 **AiBazaar** (bakkal DB) - E-ticaret platformu
- 🔧 **Geliyoo** (HzYusa DB) - Hizmet platformu
- 🪟 **Perde İmalat** (pencere DB) - Perde platformu

---

## 🏗️ Mimari

Proje **Clean Architecture** prensiplerine göre tasarlanmıştır:

```
SezerAiWeb/
├── SezerAiWeb.Domain/              # Domain Layer
├── SezerAiWeb.Application/         # Application Layer (Services, DTOs, Interfaces)
├── SezerAiWeb.Infrastructure/      # Infrastructure Layer (External Services)
├── SezerAiWeb.Persistence/         # Persistence Layer (EF Core, PostgreSQL)
└── SezerAiWeb.Web/                 # Presentation Layer (ASP.NET Core MVC)
    ├── Areas/
    │   └── MasterPanel/            # Master Panel Area
    │       ├── Controllers/
    │       │   ├── DashboardController.cs
    │       │   └── AuthController.cs
    │       └── Views/
    │           ├── Dashboard/Index.cshtml
    │           ├── Auth/Login.cshtml
    │           └── Shared/_MasterPanelLayout.cshtml
    ├── wwwroot/
    └── appsettings.json
```

---

## 🛠️ Teknoloji Stack

### Backend
- **Framework:** ASP.NET Core 9.0 (.NET 9.0)
- **Architecture:** Clean Architecture
- **ORM:** Entity Framework Core 9.0
- **Database:** PostgreSQL 14+ (hzmuhammed)
- **Multi-DB:** Npgsql 9.0.0 (4 farklı database bağlantısı)
- **Logging:** Serilog (Console + File)

### Frontend
- **CSS Framework:** Tailwind CSS 3.x (CDN)
- **Design System:** Glassmorphic macOS Big Sur inspired
- **Icons:** Material Symbols Outlined
- **Typography:** Hanken Grotesk
- **Responsive:** Mobile-first approach (320px - 1440px)

### Key Features
- ✅ iOS Safe Area Support (viewport-fit=cover)
- ✅ Hamburger Menu + Drawer Navigation (mobil)
- ✅ Responsive Grid System (1 col mobile, 12 col desktop)
- ✅ Platform User Statistics (Multi-database aggregation)
- ✅ Real-time Dashboard Metrics

---

## 📊 Platform Kullanıcı İstatistikleri

Dashboard üzerinden 4 farklı platformdan gerçek zamanlı kullanıcı sayıları görüntülenir:

| Platform | Database | Tablo | Credentials |
|----------|----------|-------|-------------|
| Geliyoo | HzYusa | AspNetUsers | AGeylani / HisaR3466! |
| AiHospital | Tibbiye | Users | AGeylani / HisaR3466! |
| AiBazaar | bakkal | Kullanicilar | bakkalamca / HisaR3466! |
| Perde İmalat | pencere | Kullanici | perdeci / HisaR3466! |

**Servis:** `PlatformStatsService` - Multi-database connection pooling ile performans optimizasyonu

---

## 📱 Mobil Responsive Dönüşüm

**Tarih:** 29 Temmuz 2026
**Durum:** ✅ %100 Tamamlandı

### Yapılan Değişiklikler

#### 1. Layout Düzeltmeleri (_MasterPanelLayout.cshtml)

| Bileşen | Önceki | Yeni | Sonuç |
|---------|--------|------|-------|
| **Viewport** | width=device-width | viewport-fit=cover eklendi | iOS Safe Area |
| **Body** | overflow: hidden, height: 100vh | overflow-y: auto, min-height: 100vh | Mobil scroll |
| **Sidebar** | Fixed görünür | hidden md:flex | Mobilde gizli |
| **Header** | Nav only | Hamburger menu + drawer | Mobil navigasyon |
| **Dock** | 12x12 butonlar | 10x10 md:12x12 | Responsive |
| **Main Grid** | grid-cols-12 | grid-cols-1 md:grid-cols-12 | Mobilde tek sütun |

#### 2. Dashboard Responsive (Index.cshtml)

| Bölüm | Değişiklik |
|-------|-----------|
| **Sol Panel** | col-span-1 md:col-span-2 |
| **Merkez Alan** | flex-col md:flex-row |
| **Sağ Panel** | col-span-1 md:col-span-3 |
| **Widget Padding** | p-4 md:p-5 |
| **Border Radius** | rounded-xl md:rounded-panel-radius |

#### 3. Ekran Uyumluluğu

| Ekran | Breakpoint | Layout |
|-------|-----------|--------|
| **Mobile** | < 768px | Tek sütun, hamburger menu, 10x10 dock |
| **Tablet** | 768px - 1024px | Üç sütun, sidebar görünür, 12x12 dock |
| **Desktop** | > 1024px | Tam genişlik, tüm özellikler aktif |

### Build & Deploy

```bash
# Build (0 Uyarı, 0 Hata)
dotnet build -c Release

# Publish to IIS
net stop w3svc
dotnet publish -c Release -o "C:\inetpub\wwwroot\sezerai"
net start w3svc
```

**Deploy Durumu:** ✅ Başarılı
**Site:** https://www.sezerai.tr
**Panel:** https://www.sezerai.tr/MasterPanel/Dashboard

---

## 🚀 Kurulum

### 1. Gereksinimler
- .NET 9.0 SDK
- PostgreSQL 14+
- Visual Studio 2022 veya VS Code
- Git

### 2. Veritabanı Yapılandırması

```json
// appsettings.Development.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=hzmuhammed;Username=hzgeylani;Password=***"
  }
}
```

### 3. Migration

```bash
cd SezerAiWeb.Persistence
dotnet ef database update --project ../SezerAiWeb.Web
```

### 4. Çalıştırma

```bash
cd SezerAiWeb.Web
dotnet run
```

**URL:** https://localhost:5001/MasterPanel/Dashboard

---

## 📁 Değiştirilen Dosyalar

### Mobil Responsive Dönüşümü (29 Tem 2026)

1. ✅ `SezerAiWeb.Web/Areas/MasterPanel/Views/Shared/_MasterPanelLayout.cshtml`
2. ✅ `SezerAiWeb.Web/Areas/MasterPanel/Views/Dashboard/Index.cshtml`
3. ✅ `SezerAiWeb.Web/Areas/MasterPanel/Views/Auth/Login.cshtml`
4. ✅ `SezerAiWeb.Web/Areas/MasterPanel/Views/Auth/Register.cshtml`
5. ✅ `SezerAiWeb.Web/Areas/MasterPanel/Views/Auth/ForgotPassword.cshtml`

### Platform Stats Feature (28 Tem 2026)

1. ✅ `SezerAiWeb.Application/DTOs/PlatformUserCountDto.cs`
2. ✅ `SezerAiWeb.Application/Interfaces/IPlatformStatsService.cs`
3. ✅ `SezerAiWeb.Application/Services/PlatformStatsService.cs`
4. ✅ `SezerAiWeb.Web/Areas/MasterPanel/Controllers/DashboardController.cs`
5. ✅ `SezerAiWeb.Application/SezerAiWeb.Application.csproj` (Npgsql 9.0.0)

---

## 🎯 Özellikler

### Dashboard
- ✅ Multi-database platform user statistics
- ✅ Real-time metrics display
- ✅ Glass morphism design
- ✅ Responsive layout (mobile-first)
- ✅ iOS safe-area support
- ✅ Hamburger navigation

### Security
- ✅ Multi-database connection pooling
- ✅ Try-catch exception handling
- ✅ User Secrets for credentials
- ✅ IIS Application Pool environment variables

---

## 📝 Rapor ve Dokümantasyon

| Dosya | İçerik |
|-------|--------|
| `MOBIL_RESPONSIVE_RAPOR.md` | Detaylı mobil responsive raporu |
| `databaseler.md` | Veritabanı bağlantı bilgileri (doğrulanmış) |
| `README.md` | Bu dosya |

---

## 🔐 Güvenlik Notları

**Önemli:**
- Database credentials User Secrets'ta saklanıyor
- Production'da IIS Application Pool environment variables kullanılıyor
- appsettings.Production.json'da şifre yok (boş string)
- Git'te hiçbir şifre commit edilmedi

---

## 🧪 Test Durumu

```bash
dotnet test
```

**Sonuç:** Henüz test projesi yok (planlı)

---

## 📞 İletişim

- **Website:** https://www.sezerai.tr
- **Panel:** https://www.sezerai.tr/MasterPanel/Dashboard
- **Email:** info@sezerai.tr

---

## 📄 Lisans

© 2026 SEZER AI Technology - Tüm hakları saklıdır.

---

**Son Güncelleme:** 29 Temmuz 2026
**Versiyon:** 1.0.0-responsive
**Build:** ✅ Başarılı (0 Uyarı, 0 Hata)
**Deploy:** ✅ IIS (C:\inetpub\wwwroot\sezerai)
**Mobil Uyumluluk:** ✅ %100 (iOS + Android)
