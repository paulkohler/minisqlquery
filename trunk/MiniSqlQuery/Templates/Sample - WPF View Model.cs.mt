## Example WPF "View Model" template
## Assumes the use of something like http://mvvmfoundation.codeplex.com/ for the base classes etc

## NOTE - "WIP"

#foreach ($table in $Host.Model.Tables)
#set($classNm = ${table.Name.Replace(" ", "")} )
##set($classNm= ${classNm}+"ViewModel" )
public class ${classNm}ViewModel : ObservableObject
{
	private ${classNm}Entity _entity;
	
#foreach ($c in $table.Columns)
#set($nm=$c.Name.Replace(" ", ""))
	private $c.DbType.SystemType.Name _${nm};
#end
	
	public ${classNm}ViewModel(${classNm}Entity entity)
	{
		_entity = entity;
	}
	
#foreach ($c in $table.Columns)
#set($nm=$c.Name.Replace(" ", ""))
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
}



#end