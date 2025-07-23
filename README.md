# Priority Setup Management System

A **full-stack web application** built with **React.js** (frontend) and **ASP.NET Core** (backend).  
The app manages priority setup configurations across **_Payers, Locations, Ageing Buckets_, and _Priority Types_**, following a clean **code-first approach** for the backend data model and supporting seamless **switching between mock and real database** modes.

---

## 📋 Overview

This project was developed as part of a **technical assessment** (see screenshots 1–4 in the attached document for expected UI flows).  
The system provides:
- **Full CRUD operations** for Priority Setups.
- **Frontend UI** (React) with **search, toggle, export, and modal add**.
- **Backend API** (ASP.NET Core) with **code-first Entity Framework Core** migrations for database setup.
- **Support for both mock (in-memory) and real (SQL Server) data** for rapid development, testing, or production.

---

## ⚙️ Technology Stack

| Layer         | Technology              | Details                                   |
|---------------|-------------------------|-------------------------------------------|
| **Frontend**  | React.js                | Hooks, Context, Fetch API                 |
| **UI**        | Bootstrap 5 + CSS       | Responsive, clean layouts                 |
| **Backend**   | ASP.NET Core            | REST API, Swagger documentation           |
| **Data**      | Entity Framework Core   | **Code-first** migrations                 |
| **Database**  | SQL Server (or mock)    | Switch between real and in-memory data    |
| **Styling**   | CSS                     | Custom styles for compact, modern look    |

---

## 📂 Project Structure

/priority-setup-app/
│
├── client/ # React frontend
│ ├── public/
│ ├── src/
│ │ ├── components/ # Reusable UI components
│ │ │ ├── AddPriorityForm.jsx # Modal for adding new record
│ │ │ ├── PriorityList.jsx # Table with search/toggle/export
│ │ │ ├── SearchBox.jsx # Global filter
│ │ ├── App.js # Main app logic, data fetch
│ │ └── App.css # Base styling
│ ├── package.json
│ └── README-react.md
│
├── Server/ # ASP.NET Core backend
│ ├── Controllers/
│ │ ├── PrioritySetupController.cs
│ │ └── MasterDataController.cs
│ ├── Data/
│ │ └── AppDbContext.cs # DbContext (EF Core)
│ ├── Models/
│ │ ├── PrioritySetup.cs
│ │ ├── Payers.cs
│ │ ├── Locations.cs
│ │ ├── AgeingBucket.cs
│ │ └── PriorityType.cs
│ ├── Services/
│ │ ├── IPrioritySetupService.cs
│ │ └── PrioritySetupService.cs
│ ├── Mocks/
│ │ └── PrioritySetupDemoService.cs # Mock service for demo
│ ├── appsettings.json
│ ├── Program.cs
│ ├── Properties/
│ │ └── launchSettings.json
│ └── README-api.md
│
├── .gitignore
├── README.md (YOU ARE HERE)
└── NetCore_SQL_Assesment.docx # Original assignment document


---

## 🚀 Getting Started (Step-by-Step)

### 1. **Clone the Project**

**Open a terminal and run:**
git clone https://github.com/TheGabbarCoder/DemoApplicationJindalX.git
cd DemoApplicationJindalX


---

### 2. **Backend Setup**

#### A. Open the Backend Solution
- Open `Server/` in **Visual Studio** or **VS Code**.
**- Restore dependencies:**
cd Server
dotnet restore


#### B. Database Setup (Code-First Approach)
You can use either **SQL Server** (recommended for production) or **mock data** (for demo/testing):

##### **Option A: Real Database (SQL Server)**
1. **Add your connection string** to `appsettings.json`:
"ConnectionStrings": {
"DefaultConnection": "Server=localhost;Database=PrioritySetup;Trusted_Connection=True;MultipleActiveResultSets=true"
}

2. **Apply migrations** to create the database (code-first):
dotnet ef migrations add InitialCreate
dotnet ef database update

(This creates all tables: PrioritySetup, Payers, Locations, AgeingBucket, PriorityType.)
3. **Run the API**:
dotnet run


##### **Option B: Mock Data (No Database Required)**
- **Comment out** the real service registration in `Program.cs` and **uncomment** the mock service:
// For real database
// builder.Services.AddScoped<IPrioritySetupService, PrioritySetupService>();
// For mock data (demo/interview)
builder.Services.AddSingleton<IPrioritySetupService, PrioritySetupDemoService>();

- **Run the API**:
dotnet run


#### C. **Test the API**
- Visit [http://localhost:5193/swagger](http://localhost:5193/swagger) to explore and test endpoints.
- The API is now ready to serve the frontend.

---

### 3. **Frontend Setup**

#### A. Install Dependencies
cd client
npm install


#### B. **Configure Proxy**
Ensure `client/package.json` has:
"proxy": "http://localhost:5193"

This tells React to proxy API calls to your backend.

#### C. **Start the React App**
npm start

This opens your browser at [http://localhost:3000](http://localhost:3000).

---

### 4. **Using the Application**

- **View Records:** The main table loads all Priority Setup entries.
- **Add Record:** Click “Add Priority Setup”, fill the form, and save—the new record appears in the table.
- **Search:** Use the search box to filter all visible columns.
- **Toggle Status:** Activate/Deactivate rows with one click.
- **Export:** Select rows and click “Export to CSV” to download records.
- **Drop-downs:** All dropdowns (Payer, Location, Ageing Bucket, Priority Type) load master data from the API.

---

## 🔧 Features Mapped to Assessment Requirements

| **Task from NetCore_SQL_Assesment.docx**           | **Implementation**                          |
|----------------------------------------------------|---------------------------------------------|
| **Display Priority Setup Records**                 | Table shows all records (mock or real)      |
| **Add New Record**                                 | Modal form with validation                  |
| **Dropdowns from Master Data**                     | API: `/api/master/payers`, `/locations`, `/ageing-buckets`, `/priority-types` |
| **Total Balance Entry**                            | Input field for total balance               |
| **Active/Inactive Toggle**                         | Toggle button + status display              |
| **Global Search**                                  | Filters all visible columns                 |
| **Export Selected/All to CSV**                     | Backend API + download in browser           |

---

## 🧪 Testing the Application

### **Frontend Testing**
- **Add/Edit/Delete**: Ensure new records appear, can be toggled, and exported.
- **Search**: Verify the search box filters any column.
- **Mock vs Real**: Switch between mock and database modes (see above) to confirm both work.

### **Backend Testing**
- **Swagger**: Test all endpoints at `localhost:5193/swagger`.
- **Database Migrations**: If using real DB, confirm tables exist in SQL Server.

### **Integration Testing**
- **Ensure React can talk to .NET API**: Both servers must run simultaneously.
- **Verify dropdowns are populated**: Check `/api/master/...` endpoints.

---

## 📜 Project Documentation

- **Assessment Requirements**: See screenshots and requirements in `NetCore_SQL_Assesment.docx`.
- **Code-first**: Database schema is managed entirely via EF Core migrations in the `Server` project.
- **Mock/Real Switch**: Easily toggle between in-memory mock and real SQL Server data.
- **UI/UX**: Follows assessment images for layout, buttons, and workflow.

---

## 🤝 Thank You

Thank you for the opportunity to work on this assessment. It was a valuable experience in applying modern full-stack development practices, clean architecture, and code-first data modeling. I look forward to your feedback and any opportunities to contribute further.

— **Indu Mauli Tiwari**

---
