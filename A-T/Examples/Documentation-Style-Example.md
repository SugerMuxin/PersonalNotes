---
title: "Project Phoenix - API Documentation"
version: "2.4.0"
lastUpdated: 2024-01-15
projectStatus: Active
authors:
  - Dev Team
  - Technical Writers
---

# Project Phoenix API Documentation

> **Version 2.4.0** | Last Updated: January 15, 2024

---

## 📖 Overview

**Project Phoenix** is a RESTful API for managing user authentication, data synchronization, and real-time notifications. Built with Node.js, Express, and MongoDB.

### Base URL

```
Development:  http://localhost:3000/api/v1
Staging:      https://staging-api.phoenix.com/v1
Production:   https://api.phoenix.com/v1
```

### Authentication

Most endpoints require a Bearer token:

```http
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

---

## 🚀 Quick Start

### 1. Get Your API Key

```bash
curl -X POST https://api.phoenix.com/v1/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "securePassword123!"
  }'
```

**Response:**

```json
{
  "success": true,
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "user": {
      "id": "64a7b8c9d0e1f2a3b4c5d6e7",
      "email": "user@example.com",
      "role": "user"
    }
  }
}
```

### 2. Make Your First Request

```bash
curl -X GET https://api.phoenix.com/v1/users/profile \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

---

## 📚 API Reference

### Authentication

#### Register User

Create a new user account.

```http
POST /auth/register
```

**Request Body:**

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `email` | string | ✅ Yes | Valid email address |
| `password` | string | ✅ Yes | Min 8 chars, 1 uppercase, 1 number |
| `name` | string | ❌ No | User's display name |

**Example Request:**

```json
{
  "email": "alice@example.com",
  "password": "SecurePass123",
  "name": "Alice Chen"
}
```

**Success Response (201):**

```json
{
  "success": true,
  "message": "User registered successfully",
  "data": {
    "user": {
      "id": "64a7b8c9d0e1f2a3b4c5d6e7",
      "email": "alice@example.com",
      "name": "Alice Chen",
      "createdAt": "2024-01-15T10:30:00Z"
    },
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  }
}
```

**Error Response (400):**

```json
{
  "success": false,
  "message": "Validation Error",
  "errors": [
    {
      "field": "email",
      "message": "Email already exists"
    }
  ]
}
```

---

#### Login

Authenticate existing user.

```http
POST /auth/login
```

**Request Body:**

| Field | Type | Required |
|-------|------|----------|
| `email` | string | ✅ Yes |
| `password` | string | ✅ Yes |

**Example Request:**

```json
{
  "email": "alice@example.com",
  "password": "SecurePass123"
}
```

**Success Response (200):**

```json
{
  "success": true,
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "tokenExpiresIn": 86400,
    "user": {
      "id": "64a7b8c9d0e1f2a3b4c5d6e7",
      "email": "alice@example.com",
      "lastLogin": "2024-01-15T10:30:00Z"
    }
  }
}
```

---

### Users

#### Get User Profile

Fetch current user's profile.

```http
GET /users/profile
```

**Headers:**

```
Authorization: Bearer {token}
```

**Success Response (200):**

```json
{
  "success": true,
  "data": {
    "id": "64a7b8c9d0e1f2a3b4c5d6e7",
    "name": "Alice Chen",
    "email": "alice@example.com",
    "avatar": "https://cdn.phoenix.com/avatars/alice.jpg",
    "preferences": {
      "theme": "dark",
      "notifications": true,
      "language": "en"
    },
    "stats": {
      "loginCount": 42,
      "lastActive": "2024-01-15T10:30:00Z"
    }
  }
}
```

**Error Response (401):**

```json
{
  "success": false,
  "message": "Unauthorized: Invalid or missing token"
}
```

---

#### Update User Profile

Update user information.

```http
PATCH /users/profile
```

**Headers:**

```
Authorization: Bearer {token}
```

**Request Body:**

| Field | Type | Required |
|-------|------|----------|
| `name` | string | ❌ No |
| `avatar` | string (URL) | ❌ No |
| `preferences` | object | ❌ No |

**Example Request:**

```json
{
  "name": "Alice Chen Jr.",
  "preferences": {
    "theme": "light",
    "language": "zh-CN"
  }
}
```

