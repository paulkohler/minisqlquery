ConnectionString: "${Host.Model.ConnectionString}"
ProviderName: "${Host.Model.ProviderName}"

#foreach ($table in ${Host.Model.Tables})
Table: ${table.Name} (Row count: ${Data.Get($table.FullName).Rows.Count})
#foreach ($c in ${table.Columns})
  * ${c.Name}
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
	 ${c.ForiegnKeyReference.ReferenceTable.FullName}.${c.ForiegnKeyReference.ReferenceColumn.Name}
#end

#end

#end
