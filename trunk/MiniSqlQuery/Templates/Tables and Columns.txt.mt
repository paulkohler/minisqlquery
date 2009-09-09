ConnectionString: "${Model.ConnectionString}"
ProviderName: "${Model.ProviderName}"

#foreach ($table in ${Model.Tables})
${table.Name} (Row count: ${Data.Get($table.FullName).Rows.Count})
#foreach ($c in ${table.Columns})
  * ${c.Name}
#end
#end
