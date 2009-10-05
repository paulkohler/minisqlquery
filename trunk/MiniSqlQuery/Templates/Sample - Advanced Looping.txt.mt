## The following script demonstraits the advanced looping abilities using foreach.

#foreach ($row in ${Data.Get("Categories").Rows})

${Host.Data.ColumnValue($row, "Category ID")}
${Host.Data.ColumnValue($row, "Category Name")}

#end
