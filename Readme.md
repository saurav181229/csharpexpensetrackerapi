# 💰 Expense Tracker API  
> A full-stack backend project showcasing real-world software engineering principles using **.NET 8**, **Entity Framework Core**, **PostgreSQL**, **Docker**, and **CI/CD pipelines**.

---

## 🧾 Overview

This project started as a simple **file-based expense tracker** and evolved into a **cloud-deployed, containerized API** with authentication, database persistence, and automated deployment.

It demonstrates:
- Clean backend architecture  
- Authentication and authorization (JWT)  
- CI/CD automation  
- Cloud deployment using Docker containers  

---

## 🧠 Tech Stack & Tools

| Layer | Technology |
|-------|-------------|
| **Backend** | ASP.NET Core (.NET 8) |
| **Database** | PostgreSQL (Render Cloud) |
| **ORM** | Entity Framework Core |
| **Authentication** | JWT (JSON Web Tokens) |
| **DevOps** | Docker, Docker Compose |
| **CI/CD** | GitHub Actions + Docker Hub |
| **Deployment** | Render |
| **Docs** | Swagger / OpenAPI |

---

## 🏗️ Architecture

```text
Controllers (Presentation Layer)
        ↓
Services (Business Logic)
        ↓
Data Layer (EF Core + Repository Pattern)
        ↓
PostgreSQL (Containerized or Cloud)
