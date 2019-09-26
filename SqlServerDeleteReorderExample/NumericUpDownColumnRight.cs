using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SqlServerDeleteReorderExample
{
    public class NumericUpDownColumnRight : DataGridViewColumn
    {
        private bool bAllowNegative = false;
        [Category("Behavior"), Description("Allow negative values"), DefaultValue(typeof(bool), "False")]
        public bool AllowNegative
        {
            get
            {
                return bAllowNegative;
            }
            set
            {
                bAllowNegative = value;
            }
        }
        private int mDecimalPlaces = 2;
        [Category("Behavior"), Description("Decimal places"), DefaultValue(typeof(bool), "False")]
        public int DecimalPlaces
        {
            set
            {
                mDecimalPlaces = value;
            }
            get
            {
                return mDecimalPlaces;
            }
        }
        public override object Clone()
        {
            NumericUpDownColumnRight TheCopy = (NumericUpDownColumnRight)base.Clone();

            TheCopy.AllowNegative = this.AllowNegative;
            TheCopy.DecimalPlaces = this.DecimalPlaces;

            return TheCopy;
        }
        public NumericUpDownColumnRight() : base(new NumericUpDownRightCell())
        {
        }
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {

                // Ensure that the cell used for the template is a NumericUpDownCell. 
                if (value != null && !(value.GetType().IsAssignableFrom(typeof(NumericUpDownRightCell))))
                {
                    throw new InvalidCastException("Must be a NumericUpDown");
                }

                base.CellTemplate = value;
            }
        }
    }
    public class NumericUpDownRightCell : DataGridViewTextBoxCell
    {
        public NumericUpDownRightCell()
        {
        }
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            NumericUpDownRightEditingControl ctl = (NumericUpDownRightEditingControl)DataGridView.EditingControl;
            NumericUpDownColumnRight MyOwner = (NumericUpDownColumnRight)OwningColumn;

            decimal CurrentValue = 0M;

            if (MyOwner.AllowNegative)
            {
                ctl.Minimum = -100;
            }
            else
            {
                ctl.Minimum = 1;
            }

            if (!(DBNull.Value.Equals(this.Value)))
            {
                if (Value == null)
                {
                    return;
                }
                if (decimal.TryParse(this.Value.ToString(), out CurrentValue))
                {
                    ctl.Value = CurrentValue;
                }
            }
            else
            {
                ctl.Value = 0;
            }
        }
        public override Type EditType
        {
            get
            {
                // Return the type of the editing contol that NumericUpDownCell uses. 
                return typeof(NumericUpDownRightEditingControl);
            }
        }
        public override Type ValueType
        {
            get
            {
                // Return the type of the value that NumericUpDownCell contains. 
                return typeof(decimal);
            }
        }
        public override object DefaultNewRowValue
        {
            get
            {
                // Use as the current default value. 
                return null;
            }
        }
    }
    internal class NumericUpDownRightEditingControl : NumericUpDown, IDataGridViewEditingControl
    {
        private DataGridView dataGridViewControl;
        private bool valueIsChanged = false;
        private int rowIndexNum;

        public NumericUpDownRightEditingControl()
        {
            this.DecimalPlaces = 0; // default to no decimals 
            this.TextAlign = HorizontalAlignment.Left;
            this.UpDownAlign = LeftRightAlignment.Right;
            this.Maximum = 999;
        }
        public object EditingControlFormattedValue
        {

            get
            {
                //Return Me.Value.ToString("#.##") 
                return this.Value.ToString("##.###"); // default to no decimals 
            }

            set
            {
                if (value is decimal)
                {
                    this.Value = decimal.Parse(Convert.ToString(value));
                }
            }

        }
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return this.Value.ToString(); // Me.Value.ToString("#.##") 
        }
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {

            this.Font = dataGridViewCellStyle.Font;
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            this.BackColor = dataGridViewCellStyle.BackColor;

        }
        public int EditingControlRowIndex
        {

            get
            {
                return rowIndexNum;
            }
            set
            {
                rowIndexNum = value;
            }

        }
        public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the NumericUpDown handle the keys listed. 
            if (((key & Keys.KeyCode) == Keys.Left) || ((key & Keys.KeyCode) == Keys.Up) || ((key & Keys.KeyCode) == Keys.Down) || ((key & Keys.KeyCode) == Keys.Right) || ((key & Keys.KeyCode) == Keys.Home) || ((key & Keys.KeyCode) == Keys.End) || ((key & Keys.KeyCode) == Keys.PageDown) || ((key & Keys.KeyCode) == Keys.PageUp))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // No preparation needs to be done. 
        }
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridViewControl;
            }
            set
            {
                dataGridViewControl = value;
            }
        }
        public bool EditingControlValueChanged
        {
            get
            {
                return valueIsChanged;
            }
            set
            {
                valueIsChanged = value;
            }
        }
        Cursor IDataGridViewEditingControl.EditingPanelCursor
        {
            get
            {
                return this.EditingControlCursor;
            }
        }
        public Cursor EditingControlCursor
        {
            get
            {
                return base.Cursor;
            }
        }
        protected override void OnValueChanged(EventArgs e)
        {
            // Notify the DataGridView that the contents of the cell have changed. 
            valueIsChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(e);
        }
    }
}