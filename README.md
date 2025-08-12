MaintainX
MaintainX is een full-stack voertuigbeheer- en onderhoudsplatform, gebouwd met Blazor WebAssembly en ASP.NET Core Web API. De applicatie ondersteunt gebruikersbeheer, voertuigbeheer, onderhoudslogs en -schema’s met een moderne, responsieve UI en veilige JWT-authenticatie.

🚀 Technologieën
Backend
.NET 8, ASP.NET Core Web API

Entity Framework Core + SQL Server

ASP.NET Core Identity (gebruikers & rollenbeheer)

JWT Bearer Authentication

Swagger/OpenAPI documentatie

CORS-configuratie voor client-API communicatie

Frontend
Blazor WebAssembly (standalone client)

AuthenticationStateProvider met JWT

HttpClient gekoppeld aan API

Custom CSS met responsieve layout (Grid & Flexbox)

📁 Hoofdfunctionaliteiten
Auth & Gebruikersbeheer
Inloggen, registreren (Admin only)

JWT-tokenbeheer

Rollen: Admin, Mechanic, User

Voertuigenbeheer
CRUD-operaties

Filteren en statusweergave

Emissiefactoren tonen

Onderhoudslogs & Schema’s
Onderhoud registreren, bewerken en verwijderen

Uploaden/downloaden van bijlagen (PDF, afbeeldingen, Word, Excel)

Onderhoud markeren als voltooid

Visuele statusbadges (Voltooid, Gepland, Geannuleerd, etc.)

Schema’s beheren per voertuig en monteur

Dashboard
Overzicht voertuigen, onderhoud en waarschuwingen

Statistieken en prioriteiten

Geautomatiseerde data verversing

📂 Projectstructuur (globaal)
API Controllers: AuthController, VehiclesController, MaintenanceLogController, MaintenanceScheduleController, UserProfileController, UsersController

Blazor Pages: Dashboard.razor, MaintenanceSchedules.razor, MaintenanceLogs.razor, Register.razor, Vehicles.razor

Services: VehicleService, MaintenanceLogService, MaintenanceScheduleService, AuthService, UserService, TokenService, UserProfileService

wwwroot/uploads/maintenance/: opslag voor upload-bestanden

🎨 UI & Styling
Custom CSS voor consistente look & feel

Responsief met CSS Grid en Flexbox

Kleurenpalet: blauw (#4a90e2), groen (#28a745), rood (#dc3545), geel (#ffc107)

Specifieke stijlen voor login, dashboard, knoppen, alerts, progress bars en statusbadges

⚙️ Installatie & Gebruik
Clone de repo

Configureer de appsettings.json voor database en JWT

Voer dotnet ef database update uit om de database te initialiseren

Start backend en frontend projecten

Open de Blazor client in de browser via https://localhost:xxxx

📝 Opmerkingen
Uploaden van bestanden wordt beperkt tot max 10MB per bestand

Autorisatie op basis van rollen voorkomt ongeautoriseerde toegang

Data validatie via data annotations in Blazor-formulieren

