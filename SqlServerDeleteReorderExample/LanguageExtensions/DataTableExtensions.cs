using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SqlServerDeleteReorderExample.LanguageExtensions
{
    public static class DataTableExtensions
    {
        public static void ReorderPositionMarker(this DataTable sender)
        {
            if (!sender.Columns.Contains("RowPosition")) return;

            var indexer = 1;

            var rows = sender.AsEnumerable().Where(row => row.RowState != DataRowState.Deleted).ToList();

            for (var rowIndex = 0; rowIndex < rows.Count; rowIndex++)
            {
                rows[rowIndex].SetField("RowPosition", indexer);
                indexer += 1;
            }
            sender.AcceptChanges();
        }       
    }
}
