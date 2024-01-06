# BackOfficeApp

## Overview
BackOfficeApp is a comprehensive management system for a library's back-office operations. It allows administrators and employees to manage books, members (adherents), and lending transactions efficiently.

## Features
- **Employee Management**: Add, modify, or delete employee details. Manage roles and permissions.
- **Member Management**: Add or remove members, modify their details, and manage their memberships.
- **Book Catalog Management**: Add new books, update book details, and remove obsolete or damaged books.
- **Loan Management**: Record new book loans, track currently borrowed books, and manage returns.
- **Reservation Management**: Handle book reservations made by members.

## Getting Started
To get started with BackOfficeApp, follow these steps:

### Prerequisites
- .NET Framework
- SQL Server 

### Installation
1. Clone the repository to your local machine.
2. Open the solution in Visual Studio.
3. Restore NuGet packages.
4. Configure the database connection string in `app.config` (if you don't want to do this step use SQLEXPRESS and name your database 'bibliotheque')
5. Build and run the application.