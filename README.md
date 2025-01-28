# RealEstateApp

This repository contains a complete real estate management application with a **.NET 9** backend and a **React.js** frontend.

---

## Requirements

- **Backend**: .NET SDK 9.0, MongoDB running on `mongodb://localhost:27017`
- **Frontend**: Node.js 16+ and npm 8+

---

## Repository Structure

```plaintext
/
├── backend/        # Backend code (RealEstateApp API)
└── frontend/       # Frontend code (React.js)
```
---

## Setup Instructions

### Backend

Navigate to the backend folder:
```bash
cd backend
```
Restore the dependencies:
```bash
dotnet restore
```
Compile and run the project over HTTPS on port 7161:
```bash
dotnet run --urls "https://localhost:7161"
```
The backend will be available at:
- **API Base URL**: [https://localhost:7161](https://localhost:7161)
- **Swagger URL**: [https://localhost:7161/swagger](https://localhost:7161/swagger)

---

### Frontend

Navigate to the frontend folder:
```bash
cd frontend
```
Install dependencies:
```bash
npm install
```
Start the application:
```bash
npm start
```
Access the frontend at:
```bash
http://localhost:3000
```
---

## Key URLs

- **Backend API**: [https://localhost:7161](https://localhost:7161)
- **Swagger**: [https://localhost:7161/swagger](https://localhost:7161/swagger)
- **Frontend**: [http://localhost:3000](http://localhost:3000)

---

## Notes

- The backend **must** run over HTTPS on port `7161` to ensure proper communication with the frontend.
- Ensure MongoDB is running locally on `localhost:27017`.
- The backend uses **.NET SDK 9.0**. Ensure you have the correct version installed.
