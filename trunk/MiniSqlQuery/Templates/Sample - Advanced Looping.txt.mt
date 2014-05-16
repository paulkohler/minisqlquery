## The following script demonstraits the advanced looping abilities using foreach.

#foreach ($row in ${Data.Get(null, "Categories").Rows})
#beforeall

	I am before everything
	
#before
		>>> (before each item)
#each
			"${Host.Data.ColumnValue($row, "Category Name")}"
#after
		<<< (after each item)
#between
	(i am between each line)
#odd
	[I am an odd row...]
#even
	[I am an even row]
#nodata
	(I appear if theres no data)
#afterall

	I am last.
	
#end
