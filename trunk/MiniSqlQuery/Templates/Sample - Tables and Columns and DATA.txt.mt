ConnectionString: "${Host.Model.ConnectionString}"
ProviderName: "${Host.Model.ProviderName}"

#foreach ($table in ${Host.Model.Tables})
Table Data: ${table.Name} (Row count: ${Data.Get($table.FullName).Rows.Count})

#foreach ($c in ${table.Columns})
#set($dataTable = $Host.Data.Get(${table.Schema}, ${table.Name}))
#foreach ($row in $dataTable.Rows)
#foreach ($c in ${table.Columns})
${c.Name}: ${Host.Data.ColumnValue($row, $c.Name)}
#end ## table columns
#end ## table rows

#end ## foreach column
#end ## foreach table

