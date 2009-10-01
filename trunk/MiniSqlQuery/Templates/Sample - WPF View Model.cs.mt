## Example WPF "View Model" template
## Assumes the use of something like http://mvvmfoundation.codeplex.com/ for the base classes etc

## NOTE - "WIP"

#foreach ($table in $Host.Model.Tables)
#set($classNm = ${Host.ToPascalCase($table.Name)} )
public class ${classNm}ViewModel : ObservableObject
{
	private ${classNm}Entity _entity;
	
	public ${classNm}ViewModel(${classNm}Entity entity)
	{
		_entity = entity;
	}
#foreach ($c in $table.Columns)
#set($nm=$Host.ToPascalCase($c.Name))

	public $c.DbType.SystemType.Name ${nm}
	{
		get { return _entity.${nm}; }
		set
		{
			if (_entity.${nm} != value)
			{
				_entity.${nm} = value;
				OnPropertyChanged("${nm}");
			}
		}
	}
#end

	//TODO - sample commands, save etc
}



#end