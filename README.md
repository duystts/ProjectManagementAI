# ProjectManagementAI

Ứng dụng quản lý dự án thông minh với ASP.NET Core Web API và Windows Forms, tích hợp AI để tìm kiếm và gợi ý tasks.

## 🏗️ Kiến trúc

- **API**: ASP.NET Core (.NET 10)
- **Frontend**: Windows Forms (.NET 8)
- **Database**: SQLite với Entity Framework Core
- **Authentication**: JWT Bearer Token
- **AI**: Google AI Embedding API

## 📁 Cấu trúc

```
ProjectManagementAI/
├── ProjectManagement.API/          # Web API
├── ProjectManagement.WinForms/     # Windows Forms UI
├── ProjectManagement.Entities/     # Models & DTOs
├── ProjectManagement.DAL/          # Data Access Layer
└── ProjectManagement.BLL/          # Business Logic Layer
```

## ⚙️ Cấu hình

### API Key (Bắt buộc)

**Environment Variables:**
```bash
# Windows
set GOOGLEAI__APIKEY=your_google_ai_api_key

# Linux/Mac
export GOOGLEAI__APIKEY=your_google_ai_api_key
```

### Database
Database SQLite sẽ tự động tạo khi chạy lần đầu.

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
- ✅ **AI Search**: Tìm kiếm tasks thông minh
- ✅ **AI Suggestions**: Gợi ý tasks liên quan

## 🤖 Tính năng AI

- **Semantic Search**: Tìm kiếm tasks theo nghĩa, không chỉ từ khóa
- **Task Similarity**: Tìm tasks tương tự dựa trên nội dung
- **Smart Suggestions**: Gợi ý tasks liên quan khi xem chi tiết

## 👥 Roles

- **Admin**: Toàn quyền
- **Manager**: Quản lý projects/tasks
- **Member**: Tạo/sửa tasks
- **Viewer**: Chỉ xem

## 🔧 Công nghệ

- .NET 8/10, EF Core, SQLite
- JWT, BCrypt
- Windows Forms, System.Text.Json
- Google AI Embedding API
- Vector Search với Cosine Similarity

## 🔒 Bảo mật

- API keys đọc từ Environment Variables
- Không commit sensitive data vào Git
- JWT token authentication
- Password hashing với BCrypt