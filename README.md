# KPI
This is a simple application according to task for "ssc company" hiring process.
The solution contains:
• KPI : The Standalone TypeScript Angular template, for frontend
• KPIAPI: The ASP.NET Core Web API template, for backend

- the user can retrieve kpis in certain department, and 
showed them in table grid view.
- the table is "editable" , and the user can update in its cells.
- user can open new page,
  save current page
  delete current page


# Description

KPI: Front end side

	- the application consists of 
	1 component == appComponent
	that contains all ui, as it is a simple page and no need to divide it.

	, and a service file kpiService
	that contains the logic for http with API.

	- AngularMaterial is used for ui styling.
	
KPIAPI: back end side
	- the application consists of 
	Data folder: for models, migrations, data types and unit of work
	Controllers folder: that has the logic for API endpoints actions.
	Core folder: that has the logic for repository pattern implementation.
	
	Note: Althogh the task is simple and no real neeed to use repository pattern.
	I prefered to stick with best practice approach in this point.
	
## Getting started

### Create Db
In ssms :
"add a login account"

CREATE LOGIN KPIAdmin
 WITH PASSWORD = 'KPIAdmin$123'
GO

CREATE USER KPIAdmin
 FOR LOGIN KPIAdmin
 WITH DEFAULT_SCHEMA = dbo
GO
EXEC sp_addrolemember N'db_owner', N'KPIAdmin'
GO

### Build:
application can be build easily is visual studio 2022.


### Run the appliaction:
simply press F5 in visual studio.

