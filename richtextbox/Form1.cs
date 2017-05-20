using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;


namespace richtextbox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        FormWindowState saveWinState { set; get; }
        ForText child;
        public ToolStripLabel ParentLabel;

        private void Form1_Load(object sender, EventArgs e)
        {
            ParentLabel = this.toolStripLabel1;
            child = new ForText();
            child.MdiParent = this;
            child.Show();

        //    this.toolTip1.SetToolTip((Control)this.toolStripButton1, "Open");
            


            this.Icon = new Icon("..//..//..//black.ico");
            child.ChildRichTextBox.SelectionColor = Color.Indigo;
            child.ChildRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
            //this.richTextBox1.Select()
            child.ChildRichTextBox.SelectionFont = new Font(child.ChildRichTextBox.Font, FontStyle.Bold);
            //Image img = Image.FromFile("..//..//..qqq.jpg");
            //Clipboard.Clear();
           // Clipboard.SetImage(img);
           // this.richTextBox1.Paste();
            Clipboard.Clear();
            this.notifyIcon1.Icon = new Icon("..//..//..//black.ico");
            this.notifyIcon1.Text = "NotepadT";
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            this.saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files(*.*)|*.*";
            this.saveFileDialog1.FilterIndex = 2;
            this.saveFileDialog1.RestoreDirectory = true;
            //open dialog
            if(this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(this.saveFileDialog1.FileName, FileMode.Create);
                fs.Write(UnicodeEncoding.Unicode.GetBytes(child.ChildRichTextBox.Text), 0,
                    UnicodeEncoding.Unicode.GetBytes(child.ChildRichTextBox.Text).Length);
                fs.Close();
                MessageBox.Show("Done!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.RestoreDirectory = true;
            //open dialog
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(this.openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
                {
                    // fs.Read(UnicodeEncoding.Unicode.GetBytes(), 0, )
                    int size = (int)fs.Length;
                    byte[] data = new byte[size];
                    //string str =  fs.Read(data, 0, size);
                    //int byteCounter = 0;
                    //this.richTextBox1.Text = File.ReadAllText(this.openFileDialog1.FileName);
                    child.ChildRichTextBox.LoadFile(this.openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                }
            }

        }

        private void textColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                child.ChildRichTextBox.SelectionColor = this.colorDialog1.Color;
            }
        }

        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                child.ChildRichTextBox.BackColor = this.colorDialog1.Color;
            }
        }

        private void fontStyleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(this.fontDialog1.ShowDialog() == DialogResult.OK)
            {
                child.ChildRichTextBox.Font = this.fontDialog1.Font;
            }
        }



        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(PointToScreen(e.Location));
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileToolStripMenuItem_Click(sender, e);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileToolStripMenuItem_Click(sender, e);
        }

        private void fontToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            fontStyleToolStripMenuItem1_Click(sender, e);
        }

        private void textColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textColorToolStripMenuItem_Click(sender, e);
        }

        private void backColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            backColorToolStripMenuItem_Click(sender, e);
        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ver.1.0.0 by Ivan Tkachuk", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)== DialogResult.OK)
            {
                Clipboard.Clear();
                this.Close();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            child.ChildRichTextBox.SelectAll();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            child.ChildRichTextBox.SelectedText = "";
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(child.ChildRichTextBox.SelectedText);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            child.ChildRichTextBox.Paste();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(child.ChildRichTextBox.SelectedText);
            child.ChildRichTextBox.SelectedText = "";
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(this.WindowState == FormWindowState.Normal || this.WindowState == FormWindowState.Maximized)
            {
                saveWinState = this.WindowState;
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.WindowState = saveWinState;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.Filter = "All files(*.*)|*.*";
            this.saveFileDialog1.FilterIndex = 2;
            this.saveFileDialog1.RestoreDirectory = true;
            //open dialog
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(this.saveFileDialog1.FileName, FileMode.Create);
                fs.Write(UnicodeEncoding.Unicode.GetBytes(child.ChildRichTextBox.Text), 0,
                    UnicodeEncoding.Unicode.GetBytes(child.ChildRichTextBox.Text).Length);
                fs.Close();
                MessageBox.Show("Done!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            aboutProgramToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            saveFileToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            openFileToolStripMenuItem_Click(sender, e);
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(this.GetType());
            resources.ApplyResources(this, "$this");
            foreach (Control c in this.Controls)
                resources.ApplyResources(c, c.Name);
        }

        private void українськаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("uk");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("uk");

            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(this.GetType());
            resources.ApplyResources(this, "$this");
            foreach (Control c in this.Controls)
                resources.ApplyResources(c, c.Name);
        }
    }
}
