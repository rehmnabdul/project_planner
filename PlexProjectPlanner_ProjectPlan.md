
# 🧭 Project Plan — Plex Project Planner

**Goal:**  
Create a production-ready **ASP.NET Aspire** solution using **Blazor (Server)**, **ASP.NET Identity**, **EF Core (SQL Server)**, and **Domain-Driven Design** (DDD) similar to ABP.io.  
The app should clone **ClickUp’s tools** (workspaces, teams, projects, lists, tasks, subtasks, comments, goals, dashboards, permissions).

**Database connection string (exact):**
```
Server=(LocalDb)\MSSQLLocalDB;Database=ppp_db;Trusted_Connection=True;TrustServerCertificate=true
```

---

## 1️⃣ Solution & Project Layout (DDD + Aspire)

Create a solution named **Plex Project Planner** with these projects:

```
/PlexProjectPlanner.sln
/src
  /PlexProjectPlanner.Domain
  /PlexProjectPlanner.Application
  /PlexProjectPlanner.Infrastructure
  /PlexProjectPlanner.Web  (Blazor Server UI)
  /PlexProjectPlanner.ServiceDefaults   (Aspire)
  /PlexProjectPlanner.AppHost           (Aspire)
```

**Project references:**
- `PlexProjectPlanner.Application` → references `PlexProjectPlanner.Domain`
- `PlexProjectPlanner.Infrastructure` → references `PlexProjectPlanner.Domain` and `PlexProjectPlanner.Application`
- `PlexProjectPlanner.Web` → references `PlexProjectPlanner.Application` and `PlexProjectPlanner.Infrastructure`
- Aspire hosts (`ServiceDefaults`, `AppHost`) reference the Web project

---

## 2️⃣ DDD Folder Structure

### **/src/PlexProjectPlanner.Domain**
Entities, aggregates, and repositories for Workspaces, Projects, Tasks, Goals.

### **/src/PlexProjectPlanner.Application**
CQRS commands, queries, DTOs, and Application services using MediatR.

### **/src/PlexProjectPlanner.Infrastructure**
EF Core configurations, repositories, Identity setup, and DI registration.

### **/src/PlexProjectPlanner.Web (Blazor UI)**
Razor pages for Workspaces, Projects, Tasks, Goals, Dashboard, and Admin pages.

### **/src/PlexProjectPlanner.ServiceDefaults / AppHost**
Aspire default hosting and application composition.

---

## 3️⃣ Packages to Add

- MediatR, FluentValidation, AutoMapper  
- Microsoft.EntityFrameworkCore.SqlServer  
- Microsoft.AspNetCore.Identity.EntityFrameworkCore  
- MudBlazor (UI components)  
- Serilog (logging)  
- Microsoft.AspNetCore.SignalR (optional realtime)  

---

## 4️⃣ Identity & Permissions

- `AppUser : IdentityUser`  
- `AppRole : IdentityRole`  
- Seed default roles: `Admin`, `Member`, `Guest`  
- Policy-based authorization using Permission constants  

---

## 5️⃣ EF Core & Auto-Migrations

Database name: **ppp_db**  
Auto-migrate and seed Identity on startup.  
Includes Workspace, Project, TaskItem, Subtask, Comment, Goal entities.

---

## 6️⃣ Blazor UI Modules

- **Workspaces**: Create, manage, invite users  
- **Projects**: CRUD projects and lists  
- **Tasks**: Board view (drag-drop), details, subtasks, comments  
- **Goals**: Create, link to tasks/projects, progress tracking  
- **Dashboard**: Task metrics and charts  
- **Admin**: Manage users, roles, and permissions  

---

## 7️⃣ Aspire Configuration

AppHost composes Web app and SQL dependency with shared environment variables.

---

## 8️⃣ Seed Data

Default demo workspace, sample project, lists, tasks, and admin user `admin@local.test / Admin123!`

---

## 9️⃣ Output Expectations

- Fully compiling solution  
- EF Core migrations auto-applied  
- Identity and Blazor login working  
- CRUD and dashboard functional  
- Role-based access control enforced  
- Serilog logging configured  

---

### ✅ Connection String
```
Server=(LocalDb)\MSSQLLocalDB;Database=ppp_db;Trusted_Connection=True;TrustServerCertificate=true
```

### ✅ Solution Name
**Plex Project Planner**

