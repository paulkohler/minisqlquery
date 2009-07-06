using System;

namespace MiniSqlQuery.Core
{
	public class BatchProgressEventArgs : EventArgs 
	{
		public BatchProgressEventArgs(Query query, int count, int index)
		{
			Query = query;
			Count = count;
			Index = index;
		}

		public Query Query { get; private set; }
		public int Count { get; private set; }
		public int Index { get; private set; }
	}
	
}