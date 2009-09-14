#foreach ($table in $Host.Model.Tables)
public class ${table.Name}
{
#foreach ($c in $table.Columns)
	public $c.DbType.SystemType $c.Name { get; set; }
#end
}
#end
