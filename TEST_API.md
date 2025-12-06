# Hướng dẫn kiểm tra và chạy API

## Bước 1: Chạy API

Mở terminal/command prompt và chạy:

```bash
cd C:\Users\NgocIT\OneDrive\Desktop\PMWAI\ProjectManagementAI-main\ProjectManagement.API
dotnet restore
dotnet run
```

API sẽ chạy tại: `https://localhost:7089`

## Bước 2: Kiểm tra API đang chạy

Mở trình duyệt và truy cập: `https://localhost:7089/api/projects`

- Nếu thấy JSON data → API đang chạy ✅
- Nếu không kết nối được → API chưa chạy ❌

## Bước 3: Test Login bằng Postman hoặc curl

### Dùng curl:
```bash
curl -X POST https://localhost:7089/api/auth/login ^
  -H "Content-Type: application/json" ^
  -d "{\"username\":\"admin\",\"password\":\"admin123\"}"
```

### Dùng Postman:
- URL: `https://localhost:7089/api/auth/login`
- Method: POST
- Body (JSON):
```json
{
  "username": "admin",
  "password": "admin123"
}
```

## Bước 4: Kiểm tra lỗi thường gặp

### Lỗi 1: Port đã được sử dụng
Nếu thấy lỗi "Address already in use", kiểm tra port:
```bash
netstat -ano | findstr :7089
```

### Lỗi 2: Database không tạo được
Kiểm tra SQL Server đang chạy:
- Mở SQL Server Management Studio
- Kết nối với `localhost\SQLEXPRESS`

### Lỗi 3: Certificate SSL
Nếu thấy lỗi SSL, chạy:
```bash
dotnet dev-certs https --trust
```

## Bước 5: Kiểm tra connection string

Mở file `appsettings.json` và kiểm tra:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=ProjectManagementDB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

Nếu SQL Server của bạn không phải `SQLEXPRESS`, thay đổi thành:
- `(localdb)\\mssqllocaldb` - cho LocalDB
- `localhost` - cho SQL Server mặc định
- `localhost\\TÊN_INSTANCE` - cho instance khác

## Bước 6: Xem log API

Khi chạy API, xem console output để biết:
- Database có được tạo thành công không
- Có lỗi gì không
- Port đang chạy là gì

## Tài khoản test

| Username | Password   | Role    |
|----------|-----------|---------|
| admin    | admin123  | Admin   |
| manager  | manager123| Manager |
| member   | member123 | Member  |
| viewer   | viewer123 | Viewer  |

## Nếu vẫn lỗi

1. Xóa database cũ:
```sql
DROP DATABASE ProjectManagementDB;
```

2. Chạy lại API để tạo database mới

3. Kiểm tra lại với curl/Postman trước khi dùng WinForms
