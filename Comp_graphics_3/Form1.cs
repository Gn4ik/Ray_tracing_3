using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comp_graphics_3
{
	public partial class Form1 : Form
	{
		private View view = new View();

		public Form1()=>
			InitializeComponent();

		private void glControl1_Paint(object sender, PaintEventArgs e)
		{
			view.Update();
			glControl1.SwapBuffers();
			GL.UseProgram(0);
		}

		private void glControl1_Load(object sender, EventArgs e)
		{
			view.load();
		}
	}
}
