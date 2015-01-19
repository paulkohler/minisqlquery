Adding a MSSQL Connection
=========================

Click *Add*.

Select a Provider, in this example I am leaving it as “System.Data.SqlClient”.

Enter a *Name* for the connection, e.g. “Adventure Works (local)”.

Set the Connection Strings “Integrated Security” value to “True”.

Set the Connection Strings “Data source” value to “localhost” (or “localhost\SQLEXPRESS” as required.)

Set the Connection Strings “Initial Catalog” either by using the dropdown or typing name straight in. 

***Note***

The dropdown will only work if the connection details are sufficient to query the datasource at the time, e.g. Integrated Security=True will typically suffice depending on your environment.

You can use the *"Test..."* button to confirm the connectivity.
 
Press *OK* and you will return to the “Database Connection List Editor” and you will see the new item in the list. Press *OK* here to return to Mini SQL Query.
 
Next select the new connection definition from the dropdown list on the toolbar. The application will pause while it loads the database schema information. Now you can explore the schema using the tree of database objects on the left. Keep in mind the “mini” in Mini SQL Query, the list of objects is by no means exhaustive! Currently the essentials such as tables, views and their associated column information are loaded. Depending on the provider foreign key information is also loaded.
 
Note – Foreign Keys
-------------------
The core schema engine is generic for all ADO.NET providers. The only thing not discoverable by the generic engine is the foreign key relationships. To retrieve foreign key information from a database, a provider specific implementation is required; currently there are two, Microsoft SQL Server and Microsoft’s SQL Server Compact Edition. These are typically driven by demand. Patches accepted!

Note – Tables and Views Only
----------------------------
Currently only table and view information is retrieved from the databases schema. Again, this is driven by demand. The number of times access to stored procedures has been too low to warrant implementation. Patches accepted!

[Quickstart](https://github.com/paul-kohler-au/minisqlquery/blob/master/src/Docs/Quickstart.md)
