# Expense Tracker System - ASP.NET Core

A modern full-stack web application built with **ASP.NET Core** to efficiently manage personal expenses using current best practices.

---

## Project Screenshots

![Dashboard](./Screenshot/a.png)
![Add Expense](./Screenshot/b.png)
![Expense List](./Screenshot/c.png)

---

## Features

* **User Authentication**

  * Secure login and registration
  * Role-based access control

* **Expense Management**

  * Add, edit, delete, and view expense records
  * Display detailed information: amount, category, date, description
  * Filter expenses by category or date
  * Recurring expense management
  * Budget setting and tracking
  * Pagination for expense lists

* **Export Functionality**

  * Export expenses to CSV

---

## Tech Stack

### Frontend

* Razor Pages / MVC
* Tailwind CSS (if used)

### Backend

* ASP.NET Core
* SQL Server / SQLite
* Swagger for API testing

---

## Getting Started

### Prerequisites

* .NET SDK installed
* SQL Server / SQLite installed

### Installation

```bash
# Clone the repository
git clone https://github.com/rubayitalam/expense-tracker.git

# Navigate to project folder
cd expense-tracker

# Restore dependencies
dotnet restore

# Run the project
dotnet run
```

---

## API Testing

* Swagger UI available at: `https://localhost:5001/swagger`

---

## Project Structure

```bash
ExpenseTracker/
├─ ExpenseTracker.sln
├─ BLL/          # Business Logic Layer
├─ DAL/          # Data Access Layer
├─ Presentation/ # MVC/Razor Pages Layer
├─ Screenshot/   # Project screenshots
└─ README.md
```

---

## Security Features

* Password hashing (if implemented)
* Role-based access to sensitive routes
* Secure handling of user data

---

## Key Challenges & Solutions

1. **Authentication & Authorization**

   * Implemented secure login and registration
   * Role-based access control

2. **Expense Management**

   * CRUD operations with validation
   * Recurring expenses handled with proper logic

3. **Performance & Usability**

   * Pagination for large expense lists
   * Filtering by category/date for faster access

---

## Future Enhancements

* Analytics dashboard for spending trends
* Multi-user support with role hierarchy
* Notifications for budget limits
* Mobile-friendly responsive design
* Unit and integration testing

---

## Author

**Rubayit Alam** – Full-Stack Developer

GitHub: [https://github.com/rubayitalam](https://github.com/rubayitalam)
Email: [rubayit100@gmail.com](mailto:rubayit100@gmail.com)

---

## Project Description

**Expense Tracker** is a modern, user-friendly ASP.NET Core web application that allows users to efficiently manage personal finances. Users can add, edit, delete, filter, track recurring expenses, set budgets, and export data to CSV. The project demonstrates a clean full-stack architecture with BLL, DAL, and presentation layers.
