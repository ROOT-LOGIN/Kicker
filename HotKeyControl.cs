using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kicker
{
    public partial class HotKeyControl : UserControl
    {
        public HotKeyControl( )
        {
            InitializeComponent();

			dataGridView.AutoGenerateColumns = false;

            dataGridView.RowLeave += dataGridView_RowLeave;            
            
        }

        public event DataGridViewCellCancelEventHandler RowValidating
        {
            add { dataGridView.RowValidating += value; }
            remove { dataGridView.RowValidating -= value; }
        }

        public event DataGridViewDataErrorEventHandler DataError
        {
            add { dataGridView.DataError += value; }
            remove { dataGridView.DataError -= value; }
        }

        void dataGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView.IsCurrentRowDirty)
            {
                if(SubmitMode == HotKeyControl.SubmitMethod.AutoCommit)
                    dataGridView.EndEdit();
                else if(SubmitMode == HotKeyControl.SubmitMethod.AutoCancel)
                    dataGridView.CancelEdit();
                else if(SubmitMode == SubmitMethod.Prompt)
                {
                    var msg = string.Format("Do you want to commit change?\n\nHotKey: {0}\n[Origin]{1}: {2}",
                        dataGridView.CurrentRow.Cells["ColKey"].Value,
                        dataGridView.Columns[e.ColumnIndex].HeaderText,
                        dataGridView.CurrentRow.Cells[e.ColumnIndex].Value
                        );
                    if(MessageBox.Show(msg, "Confirm Editing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        dataGridView.EndEdit();
                    else
                        dataGridView.CancelEdit();
                }
            }
                
        }

		public HotKey[] HotKeys
        {
			get { return (HotKey[])dataGridView.DataSource; }
            set { dataGridView.DataSource = value; }
        }

		public string Caption
		{
			get { return groupBox.Text; }
			set { groupBox.Text = value; }
		}

        public enum SubmitMethod
        {
            Prompt,
            AutoCommit,
            AutoCancel            
        }

        public SubmitMethod SubmitMode
        {
            get;
            set;
        }

		public static HotKey[] GenerateAllHotKeysFor(Keys Key, PropertyChangedEventHandler handler)
		{
			// const int j = ((int)(ShellNotifyIcon.MOD.MOD_CONTROL | ShellNotifyIcon.MOD.MOD_ALT | ShellNotifyIcon.MOD.MOD_SHIFT | ShellNotifyIcon.MOD.MOD_WIN));
			const int j = 16;
			var _HotKeys = new HotKey[j];
			for(int i = 0; i < j; i++)
			{
				var H = new HotKey(Key);
				H.Mods = (ShellNotifyIcon.MOD)i;
				if(handler != null) H.PropertyChanged += handler;
				_HotKeys[i] = H;
			}
			return _HotKeys;
		}
    }

	public class HotKey : INotifyPropertyChanged, IEditableObject
	{
		public int id { get; set; }

		bool _i_Enabled;
		public bool Enabled {
			get { return _i_Enabled; }
			set { if(_i_Enabled != value) { _i_Enabled = value; Notify("Enabled"); } }
		}		
		public bool Control {
            get { return _i_Mods.HasFlag(ShellNotifyIcon.MOD.MOD_CONTROL); }
            set 
            {
                if(_i_Mods.HasFlag(ShellNotifyIcon.MOD.MOD_CONTROL) != value)
                {
                    if(value)
                        Mods |= ShellNotifyIcon.MOD.MOD_CONTROL;
                    else
                        Mods &= ~ShellNotifyIcon.MOD.MOD_CONTROL;
                    Notify("Control", "Mods");
                }
            }
        }
        public bool Alt {
            get { return _i_Mods.HasFlag(ShellNotifyIcon.MOD.MOD_ALT); }
            set 
            {
                if(_i_Mods.HasFlag(ShellNotifyIcon.MOD.MOD_ALT) != value)
                {
                    if(value)
                        Mods |= ShellNotifyIcon.MOD.MOD_ALT;
                    else
                        Mods &= ~ShellNotifyIcon.MOD.MOD_ALT;
                    Notify("Alt", "Mods");
                }
            }
        }
        public bool Shift {
            get { return _i_Mods.HasFlag(ShellNotifyIcon.MOD.MOD_SHIFT); }
            set 
            {
                if(_i_Mods.HasFlag(ShellNotifyIcon.MOD.MOD_SHIFT) != value)
                {
                    if(value)
                        Mods |= ShellNotifyIcon.MOD.MOD_SHIFT;
                    else
                        Mods &= ~ShellNotifyIcon.MOD.MOD_SHIFT;
                    Notify("Shift", "Mods");
                }
            }
        }
        public bool Win {
            get { return _i_Mods.HasFlag(ShellNotifyIcon.MOD.MOD_WIN); }
            set 
            {
                if(_i_Mods.HasFlag(ShellNotifyIcon.MOD.MOD_WIN) != value)
                {
                    if(value)
                        Mods |= ShellNotifyIcon.MOD.MOD_WIN;
                    else
                        Mods &= ~ShellNotifyIcon.MOD.MOD_WIN;
                    Notify("Win", "Mods");
                }
            }
        }
		public Keys Key { get; set; }
		object _i_Action;
		public object Action
		{
			get { return _i_Action; }
			set { if(_i_Action != value) { _i_Action = value; Notify("Action"); } }
		}		


		public string Display {
			get
			{
				var ret = string.Empty;
				if(Control) ret += "Ctrl + ";
				if(Alt) ret += "Alt + ";
				if(Shift) ret += "Shift +";
				if(Win) ret += "WIN + ";
				ret += Key.ToString();
				return ret;
			}
		}

		public HotKey(Keys key) { Key = key; }

        ShellNotifyIcon.MOD _i_Mods;
		public ShellNotifyIcon.MOD Mods
		{
			get { return _i_Mods; }
            set
            {
                if(_i_Mods != value)
                {
                    _i_Mods = value;
                    Notify("Control", "Alt", "Shift", "Win", "Mods");
                }				
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		void Notify(params string[] props)
		{
            if(PropertyChanged != null && props.Length > 0)
            {
                foreach(var prop in props)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
		}

		HotKey oldValue;
		public void BeginEdit( )
		{
			oldValue = (HotKey)MemberwiseClone();
		}

		public void CancelEdit( )
		{
			Action = oldValue.Action;
			oldValue = null;
		}

		public void EndEdit( )
		{
			oldValue = null;
		}
	}

}
