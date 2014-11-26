#@get TableName
IF OBJECT_ID('${TableName}', 'U') IS NOT NULL
  DROP TABLE [${TableName}]

CREATE TABLE [${TableName}]
(
	${TableName}Id INT IDENTITY NOT NULL, 
    CONSTRAINT PK_${TableName}Id PRIMARY KEY (${TableName}Id)
)
