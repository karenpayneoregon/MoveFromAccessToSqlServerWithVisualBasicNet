### Moving from MS-Access to SQL-Server with VB.NET

Within this repository contains an ever-increasing number of projects walkthroughs for taking VB.NET code working with MS-Access data operations to working with SQL-Server data operations. The objective for these code samples is to provide a path for developers to migrate MS-Access solutions to SQL-Server for Windows Forms, WPF and finally web solutions.

To start, basic table layouts will be used building to master-detail then to grand father master to parent master to child master. 

There will be discussions in many of the projects that will discuss that the project is looking to solve along with best practices.

The first project in this respository is well out of place as it's much more advance then a starter project but has been placed here to show this is not simply for beginner programmers. Some Entity Framework samples will be also added before their time in the lesson plan for those who are ready now to get an idea of how to work with Entity Framework. With that mentioned don't try to jump passed starter project as these code samples past basic levels can become overwhleming.

**This repository is a work in progress**

#### Requirements
- Basic knowledge working with databases, for basic syntax see [the following page](https://www.w3schools.com/sql/default.asp).
- Microsoft [Visual Studio](https://visualstudio.microsoft.com/) 2017 or higher
- Target Framework 4.5 or higher
- Microsoft [SQL-Server 2012](https://www.microsoft.com/en-us/sql-server/sql-server-2017?&OCID=AID739534_SEM_qJcWCEdr&MarinID=sqJcWCEdr_258104131131_microsoft%20sql%20server_e_c__49923480701_aud-394034018130:kwd-294748417622_) and higher
- Microsoft SSMS ([SQL-Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-2017)), optional but highly recommended

### Building the solution
- Before these project can run execute the attached scripts under Solution Explorer, solution items either in Visual Studio or in [SSMS](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-2017). 
- For SQL-Server operations change the connection string server name from KARENS-PC to the name of your server or if using SQL-Express use .\SQLEXPRESS.

### References
- [Migrate an Access database to SQL Server](https://support.office.com/en-us/article/migrate-an-access-database-to-sql-server-7bac0438-498a-4f53-b17b-cc22fc42c979).
- [Walkthrough: Manipulating Data (Visual Basic)](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql/linq/walkthrough-manipulating-data-visual-basic).
- [Microsoft Entity Framework](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ef/overview).
- [System.Data.SqlClient Namespace](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient?view=netframework-4.8).
- [System.Data.OleDb Namespace](https://docs.microsoft.com/en-us/dotnet/api/system.data.oledb?view=netframework-4.8).
- NuGet package [BaseConnectionLibrary](https://www.nuget.org/packages/BaseConnectionLibrary/1.0.3#).
- GitHub repository [Working with MS-Access with VB.NET](http://example.com).
