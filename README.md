# E-Shopping Microservices Project

Welcome to the **E-Shopping Microservices Project**, a comprehensive implementation of scalable, secure, and efficient Microservices architecture using **.Net Core 8**, **Clean Architecture**, and **Angular 18**. This project is the result of the practical application of the concepts and tools taught in the course: **"Creating .Net Core Microservices using Clean Architecture using .Net 8 & Angular 18."**

---

## 🚀 Features

- **Microservices Architecture**: Modular, scalable, and fault-tolerant system design.
- **Clean Architecture**: Clear separation of concerns for maintainability and scalability.
- **Secure Communication**: Authentication and Authorization using **Azure AD** / **Identity Server 4**.
- **Efficient Messaging**: Asynchronous messaging with **RabbitMQ**.
- **High-Performance Communication**: Use of **GRPC** for inter-service communication.
- **Service Mesh Management**: Traffic control and observability with **Istio**.
- **Multi-Database Support**: Integration with **SQL Server**, **MongoDB**, **PostgreSQL**, and **Redis**.
- **Dynamic API Gateway**: Powered by **Azure API Gateway**, **Ocelot**, and **Nginx**.
- **Kubernetes Deployment**: Seamless local and cloud deployment with **Helm Charts** and **AKS**.
- **Advanced UI**: Fully functional **Angular 18** frontend for eCommerce applications.

---

## 📂 Project Structure

This project follows a clean architecture approach with modularized components for better scalability and maintainability:

- **Core**: Business logic and domain entities.
- **Infrastructure**: Database access, external APIs, and messaging.
- **Application**: Use cases, services, and DTOs.
- **Presentation**: Frontend built with Angular 18.

---

## 🛠️ Technology Stack

| Technology           | Purpose                                             |
|-----------------------|-----------------------------------------------------|
| .NET Core 8           | Backend Development                                |
| Angular 18            | Frontend Development                               |
| Docker & Kubernetes   | Containerization and Orchestration                 |
| Azure AD              | Authentication & Authorization                     |
| RabbitMQ              | Message Queue for Communication                    |
| GRPC                  | High-Performance Service Communication             |
| Istio                 | Service Mesh for Traffic Control and Observability |
| SQL Server            | Relational Database                                |
| MongoDB               | NoSQL Database                                     |
| Redis                 | Caching and Pub/Sub                                |
| Azure API Gateway     | API Gateway                                        |
| Helm Charts           | Kubernetes Deployment                              |

---

## 🎯 Course Overview

This project aligns with the following course chapters:

1. **E-Shopping Vision**: Setting up Microservices using Docker, Kubernetes, and more.
2. **Security**: Implementing Identity Server 4 for authentication and authorization.
3. **Cross-Cutting Concerns**: Logging, caching, and validation.
4. **Versioning**: Managing updates with various versioning strategies.
5. **Frontend Development**: Building a dynamic E-commerce UI with Angular 18.
6. **Deployment**: Scaling and deploying to **Azure Kubernetes Service (AKS)**.

---

## 💡 Key Learning Outcomes

- Build scalable and secure Microservices applications using industry best practices.
- Hands-on experience with modern tools like Docker, Kubernetes, Istio, and RabbitMQ.
- Develop, deploy, and manage E-commerce platforms with ease.

---

## 📖 Getting Started

### Prerequisites

### Prerequisites

- Install [.NET SDK 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Install [Node.js](https://nodejs.org/)
- Install [Docker](https://www.docker.com/)
- Install [Kubernetes](https://kubernetes.io/)

### Running the Application

1. Clone the repository:
   ```bash
   https://github.com/SAADRmili/Eshopping.git
   cd Eshopping
   docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d
   
