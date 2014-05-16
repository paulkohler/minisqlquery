ConnectionString: "${Host.Model.ConnectionString}"
ProviderName: "${Host.Model.ProviderName}"

#foreach ($table in ${Host.Model.Tables})
Table: ${table.FullName} (Row count: ${Data.Get(${table.Schema}, ${table.Name}).Rows.Count})
#foreach ($c in ${table.Columns})
  * Column.Name:     ${c.Name}
	DbType.Summary:  ${c.DbType.Summary}
		Name:        ${c.DbType.Name}
		Length:      ${c.DbType.Length}
		Precision:   ${c.DbType.Precision}
		Scale:       ${c.DbType.Scale}
		SystemType:  ${c.DbType.SystemType}
	Nullable:        ${c.Nullable}
	IsKey:           ${c.IsKey}
	IsUnique:        ${c.IsUnique}
	IsRowVersion:    ${c.IsRowVersion}
	IsIdentity:      ${c.IsIdentity}
	IsAutoIncrement: ${c.IsAutoIncrement}
	IsReadOnly:      ${c.IsReadOnly}
	IsWritable:      ${c.IsWritable}
	HasFK:           ${c.HasFK}
#if($c.HasFK)
	 ${c.ForeignKeyReference.ReferenceTable.FullName}.${c.ForeignKeyReference.ReferenceColumn.Name}
#end ## hasFK
#end ## foreach column
#end ## foreach table

