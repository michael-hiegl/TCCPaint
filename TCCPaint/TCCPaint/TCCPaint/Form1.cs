using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Printing;




namespace TCCPaint
{
    public partial class Form1 : Form
    {

        //Define a Pen
        private Pen MyPen = new Pen(Color.Black,3);

        //Zeichenfläche
        private Graphics PaintArea;

        private Graphics PaintAreaPrint;
        private Bitmap MyBitmap;

        //Zeichnen aktiv
        private bool PaintEnable = false;

        //Mouse Position
        private int MXpos=0;
        private int MYPos = 0;
 
        
        public Form1()
        {
            InitializeComponent();

            //Define Workspace as Grapics Area
            PaintArea = PA_Workspace.CreateGraphics();

            //Draw Bitmap
            MyBitmap = new Bitmap(PA_Workspace.Width, PA_Workspace.Height);
            PaintAreaPrint = Graphics.FromImage(MyBitmap);
        }

        #region Datei
        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PaintArea.Clear(Color.White);
            PaintAreaPrint.Clear(Color.White);
        }

        private void speichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if( saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MyBitmap.Save(saveFileDialog1.FileName);
            }
        }

        private void druckenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PrintDocument Objekt
            PrintDocument MyPrintDoc = new PrintDocument();

            //Add Event Handler
            MyPrintDoc.PrintPage += new PrintPageEventHandler(this.PrintTextFileHandler);


            printDialog1.Document = MyPrintDoc;
            if (printDialog1.ShowDialog()== DialogResult.OK)
            {
               MyPrintDoc.Print();
            }
         
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion



        #region Paint

        private void PA_Workspace_Paint(object sender, PaintEventArgs e)
        {
        }

        private void PA_Workspace_MouseDown(object sender, MouseEventArgs e)
        {
            PaintEnable = true;
            MXpos = e.X;
            MYPos = e.Y;
        }

        private void PA_Workspace_MouseMove(object sender, MouseEventArgs e)
        {
            if (PaintEnable)
            {
                PaintArea.DrawLine(MyPen, MXpos, MYPos, e.X, e.Y);
                PaintAreaPrint.DrawLine(MyPen, MXpos, MYPos, e.X, e.Y);
                MXpos = e.X;
                MYPos = e.Y;
            }
        }

        private void PA_Workspace_MouseUp(object sender, MouseEventArgs e)
        {
            PaintEnable = false;
        }
        #endregion


        #region PenSize
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MyPen.Width = 1;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            MyPen.Width = 3;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            MyPen.Width = 6;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            MyPen.Width = 9;
        }
        #endregion


        #region PenColor
        private void schwarzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyPen.Color = Color.Black;
        }

        private void rotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyPen.Color = Color.Red;
        }

        private void grünToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyPen.Color = Color.Green;
        }

        private void blauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyPen.Color = Color.Blue;
        }

        private void eigeneFarbeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                MyPen.Color = colorDialog1.Color;
            }

        }

        #endregion


        #region Event

        private void PrintTextFileHandler (object sender, PrintPageEventArgs PPEArgs)
        {
            PPEArgs.Graphics.DrawImage(MyBitmap, 100, 100);
        }


        #endregion



    }
}
