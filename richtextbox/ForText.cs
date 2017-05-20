using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace richtextbox
{
    public partial class ForText : Form
    {

        public ForText()
        {
            InitializeComponent();
            MyOwner = Owner as Form1;
        }

        protected int CurrPositionCaret { set; get; }
        public RichTextBox ChildRichTextBox;
        public string PositionCursor { set; get; }
        Form1 MyOwner;

        private void ForText_Load(object sender, EventArgs e)
        {
    //        if (this.Parent != null)
            MyOwner = (Form1)this.MdiParent;
            ChildRichTextBox = this.richTextBox1;
            this.WindowState = FormWindowState.Maximized;
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            //MyOwner = (Form1)this.MdiParent;
            //MyOwner.ParentLabel.Text =  this.richTextBox1.Lines[this.richTextBox1.GetLineFromCharIndex(this.richTextBox1.GetFirstCharIndexOfCurrentLine())].ToString();
            int cursorPosition = this.richTextBox1.SelectionStart;
            int lineIndex = this.richTextBox1.GetLineFromCharIndex(cursorPosition);
            if (MyOwner != null)
            {
                try
                {
                    if (this.richTextBox1.Lines[lineIndex] == "0")
                        MyOwner.ParentLabel.Text = "0";
                    else
                        MyOwner.ParentLabel.Text = $"{lineIndex.ToString()}/{cursorPosition.ToString()}";
                }
                catch (IndexOutOfRangeException ex)
                {
                    MyOwner.ParentLabel.Text = "0";
                } 
            }
        }
    }
}
