# Dumpling Shop

A full-stack web application for a dumpling shop built with Vue.js frontend and C# backend.

## Table of Contents
- [Description](#description)
- [Technologies](#technologies)
- [Installation](#installation)
- [Launch](#launch)

## Description
Dumpling Shop is a web application that provides an interface for managing and ordering dumplings. The project consists of a Vue.js frontend for the user interface and a C# backend for business logic and data management.

## Technologies
- Frontend:
  - Vue.js (42.5%)
  - SCSS (10.1%)
  - JavaScript (9.9%)
- Backend:
  - C# (35.6%)
- Deployment:
  - Docker (1.9%)

## Installation

### Prerequisites
- Node.js (latest LTS version)
- .NET SDK
- Docker (optional, for containerized deployment)

### Frontend Setup
1. Navigate to the frontend directory:
```bash
cd frontend
```

2. Install dependencies:
```bash
npm install
```

### Backend Setup
1. Navigate to the backend directory:
```bash
cd backend
```

2. Restore .NET packages:
```bash
dotnet restore
```

## Launch

### Development Mode

#### Frontend
1. Start the Vue.js development server:
```bash
npm run serve
```
The frontend will be available at `http://localhost:8080`

#### Backend
1. Start the C# backend:
```bash
dotnet run
```
The API will be available at `http://localhost:5000`

### Using Docker
If you prefer to use Docker:
```bash
docker-compose up --build
```

This will start both the frontend and backend services in containers.