**Success Response (200):**

```json
{
  "success": true,
  "message": "Profile updated successfully",
  "data": {
    "id": "64a7b8c9d0e1f2a3b4c5d6e7",
    "name": "Alice Chen Jr.",
    "updatedAt": "2024-01-15T10:35:00Z"
  }
}
```

---

## 📊 Status Codes

| Code | Meaning | Description |
|------|---------|-------------|
| `200` | OK | Request succeeded |
| `201` | Created | Resource created successfully |
| `204` | No Content | Request succeeded, no content returned |
| `400` | Bad Request | Invalid request parameters |
| `401` | Unauthorized | Missing or invalid authentication |
| `403` | Forbidden | Insufficient permissions |
| `404` | Not Found | Resource does not exist |
| `409` | Conflict | Resource conflict (e.g., duplicate email) |
| `429` | Too Many Requests | Rate limit exceeded |
| `500` | Server Error | Internal server error |

---

## ⚠️ Errors

### Error Response Format

All error responses follow this structure:

```json
{
  "success": false,
  "message": "Error description",
  "errorCode": "ERROR_CODE",
  "errors": [
    {
      "field": "fieldName",
      "message": "Specific error message"
    }
  ]
}
```

### Common Error Codes

| Code | Message | Solution |
|------|---------|----------|
| `AUTH_001` | Invalid credentials | Check email/password |
| `AUTH_002` | Token expired | Refresh token |
| `AUTH_003` | Insufficient permissions | Check user role |
| `VAL_001` | Validation failed | Check request body |
| `RES_001` | Resource not found | Verify resource ID |
| `RATE_001` | Rate limit exceeded | Wait and retry |

---

## 🔄 Pagination

List endpoints support pagination via query parameters:

```http
GET /users?page=2&limit=20&sort=-createdAt
```

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `page` | integer | 1 | Page number |
| `limit` | integer | 10 | Items per page (max 100) |
| `sort` | string | createdAt | Sort field (prefix with `-` for descending) |

**Response:**

```json
{
  "success": true,
  "data": [
    { /* user 1 */ },
    { /* user 2 */ }
  ],
  "pagination": {
    "currentPage": 2,
    "totalPages": 5,
    "totalItems": 48,
    "itemsPerPage": 10,
    "hasNext": true,
    "hasPrevious": true
  }
}
```

---

## ⚡ Rate Limiting

| Plan | Requests | Time Window |
|------|----------|-------------|
| **Free** | 100 | 1 hour |
| **Pro** | 1000 | 1 hour |
| **Enterprise** | Unlimited | - |

Rate limit headers are included in every response:

```http
X-RateLimit-Limit: 100
X-RateLimit-Remaining: 73
X-RateLimit-Reset: 1705305600
```

---

## 🧪 Testing the API

### Postman Collection

Download our [Postman Collection](./collections/phoenix-api.json) for ready-to-use requests.

### cURL Examples

```bash
# Register new user
curl -X POST https://api.phoenix.com/v1/auth/register \
  -H "Content-Type: application/json" \
  -d '{"email":"test@example.com","password":"Test123!"}'

# Get user profile
curl -X GET https://api.phoenix.com/v1/users/profile \
  -H "Authorization: Bearer YOUR_TOKEN"

# Update profile
curl -X PATCH https://api.phoenix.com/v1/users/profile \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"name":"Updated Name"}'
```

### JavaScript (Fetch)

```javascript
// Login
const response = await fetch('https://api.phoenix.com/v1/auth/login', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    email: 'user@example.com',
    password: 'password123'
  })
});

const { data } = await response.json();
const token = data.token;

// Use token for subsequent requests
const profile = await fetch('https://api.phoenix.com/v1/users/profile', {
  headers: { 'Authorization': `Bearer ${token}` }
});
```

---

## 📞 Support

- **Documentation**: [docs.phoenix.com](https://docs.phoenix.com)
- **Status Page**: [status.phoenix.com](https://status.phoenix.com)
- **Email**: api-support@phoenix.com
- **GitHub Issues**: [github.com/phoenix/api/issues]

---

**© 2024 Phoenix Technologies. All rights reserved.**
