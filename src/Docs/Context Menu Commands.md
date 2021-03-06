Context Menu Commands
=====================

Following is a brief explanation of the commands available on the context menu of the DB Inspector.

View Table Data
---------------

To view the contents of a table, either:

- Right-click the name of the table in the DB Inspector and select *View table data*
- From the *Plugins* menu, select *View table... (Ctrl+T)* and then choose a table name from the dropdown list or type it in. 

Use *F5* to refresh or click the “Reload Table” link button.

Below is an example of data loaded by the tool. The date format can be modified via the *Edit - Options* menu item.
 
*NOTE – Exporting Data*

An interesting addition here is the “Export Script...” link. It will convert the contents of the window into insert statements. By default it will observe _identity_ or _timestamp_ columns for example and not add those columns to the insert statements. If you want these values output the option can be changed. This can be very useful with setting up test data for example. With respect to MSSQL the “SET IDENTITY_INSERT <tablename> ON” command can be used to insert the data.

Generate Select Statement
-------------------------

Make sure you have an active edit window in focus (Control+N) and right click a table and select “Generate Select Statement”. SQL code similar to below will be generated.

    SELECT
    	JobCandidateID,
    	EmployeeID,
    	Resume,
    	ModifiedDate
    FROM HumanResources.JobCandidate
    
Generate Select COUNT(*) Statement
----------------------------------

No prize for guessing what this does. Make sure you have an active edit window in focus (Control+N) and right click a table and select “Generate Select COUNT(*) Statement”. SQL code similar to below will be generated. 

    SELECT COUNT(*) FROM HumanResources.EmployeeAddress

Generate Insert Statement
-------------------------

Make sure you have an active edit window in focus (Control+N) and right click a table and select “Generate Insert Statement”. SQL code similar to below will be generated. 
When an insert statement is generated the tables’ schema is used to ignore columns that are “read-only”. Examples are identity or timestamp columns. Also note the default values and comments to assist filling out the statement. The default for a GUID column is an empty GUID, if you need a new one generated use the “Insert GUID” menu item from the Plugins menu. Dates are a bit of a can of worms so I opted for a question mark (sorry!)

	INSERT INTO HumanResources.Employee
		(NationalIDNumber,
		ContactID,
		LoginID,
		ManagerID,
		Title,
		BirthDate,
		MaritalStatus,
		Gender,
		HireDate,
		SalariedFlag,
		VacationHours,
		SickLeaveHours,
		CurrentFlag,
		rowguid,
		ModifiedDate)
	VALUES
		(N'' /*NationalIDNumber,nvarchar(15)*/,
		0 /*ContactID,int*/,
		N'' /*LoginID,nvarchar(256)*/,
		null /*ManagerID,int*/,
		N'' /*Title,nvarchar(50)*/,
		'?' /*BirthDate,datetime*/,
		N'' /*MaritalStatus,nchar(1)*/,
		N'' /*Gender,nchar(1)*/,
		'?' /*HireDate,datetime*/,
		0 /*SalariedFlag,bit*/,
		0 /*VacationHours,smallint*/,
		0 /*SickLeaveHours,smallint*/,
		0 /*CurrentFlag,bit*/,
		'00000000-0000-0000-0000-000000000000' /*rowguid,uniqueidentifier*/,
		'?' /*ModifiedDate,datetime*/)

Generate Update Statement
-------------------------

Make sure you have an active edit window in focus (Control+N) and right click a table and select “Generate Update Statement”. SQL code similar to below will be generated. 
As with the insert statement generation, the tables’ schema is used to ignore columns that are “read-only”. Also, the primary key columns go into the where clause with comments to assist.

	UPDATE HumanResources.EmployeeDepartmentHistory
	SET
		EndDate = null,
		ModifiedDate = '?'
	WHERE
		EmployeeID =  /*value:EmployeeID,int*/ AND
		DepartmentID =  /*value:DepartmentID,smallint*/ AND
		ShiftID =  /*value:ShiftID,tinyint*/ AND
		StartDate = /*value:StartDate,datetime*/

Generate Delete Statement
-------------------------

Make sure you have an active edit window in focus (Control+N) and right click a table and select “Generate Delete Statement”. SQL code similar to below will be generated. 
As with the update statement generation, the tables’ schema is used to create a where clause with the primary key columns.

	DELETE FROM
		HumanResources.Department
	WHERE
		DepartmentID = /*value:DepartmentID*/

Copy Table Name
---------------

This command copies the fully qualified table name to the windows clipboard.

Truncate Table
--------------

This command deletes all the rows from the selected table. The truncate statement is not actually used currently due to the requirements (no foreign keys) and cross database support so in essence the resulting command is just “DELETE FROM *table*”. Patches welcome!

The truncate table command also sends an application level message so that if you are viewing a table with the view data command, the contents of that window is refreshed.

[Quickstart](https://github.com/paul-kohler-au/minisqlquery/blob/master/src/Docs/Quickstart.md)
