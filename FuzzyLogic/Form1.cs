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

		int mode = 8; bool[] nw = new bool[16];
		double[] mn = new double[16];
		double[] mx = new double[16];
		Random rng = new Random();
		#endregion Variables

		public Form1() {InitializeComponent();}
		private void Form1_Load(object sender, EventArgs e) {
			fx2 = (fx = Width) / 2; fy2 = (fy = Height) / 2;
			gi = new Bitmap(fx, fy); gb = Graphics.FromImage(gi);
			gf = CreateGraphics();

			for(int q=0 ; q < 16 ; q++) { nw[q] = true; mn[q] = 10.0; mx[q] = -1.0; }


				Calc();
		}

		private void Form1_Paint(object sender, PaintEventArgs e) {
			gf.DrawImage(gi, 0, 0);
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			switch(e.KeyCode) {
				case Keys.Escape: Close(); break;
				case Keys.D0: mode = 0; Calc(); break;
				case Keys.D1: mode = 1; Calc(); break;
				case Keys.D2: mode = 2; Calc(); break;
				case Keys.D3: mode = 3; Calc(); break;
				case Keys.D4: mode = 4; Calc(); break;
				case Keys.D5: mode = 5; Calc(); break;
				case Keys.D6: mode = 6; Calc(); break;
				case Keys.D7: mode = 7; Calc(); break;
				case Keys.D8: mode = 8; Calc(); break;
				case Keys.D9: mode = 9; Calc(); break;
				case Keys.A: mode = 10; Calc(); break;
				case Keys.B: mode = 11; Calc(); break;
				case Keys.C: mode = 12; Calc(); break;
				case Keys.D: mode = 13; Calc(); break;
				case Keys.E: mode = 14; Calc(); break;
				case Keys.F: mode = 15; Calc(); break;

				case Keys.Oemtilde: gf.DrawString(
@"0    False
1    NOR
2    <!=
3    NOT A
4    =!>
5    NOT B
6    XOR
7    NAND
8    AND
9    XNOR
A    B
B    =>
C    A
D    <=
E    OR
F    True", Font, Brushes.White, fx2, 0);
					for(int q = 0 ; q < 16 ; q++) {
						if(!nw[q]) gf.DrawString("mn[" + q + "] = " + mn[q], Font, Brushes.White, fx2 + 60, 12.5f * q);


					}

						break;
			}
		}

		private void Calc() {
			gf.DrawString("Please Hold", Font, Brushes.White, fx2 - 80, fy2 - 6);
			gb.Clear(Color.Black);
			double a = 0.0, b = 0.0, c = 0.0, s = 0.0;
			double na= 0.0, nb= 0.0, nc= 0.0;
			int cr = 0, cg = 0, cb = 0;
			for(int x = 0 ; x < fx ; x++) {
				a = (double)x / fx; na = 1.0 - a;
				//gf.DrawString(x.ToString(), Font, Brushes.White, fx2, fy2 + 16);
				for(int y = 0 ; y < fy ; y++) {
					b = (double)y / fy; nb = 1.0 - b;
					c = f(a, b); nc = 1.0 - c;
					s = c + f(na, b) + f(a, nb) + f(na, nb);
					if(s < mn[mode]) mn[mode] = s; if(s > mx[mode]) mx[mode] = s;
					if(nw[mode]) gi.SetPixel(x, fy - 1 - y, Color.FromArgb((int)(c * 255.0), 0, 0));
					else gi.SetPixel(x, fy - 1 - y, Color.FromArgb((int)(c * 255.0), 0, (mx[mode] == mn[mode]) ? 0 : (int)((s - mn[mode]) / (mx[mode] - mn[mode]) * 255.0)));//(int)((s-1.75)*8.0*255.0/2.0)));
					//gi.SetPixel(x, fy - 1 - y, Color.FromArgb((int)(c * 0), 0, (int)((s - 1.75) * 8.0 * 255.0 / 2.0)));
				}

			}
			gb.DrawString("Max " + mx[mode] + "\nMin " + mn[mode], Font, Brushes.White, 0, 0);
			nw[mode] = false;

			gf.DrawImage(gi, 0, 0);
		}
		private double f(double a, double b) {
			switch(mode) {
				case 0: return 0.0; // False
				case 1: return (1.0 - a) * (1.0 - b); // NOR
				case 6: return 1.0 - (1.0 - (1.0 - a) * b) * (1.0 - a * (1.0 - b)); // XOR
				case 7: return 1 - (a * b); //NAND
				case 8: return a * b; //AND
				case 9: return (1.0 - (1.0 - a) * b) * (1.0 - a * (1.0 - b)); // XNOR
				case 11: return 1.0 - (a * (1.0 - b)); // IMP
				case 14: return 1.0 - ((1.0 - a) * (1.0 - b)); // OR
				case 15: return 1.0; // True

				default: return rng.NextDouble();
			}

		}
	}
}
