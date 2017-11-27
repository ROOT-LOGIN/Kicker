using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Resx = Kicker.Properties.Resources;

namespace Kicker
{
    public partial class MainForm : Form
    {
        public MainForm( )
        {
            InitializeComponent();

            notifyIcon.DoubleClick += notifyIcon_DoubleClick;

			HKC = new HotKeyControl();
			HKC.Dock = DockStyle.Fill;
			panel.Controls.Add(HKC);

			var hotkey = new RefPair<Keys, HotKey[]>(Keys.F21, 
				HotKeyControl.GenerateAllHotKeysFor(Keys.F21, OnConfiguration));
			toolStripComboBox.Items.Add(hotkey);
			hotkey = new RefPair<Keys, HotKey[]>(Keys.F22,
				HotKeyControl.GenerateAllHotKeysFor(Keys.F22, OnConfiguration));
			toolStripComboBox.Items.Add(hotkey);
			hotkey = new RefPair<Keys, HotKey[]>(Keys.F23,
				HotKeyControl.GenerateAllHotKeysFor(Keys.F23, OnConfiguration));
			toolStripComboBox.Items.Add(hotkey);
			hotkey = new RefPair<Keys, HotKey[]>(Keys.F24,
				HotKeyControl.GenerateAllHotKeysFor(Keys.F24, OnConfiguration));
			toolStripComboBox.Items.Add(hotkey);

			HotKeyActionMap = new Dictionary<int, HotKey>();

            toolStripComboBox1.Items.Add(new RefPair<string, HotKeyControl.SubmitMethod>(
                "Prompt", HotKeyControl.SubmitMethod.Prompt));
            toolStripComboBox1.Items.Add(new RefPair<string, HotKeyControl.SubmitMethod>(
                "Auto Commit", HotKeyControl.SubmitMethod.AutoCommit));
            toolStripComboBox1.Items.Add(new RefPair<string, HotKeyControl.SubmitMethod>(
                "Auto Cancel", HotKeyControl.SubmitMethod.AutoCancel));

            HKC.RowValidating += HKC_RowValidating;
			toolStripComboBox.SelectedIndex = 0;
            toolStripComboBox1.SelectedIndex = 0;
		}

        void HKC_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if(dgv.Columns[e.ColumnIndex].DataPropertyName == "Action")
            {
                var cel = dgv[e.ColumnIndex, e.RowIndex];
                var act = cel.Value as string;
                cel.ErrorText = null;
                if(string.IsNullOrEmpty(act) || string.IsNullOrWhiteSpace(act))
                    return;

                if(System.IO.File.Exists(act) == false)
                {
                    if(System.IO.Directory.Exists(act) == false)
                    {
                        e.Cancel = true;
                        cel.ErrorText = "Invalid path";
                    }
                }
            }            
        }

		Dictionary<int, HotKey> HotKeyActionMap;

		void OnConfiguration(object obj, PropertyChangedEventArgs arg)
		{
			HotKey hotkey = (HotKey)obj;
			if(arg.PropertyName == "Enabled")
			{
				if(hotkey.Enabled)
				{
					hotkey.id = RegHotKey(hotkey.Mods, hotkey.Key);
					HotKeyActionMap.Add(hotkey.id, hotkey);
				}
				else
				{
					UnregHotKey(hotkey.id);
					HotKeyActionMap.Remove(hotkey.id);
				}
			}
		}

		HotKeyControl HKC;
        bool is_exiting = false;

        protected override void OnClosing(CancelEventArgs e)
        {
            if(is_exiting) { base.OnClosing(e); }
            else { e.Cancel = true; Visible = false; }
        }

        protected override void OnClosed(EventArgs e)
        {
            if(is_exiting)
            {
                base.OnClosed(e);
                Application.Exit();
            }            
        }

        void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            is_exiting = true;
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            this.Close();
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        protected override void WndProc(ref Message m)
        {               
            base.WndProc(ref m);
            if(m.Msg == (int)WM.WM_HOTKEY)
            {
                var id = m.WParam.ToInt32();
                var key = (Keys)ShlUtil.HIWORD(m.LParam);
                var mod = (ShellNotifyIcon.MOD)ShlUtil.LOWORD(m.LParam);
				HotKey hotkey;
				if(HotKeyActionMap.TryGetValue(id, out hotkey) && hotkey != null)
				{
                    if(hotkey.Action == null)
                        return;
                    var str = hotkey.Action.ToString().Trim();
                    if(str.Length == 0)
                        return;
                    if(System.IO.File.Exists(str))
                    {
                        if(str.Length > 4 && System.IO.Path.GetExtension(str).ToLower() == ".exe")
                        {
                            Process.Start(str);
                        }
                        else
                        {
                            ShellNotifyIcon.ShellExecute(IntPtr.Zero, "open", str, null, null, 10);
                        }
                    }
                    else if(System.IO.Directory.Exists(str))
                    {
                        ShellNotifyIcon.ShellExecute(IntPtr.Zero, "explore", str, null, null, 10);
                    }
					MessageBox.Show(hotkey.Display);					
				}                
            }
        }
        
		internal int RegHotKey(ShellNotifyIcon.MOD mods, Keys key)
        {
            int id = 0xA000 | (((int)mods & 0xF) << 8) | (int)key;
            ShellNotifyIcon.RegisterHotKey(this.Handle, id, mods, key);
            return id;
        }

        internal void UnregHotKey(int id)
        {
            ShellNotifyIcon.UnregisterHotKey(this.Handle, id);
		}

		private void toolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(toolStripComboBox.SelectedIndex < 0)
			{
                HKC.Caption = "<<null>>";
				HKC.HotKeys = null;
				return;
			}

			var kvp = (RefPair<Keys, HotKey[]>)toolStripComboBox.SelectedItem;
			HKC.Caption = kvp.Key.ToString();
			HKC.HotKeys = kvp.Value;
		}

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(toolStripComboBox1.SelectedIndex < 0)
            {
                HKC.SubmitMode = HotKeyControl.SubmitMethod.Prompt;
                return;
            }

            var kvp = (RefPair<string, HotKeyControl.SubmitMethod>)toolStripComboBox1.SelectedItem;
            HKC.SubmitMode = kvp.Value;
        }
    }

	public class RefPair<K,V>
	{
		public K Key { get; set; }
		public V Value { get; set; }

		public RefPair( ) { }
		public RefPair(K key) { Key = key; }
		public RefPair(K key, V value) { Key = key; Value = value; }

		public override string ToString( )
		{
			return Key.ToString();
		}
	}
}
