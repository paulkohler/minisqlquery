using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace MiniSqlQuery.Core.DbModel
{
	public interface ISqlWriter
	{
		void WriteCreate(TextWriter writer, DbModelColumn column);
		void WriteSummary(TextWriter writer, DbModelColumn column);
		void WriteSelect(TextWriter writer, DbModelTable tableOrView);
		void WriteInsert(TextWriter writer, DbModelTable tableOrView);
		void WriteUpdate(TextWriter writer, DbModelTable tableOrView);
		void WriteDelete(TextWriter writer, DbModelTable tableOrView);
	}
}