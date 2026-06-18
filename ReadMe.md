# ComicCrazy API

A RESTful API for tracking comic book collections built with ASP.NET Core 8, 
Entity Framework Core, and MySQL.

## Tech Stack
- ASP.NET Core 8
- Entity Framework Core
- MySQL
- JWT Authentication
- BCrypt password hashing

## Features
- Browse a comic catalogue
- User registration and login with JWT
- Role-based access (Admin/User)
- Personal collection tracker per user

## Endpoints

### Auth
| Method | Route | Access | Description |
|--------|-------|--------|-------------|
| POST | /api/auth/register | Public | Create an account |
| POST | /api/auth/login | Public | Login and get a JWT token |

### Comics
| Method | Route | Access | Description |
|--------|-------|--------|-------------|
| GET | /api/comics | Public | Get all comics |
| GET | /api/comics/{id} | Public | Get a single comic |
| POST | /api/comics | Admin | Add a comic |
| PUT | /api/comics/{id} | Admin | Update a comic |
| DELETE | /api/comics/{id} | Admin | Delete a comic |

### Collection
| Method | Route | Access | Description |
|--------|-------|--------|-------------|
| GET | /api/collection | User | View your collection |
| POST | /api/collection/{comicId} | User | Add a comic to your collection |
| PUT | /api/collection/{comicId} | User | Update a comic's status |
| DELETE | /api/collection/{comicId} | User | Remove from collection |

## Getting Started

1. Install .NET 8 SDK and MySQL
2. Update the connection string in `appsettings.json`
3. Run migrations: 
	dotnet ef database update
4. Run app
	dotnet run
5. Open Swagger at `https://localhost:{port}/swagger`

## Future Features
- Reviews and ratings
- Discussion threads
- Comic metadata from ComicVine API