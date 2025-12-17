# ProjectManagementAI

Ứng dụng quản lý dự án với ASP.NET Core Web API và Windows Forms.

## 🏗️ Kiến trúc

- **API**: ASP.NET Core (.NET 10)
- **Frontend**: Windows Forms (.NET 8)
- **Database**: SQLite với Entity Framework Core
- **Authentication**: JWT Bearer Token

## 📁 Cấu trúc

```
ProjectManagementAI/
├── ProjectManagement.API/          # Web API
├── ProjectManagement.WinForms/     # Windows Forms UI
├── ProjectManagement.Entities/     # Models & DTOs
├── ProjectManagement.DAL/          # Data Access Layer
└── ProjectManagement.BLL/          # Business Logic Layer
```

## 🚀 Chạy ứng dụng

### 1. Backend API
```bash
cd ProjectManagement.API
dotnet run
```
API: `https://localhost:7089`

### 2. Frontend
```bash
cd ProjectManagement.WinForms
dotnet run
```

## 🎯 Tính năng

- ✅ Đăng nhập/đăng ký người dùng
- ✅ Quản lý projects (CRUD)
- ✅ Quản lý tasks (Kanban board)
- ✅ Quản lý người dùng (Admin)
- ✅ Phân quyền theo role
- ✅ JWT Authentication

## 👥 Roles

- **Admin**: Toàn quyền
- **Manager**: Quản lý projects/tasks
- **Member**: Tạo/sửa tasks
- **Viewer**: Chỉ xem

## 🔧 Công nghệ

- .NET 8/10, EF Core, SQLite
- JWT, BCrypt
- Windows Forms, System.Text.Json