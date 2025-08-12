# 🚗 MaintainX

**MaintainX** is een modern full-stack platform voor voertuigbeheer en onderhoud, gebouwd met **Blazor WebAssembly** en **ASP.NET Core Web API**. Het platform biedt gebruikersbeheer, voertuigbeheer, onderhoudslogs en meer in één krachtige applicatie.

---

## ⚡️ Features

### 🔐 Auth & Gebruikersbeheer
- Inloggen & registreren (alleen Admin)
- JWT-tokenbeheer
- Rollen: **Admin**, **Mechanic**, **User**
- Veilig autorisatiebeheer

### 🚙 Voertuigenbeheer
- CRUD-operaties voor voertuigen
- Filteren & statusweergave
- Emissiefactoren tonen

### 🛠️ Onderhoudslogs & Schema’s
- Onderhoud registreren, bewerken & verwijderen
- Bijlagen uploaden/downloaden (PDF, afbeeldingen, Word, Excel, max 10MB)
- Onderhoud markeren als voltooid
- Visuele statusbadges: _Voltooid_, _Gepland_, _Geannuleerd_, etc.
- Beheer schema’s per voertuig & monteur

### 📊 Dashboard
- Overzicht van voertuigen, onderhoud & waarschuwingen
- Statistieken & prioriteiten
- Geautomatiseerde dataverversing

---

## 🏗️ Technologieën

**Backend**
- [.NET 8](https://dotnet.microsoft.com/)
- ASP.NET Core Web API
- Entity Framework Core + SQL Server
- ASP.NET Core Identity (gebruikers & rollenbeheer)
- JWT Bearer Authentication
- Swagger/OpenAPI documentatie
- CORS voor veilige client-API communicatie

**Frontend**
- Blazor WebAssembly (standalone client)
- AuthenticationStateProvider (JWT)
- HttpClient gekoppeld aan API
- Custom CSS: responsieve layout (Grid & Flexbox)

---

## 📁 Projectstructuur

- **API Controllers**: `AuthController`, `VehiclesController`, `MaintenanceLogController`, `MaintenanceScheduleController`, `UserProfileController`, `UsersController`
- **Blazor Pages**: `Dashboard.razor`, `MaintenanceSchedules.razor`, `MaintenanceLogs.razor`, `Register.razor`, `Vehicles.razor`
- **Services**: `VehicleService`, `MaintenanceLogService`, `MaintenanceScheduleService`, `AuthService`, `UserService`, `TokenService`, `UserProfileService`
- **wwwroot/uploads/maintenance/**: opslag voor upload-bestanden

---

## 🎨 UI & Styling

- Custom CSS voor consistente look & feel
- Responsief met CSS Grid en Flexbox
- Kleurenpalet: blauw `#4a90e2`, groen `#28a745`, rood `#dc3545`, geel `#ffc107`
- Specifieke stijlen voor login, dashboard, knoppen, alerts, progress bars & statusbadges

---

## ⚙️ Installatie & Gebruik

1. **Clone** deze repository
2. Configureer `appsettings.json` (database & JWT)
3. Voer `dotnet ef database update` uit om de database te initialiseren
4. Start backend en frontend projecten
5. Open de Blazor client in je browser via `https://localhost:xxxx`

---

## 📝 Opmerkingen

- Uploaden van bestanden is beperkt tot **maximaal 10MB per bestand**
- Autorisatie op basis van rollen voorkomt ongeautoriseerde toegang
- Validatie van gegevens via data annotations in Blazor-formulieren

---

> Heb je vragen of feedback? Open een issue of neem contact op met de beheerder!
