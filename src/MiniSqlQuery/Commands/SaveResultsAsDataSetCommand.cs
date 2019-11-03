#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.Data;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using MiniSqlQuery.Core.Forms;

namespace MiniSqlQuery.Commands
{
    /// <summary>The save results as data set command.</summary>
    public class SaveResultsAsDataSetCommand
        : CommandBase
    {
        /// <summary>Initializes a new instance of the <see cref="SaveResultsAsDataSetCommand"/> class.</summary>
        public SaveResultsAsDataSetCommand()
            : base("Save Results as DataSet XML...")
        {
            SmallImage = ImageResource.table_save;
        }

        /// <summary>Execute the command.</summary>
        public override void Execute()
        {
            IQueryBatchProvider batchProvider = HostWindow.ActiveChildForm as IQueryBatchProvider;

            if (batchProvider == null)
            {
                HostWindow.DisplaySimpleMessageBox(null, "No results to save as a 'DataSet'.", "Save Results as DataSet XML Error");
            }
            else
            {
                DataSet ds = null;

                if (batchProvider.Batch != null)
                {
                    if (batchProvider.Batch.Queries.Count > 1)
                    {
                        BatchQuerySelectForm querySelectForm = Services.Resolve<BatchQuerySelectForm>();
                        querySelectForm.Fill(batchProvider.Batch);
                        querySelectForm.ShowDialog();
                        if (querySelectForm.DialogResult == DialogResult.OK)
                        {
                            ds = querySelectForm.SelectedQuery.Result;
                        }
                    }
                    else if (batchProvider.Batch.Queries.Count == 1)
                    {
                        ds = batchProvider.Batch.Queries[0].Result;
                    }
                }

                if (ds == null)
                {
                    return;
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = "Save Results as DataSet XML";
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                    saveFileDialog.Filter = Properties.Settings.Default.XmlFileDialogFilter;

                    if (saveFileDialog.ShowDialog(HostWindow.Instance) == DialogResult.OK)
                    {
                        ds.WriteXml(saveFileDialog.FileName, XmlWriteMode.WriteSchema);
                        string msg = string.Format("Saved results to file: '{0}'", saveFileDialog.FileName);
                        HostWindow.SetStatus(HostWindow.ActiveChildForm, msg);
                    }
                }
            }
        }
    }
}
