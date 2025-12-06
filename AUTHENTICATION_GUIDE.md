# Authentication & Authorization Guide

## Tài khoản mặc định

Sau khi chạy API, các tài khoản sau sẽ được tạo tự động:

| Username | Password   | Role    | Quyền hạn |
|----------|-----------|---------|-----------|
| admin    | admin123  | Admin   | Toàn quyền: Quản lý user, thêm/sửa/xóa project, thêm/sửa/xóa task |
| manager  | manager123| Manager | Thêm/sửa/xóa project, thêm/sửa/xóa task |
| member   | member123 | Member  | Chỉ thêm/sửa/xóa task trong kanban board |
| viewer   | viewer123 | Viewer  | Chỉ xem project và task, không có quyền chỉnh sửa |

## Phân quyền chi tiết

### Admin (Quyền cao nhất)
- ✅ Xem nút "User Manager" (nếu có form quản lý user)
- ✅ Thêm/sửa/xóa project
- ✅ Thêm/sửa/xóa task
- ✅ Xem tất cả dự án

### Manager
- ❌ Không có nút "User Manager"
- ✅ Thêm/sửa/xóa project
- ✅ Thêm/sửa/xóa task
- ✅ Xem tất cả dự án

### Member
- ❌ Không có nút "User Manager"
- ❌ Không thể thêm/sửa/xóa project
- ✅ Thêm/sửa/xóa task trong kanban board
- ✅ Xem tất cả dự án

### Viewer (Quyền thấp nhất)
- ❌ Không có nút "User Manager"
- ❌ Không thể thêm/sửa/xóa project
- ❌ Không thể thêm/sửa/xóa task
- ✅ Chỉ xem dự án và chọn để xem chi tiết

## Bảo mật

- **JWT Token**: Sử dụng JWT (JSON Web Token) để xác thực
- **BCrypt**: Mật khẩu được hash bằng BCrypt trước khi lưu vào database
- **Token Expiry**: Token có hiệu lực 7 ngày

## UI Changes

### ProjectCard
- Kích thước: 600x100 (gấp đôi chiều ngang, tăng chiều dọc)
- Nút Edit/Delete: Thay text bằng icon (✏️ và 🗑️)
- Hiển thị nút dựa trên role

### Dashboard
- Thêm label hiển thị thông tin user
- Thêm nút Logout
- Nút "Thêm dự án mới" chỉ hiện với Admin và Manager

### Kanban Board (Form1)
- Nút "Add Task" chỉ hiện với Admin, Manager, Member
- Viewer không thấy nút này

## Cách chạy

1. Chạy API:
```bash
cd ProjectManagement.API
dotnet restore
dotnet run
```

2. Chạy WinForms:
```bash
cd ProjectManagement.WinForms
dotnet restore
dotnet run
```

3. Đăng nhập bằng một trong các tài khoản trên

## API Endpoints

- `POST /api/auth/login` - Đăng nhập
- `POST /api/auth/register` - Đăng ký (nếu cần)
- Tất cả endpoints khác yêu cầu JWT token trong header: `Authorization: Bearer {token}`
