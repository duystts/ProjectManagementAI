# 🤖 RAG Integration Setup

## 📋 Cấu hình

### 1. Google AI Studio API Key
1. Truy cập [Google AI Studio](https://aistudio.google.com/)
2. Tạo API key
3. Cập nhật `appsettings.json`:
```json
{
  "GoogleAI": {
    "ApiKey": "YOUR_ACTUAL_API_KEY_HERE"
  }
}
```

### 2. Database Migration
```bash
cd ProjectManagement.API
dotnet ef database update
```

## 🚀 Sử dụng RAG API

### 1. Thêm Knowledge
```http
POST /api/ai/knowledge
Authorization: Bearer {jwt_token}
Content-Type: application/json

{
  "title": "Project Setup Guide",
  "content": "Để setup project, cần cài đặt .NET 8, tạo database...",
  "projectId": 1,
  "taskId": null
}
```

### 2. Chat với RAG
```http
POST /api/ai/chat
Authorization: Bearer {jwt_token}
Content-Type: application/json

{
  "message": "Làm thế nào để setup project?",
  "projectId": 1,
  "taskId": null
}
```

## 🔧 Tính năng

- ✅ **Embedding**: Sử dụng Google AI text-embedding-004
- ✅ **Vector Search**: Cosine similarity với threshold 0.3
- ✅ **Context Filtering**: Theo ProjectId/TaskId
- ✅ **RAG Chat**: Gemini-1.5-flash với context
- ✅ **Source Tracking**: Hiển thị nguồn thông tin

## 📊 Workflow

1. **Add Knowledge** → Tạo embedding → Lưu vào DB
2. **User Query** → Tạo embedding → Tìm similar knowledge
3. **RAG Response** → Gửi context + query → Gemini → Trả về answer + sources

## 🎯 Use Cases

- **Project Documentation**: Lưu trữ và tìm kiếm tài liệu dự án
- **Task Instructions**: Hướng dẫn thực hiện task cụ thể  
- **Knowledge Base**: Tích lũy kiến thức team
- **Smart Assistant**: Trả lời câu hỏi dựa trên context