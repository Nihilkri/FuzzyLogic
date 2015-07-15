using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuzzyLogic {
	public partial class Form1 : Form {
		#region Variables
		int fx, fy, fx2, fy2;
		Graphics gb, gf; Bitmap gi;

		#endregion Variables

		public Form1() {InitializeComponent();}
		private void Form1_Load(object sender, EventArgs e) {
			fx2 = (fx = Width) / 2; fy2 = (fy = Height) / 2;
			gi = new Bitmap(fx, fy); gb = Graphics.FromImage(gi);
			gf = CreateGraphics();



		}

		private void Form1_Paint(object sender, PaintEventArgs e) {
			gf.DrawImage(gi, 0, 0);
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			switch(e.KeyCode) {
				case Keys.Escape: Close(); break;



			}
		}
	}
}
