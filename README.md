# BookStoreAPI

## Overview
BookStoreAPI is a RESTful API for managing a bookstore's inventory, built with ASP .NET Core Framework. It provides features to manage books, authors, and categories efficiently.

## Features
- 📚 Book Management: Add, retrieve, update, and delete books.
- ✍️ Author Management: Manage author details.
- 🏷️ Category Management: Organize books into categories.
- 🔐 Token-based authentication.
- 📊 Logging & Exception Handling.

## Technologies Used
- **🖥 Programming Language:** C#
- **🛠 Framework:** ASP .NET Core
- **🗄 Database:** Microsoft SQL Server (MS SQL Server)
- **🔑 Authentication:** JWT
- **📌 Version Control:** Git & GitHub

## Getting Started
### Prerequisites
Ensure you have the following installed:
- .NET SDK (latest version)
- Microsoft SQL Server
- Docker (if containerization is used)
- Git

### Installation
1. **Clone the repository:**
   ```bash
   git clone https://github.com/abdullahazmy/BookStoreAPI.git
   cd BookStoreAPI
   ```
2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```
3. **Set up the database:**
   - Configure your database connection string in `appsettings.json`
   - Apply migrations:
     ```bash
     dotnet ef database update
     ```
4. **Run the API:**
   ```bash
   dotnet run
   ```

### API Documentation
API documentation is available using Swagger.
- Start the application and navigate to:
  ```
  http://localhost:5000/swagger
  ```

## Project Structure
```
BookStoreAPI/
│── Controllers/
│── Models/
│── Services/
│── Repositories/
│── Migrations/
│── appsettings.json
│── Program.cs
│── Startup.cs
│── README.md
```

## Contributing
🚀 Contributions are welcome! Please follow these steps:
1. **Fork** the repository
2. **Create** a new branch (`feature-branch`)
3. **Commit** your changes
4. **Push** the branch and create a **Pull Request**

## License
📜 BookStoreAPI is licensed under the **MIT License**.

## Contact
📩 For any inquiries, contact **Abdullah Azmy** at [your email].

