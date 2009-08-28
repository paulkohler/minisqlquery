ConnectionString: "$Model.ConnectionString"
ProviderName: "$Model.ProviderName"

#foreach ($table in $Model.Tables)
$table.Name
#foreach ($c in $table.Columns)
  * $c.Name
#end
#end
