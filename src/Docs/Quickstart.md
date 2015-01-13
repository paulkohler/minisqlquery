Mini SQL Query Quick Start
==========================

This document is a quick overview of how to use **Mini SQL Query**.

**Mini SQL Query** from is a minimalist SQL query tool for multiple providers (MSSQL, Oracle, OLEDB, MS Access, SQLite etc). The goal of the Mini SQL Query tool is to allow a developer or trouble-shooter to quickly diagnose issues or make changes to a database using a tool with a small footprint, is portable, fast, flexible and easy to use.

Mini SQL Query is "deliberately minimalist". Software too often becomes bloated and less usable while trying to satisfy the 99% of what every user wants. Mini SQL Query aims to satisfy the most common tasks what the average user seeks to achieve, in doing so it keeps the size and complexity of the application to a minimum. 

The original application was developed almost entirely using Microsoft's Visual Studio C# Express IDE.


The First Run
-------------

The very first time you run Mini SQL Query, the application will look similar to the image below:

![Mini SQL Query on first run](https://github.com/paul-kohler-au/minisqlquery/blob/master/src/Docs/Mini-SQL-Query.png)
 
The first thing you need to do is **configure some sort of connection**. From the menu, select ***Edit > Edit connection strings***. 
 
There are some default connections present, they may not work on your system - they are simply examples. Each has a Name, a Provider and a Connection with optional comments. Selecting one of the items in the left list will display the details on the right.

![](https://github.com/paul-kohler-au/minisqlquery/blob/master/src/Docs/Mini-SQL-Query--Edit-Connection-Strings.png)

From this point add, modify or copy a connection. 

More...
-------

See:


- [Basic Usage]
- [Context Menu Commands]
- [Adding a MSSQL Connection]
- [Adding an SQLite Connection]
- [Thank You]


.