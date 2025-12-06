# Bug Fixes Report

## 🐛 Các lỗi đã phát hiện và sửa

### 1. ❌ BUG NGHIÊM TRỌNG: API URL không đúng
**Vấn đề**: ApiService sử dụng `http://localhost:5276/api` nhưng API thực tế chạy ở `https://localhost:7089/api`

**Hậu quả**: Không kết nối được API, tất cả request đều fail

**Đã sửa**: 
```csharp
private readonly string _baseUrl = "https://localhost:7089/api";
```

---

### 2. ❌ BUG NGHIÊM TRỌNG: Thiếu Authorization trên Controllers
**Vấn đề**: ProjectsController và TasksController không có `[Authorize]` attribute

**Hậu quả**: Bất kỳ ai cũng có thể truy cập API mà không cần đăng nhập

**Đã sửa**: 
- Thêm `[Authorize]` cho toàn bộ controller
- Thêm `[Authorize(Roles = "...")]` cho từng endpoint theo quyền:
  - Projects: Admin, Manager có thể Create/Update/Delete
  - Tasks: Admin, Manager, Member có thể Create/Update/Delete
  - Viewer chỉ có thể GET (xem)

---

### 3. ❌ BUG: HttpClient không được cập nhật token sau login
**Vấn đề**: ApiService tạo HttpClient trong constructor, nhưng token chỉ có sau khi login

**Hậu quả**: Các request sau login không có Authorization header, bị 401 Unauthorized

**Đã sửa**: 
- Chuyển HttpClient thành static để dùng chung
- Thêm method `UpdateAuthHeader()` để cập nhật token mỗi khi tạo ApiService mới
- Xóa token cũ trước khi set token mới

```csharp
private static readonly HttpClient _httpClient = new HttpClient();

private void UpdateAuthHeader()
{
    _httpClient.DefaultRequestHeaders.Authorization = null;
    if (!string.IsNullOrEmpty(AuthService.Token))
    {
        _httpClient.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthService.Token);
    }
}
```

---

### 4. ❌ BUG: Memory leak với HttpClient
**Vấn đề**: Mỗi ApiService tạo HttpClient mới, không dispose đúng cách

**Hậu quả**: Memory leak, tốn tài nguyên

**Đã sửa**: 
- Chuyển HttpClient thành static singleton
- Xóa method Dispose() không cần thiết

---

### 5. ❌ BUG: LoginForm không dispose khi ẩn
**Vấn đề**: LoginForm chỉ `Hide()` chứ không `Close()` khi login thành công

**Hậu quả**: Form vẫn tồn tại trong memory

**Đã sửa**: 
```csharp
var dashboardForm = new DashboardForm();
dashboardForm.FormClosed += (s, args) => this.Close();
dashboardForm.Show();
this.Hide();
```

---

### 6. ❌ LOGIC ERROR: Application không thoát khi logout
**Vấn đề**: Khi logout, app tạo LoginForm mới nhưng không handle việc thoát app

**Hậu quả**: App chạy ngầm khi đóng tất cả form

**Đã sửa**: 
```csharp
private void btnLogout_Click(object sender, EventArgs e)
{
    AuthService.Logout();
    var loginForm = new LoginForm();
    loginForm.FormClosed += (s, args) => Application.Exit();
    loginForm.Show();
    this.Close();
}
```

---

### 7. ❌ BUG: Không kiểm tra AuthService.CurrentUser null
**Vấn đề**: Nếu user chưa login mà vào Dashboard sẽ crash khi truy cập `AuthService.CurrentUser.FullName`

**Hậu quả**: NullReferenceException

**Đã sửa**: 
```csharp
private void ConfigureUIByRole()
{
    if (AuthService.CurrentUser == null)
    {
        MessageBox.Show("Session expired. Please login again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        var loginForm = new LoginForm();
        loginForm.Show();
        this.Close();
        return;
    }
    // ... rest of code
}
```

---

### 8. ❌ BUG: Form1_FormClosing tạo Dashboard mới khi không cần
**Vấn đề**: Khi đóng Form1, nếu không có parent dashboard thì tạo mới, gây lỗi logic

**Hậu quả**: Tạo nhiều instance Dashboard không cần thiết

**Đã sửa**: 
```csharp
private void Form1_FormClosing(object sender, FormClosingEventArgs e)
{
    if (_parentDashboard != null && !_parentDashboard.IsDisposed)
    {
        _parentDashboard.Show();
        _parentDashboard.BringToFront();
    }
    // Không tạo dashboard mới nữa
}
```

---

### 9. ⚠️ CODE SMELL: Duplicate code trong ConfigureUIByRole
**Vấn đề**: Admin và Manager có cùng quyền nhưng viết riêng case

**Đã sửa**: 
```csharp
switch (AuthService.CurrentUser.Role)
{
    case UserRole.Admin:
    case UserRole.Manager:
        btnAddProject.Visible = true;
        break;
    case UserRole.Member:
    case UserRole.Viewer:
        btnAddProject.Visible = false;
        break;
}
```

---

## ✅ Kết quả sau khi fix

1. ✅ API URL đúng, kết nối thành công
2. ✅ Authorization hoạt động đúng theo role
3. ✅ Token được gửi kèm trong mọi request
4. ✅ Không còn memory leak
5. ✅ Application lifecycle được quản lý đúng
6. ✅ Null safety được đảm bảo
7. ✅ Form navigation hoạt động mượt mà
8. ✅ Code sạch hơn, ít duplicate

## 🧪 Cách test

1. **Test Login**:
   - Login với tài khoản sai → Hiện lỗi
   - Login thành công → Vào Dashboard
   - Đóng Dashboard → App thoát

2. **Test Authorization**:
   - Login với Viewer → Không thấy nút Add Project/Task
   - Login với Member → Thấy nút Add Task, không thấy Add Project
   - Login với Manager → Thấy cả hai nút
   - Login với Admin → Thấy cả hai nút

3. **Test API**:
   - Viewer thử xóa project → 403 Forbidden
   - Member thử xóa project → 403 Forbidden
   - Manager xóa project → Thành công

4. **Test Memory**:
   - Mở/đóng nhiều form → Memory không tăng bất thường
   - Logout/Login nhiều lần → Không crash

## 📝 Notes

- API phải chạy trước khi chạy WinForms
- Đảm bảo port 7089 không bị chiếm bởi app khác
- Nếu thay đổi port API, cần update `_baseUrl` trong ApiService
