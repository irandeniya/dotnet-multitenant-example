# .Net Core 5 and SQL server based Multi-tenant application example

This repository uses an SQL Server as the DBMS and manages multiple databases to treat each individual site. 
Using a cookie to store and track the user's site. At the end of the application, we maintain Tenant Service and initialize DB context based on the user preferences. 