using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlServerDeleteReorderExample.Classes;
using SqlServerDeleteReorderExample.LanguageExtensions;
using static SqlServerDeleteReorderExample.Classes.Dialogs;

namespace SqlServerDeleteReorderExample
{
    public partial class Form1 : Form
    {
        private readonly BindingSource _bindingSource = new BindingSource();
        private readonly DataOperations _dataOperations = new DataOperations();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _bindingSource.DataSource = _dataOperations.ReadPeople();
            dataGridView1.DataSource = _bindingSource;
            FormClosing += Form1_FormClosing;
        }
        /// <summary>
        /// Perform update for RowPosition field on all existing rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((DataTable)_bindingSource.DataSource).ReorderPositionMarker();
            _dataOperations.UpdateAllRowsPosition(((DataTable)_bindingSource.DataSource));
        }
        private void EraseCurrentRowButton_Click(object sender, EventArgs e)
        {
            if (_bindingSource.Current == null) return;
            var id = ((DataRowView)_bindingSource.Current).Row.Field<int>("id");
            _bindingSource.RemoveCurrent();
            _dataOperations.RemoveRow(id);

        }

        private void ResetManualKeysButton_Click(object sender, EventArgs e)
        {
            if (Question("Do you really want to manually reset row position for all records?"))
            {
                _dataOperations.ResetIdentifiers();
            }            
        }
    }
}
