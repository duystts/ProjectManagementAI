# ProjectManagementAI

Ứng dụng quản lý dự án được xây dựng với ASP.NET Core Web API và Windows Forms.

## 🏗️ Kiến trúc hệ thống

- **Backend**: ASP.NET Core Web API (.NET 10)
- **Frontend**: Windows Forms Application (.NET 10)  
- **Database**: SQL Server với Entity Framework Core

## 📁 Cấu trúc dự án

### ProjectManagement.API (Backend)
- **Models**: Project, ProjectTask, TaskComment
- **Controllers**: API endpoints cho Projects và Tasks
- **Data**: Entity Framework DbContext
- **Database**: Tự động tạo và migrate

### ProjectManagement.WinForms (Frontend)
- **Forms**: Dashboard và Task Management
- **Controls**: ProjectCard và TaskCard components
- **Services**: API communication service
- **Models**: DTO classes

## 🚀 Cách chạy dự án

### 1. Chạy Backend API
```bash
cd ProjectManagement.API
dotnet run
```
API sẽ chạy tại: `https://localhost:7089`

### 2. Chạy Frontend WinForms
```bash
cd ProjectManagement.WinForms
dotnet run
```

## 🎯 Tính năng

- ✅ Xem danh sách projects
- ✅ Xem tasks theo project (Kanban board)
- ✅ Click events cho project và task cards
- 🔄 Thêm/sửa/xóa project (đang phát triển)
- 🔄 Thêm/sửa/xóa task (đang phát triển)

## 🔧 Công nghệ

- .NET 10
- Entity Framework Core
- SQL Server
- Windows Forms
- System.Text.Json

## 📝 Cấu hình

Cập nhật connection string trong `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ProjectManagementDB;Trusted_Connection=true;"
  }
}
```