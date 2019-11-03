#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
    /// <summary>The i template editor.</summary>
    public interface ITemplateEditor : IPerformTask
    {
        /// <summary>The run template.</summary>
        void RunTemplate();
    }
}