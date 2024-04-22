using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Comp_graphics_3
{
	class View
	{
		int BasicProgramID;
		int BasicVertexSheder;
		int BasicFragmentShader;
		Vector3[] vertdata;
		int vbo_position;

		int uniform_pos;
		Vector3 campos;
		int uniform_aspect;
		Vector3 aspect;
		public int attribute_vpos;

		public void Update()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			GL.Enable(EnableCap.DepthTest);
			GL.EnableVertexAttribArray(attribute_vpos);
			GL.DrawArrays(PrimitiveType.Quads, 0, 4);
			GL.DisableVertexAttribArray(attribute_vpos);

		}
		void loadShader(String filename, ShaderType type, int program, out int address)
		{
			address = GL.CreateShader(type);
			using (System.IO.StreamReader sr = new StreamReader(filename))
			{
				GL.ShaderSource(address, sr.ReadToEnd());
			}
			GL.CompileShader(address);
			GL.AttachShader(program, address);
			Console.WriteLine(GL.GetShaderInfoLog(address));
		}
		private void buffObject()
		{
			vertdata = new Vector3[]{
				new Vector3(-1f,-1f,0f),
				new Vector3(1f,-1f,0f),
				new Vector3(1f,1f,0f),
				new Vector3(-1f,1f,0f)
			};

			GL.GenBuffers(1, out vbo_position);
			GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
			GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length *
				Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
			GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);
			GL.Uniform3(uniform_pos, campos);
			GL.Uniform3(uniform_aspect, aspect);
			GL.UseProgram(BasicProgramID);
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}
		private void InitShaders()
		{
			BasicProgramID = GL.CreateProgram();
			loadShader("C:\\PROGRAMMING\\Comp_graphics_3\\Comp_graphics_3\\Shaders\\raytracing.vert", ShaderType.VertexShader,
				BasicProgramID, out BasicVertexSheder);
			loadShader("C:\\PROGRAMMING\\Comp_graphics_3\\Comp_graphics_3\\Shaders\\raytracing.frag", ShaderType.FragmentShader,
				BasicProgramID, out BasicFragmentShader);
			GL.LinkProgram(BasicProgramID);
			// Проверяем успех компоновки
			int status = 0;
			GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus, out status);
			Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));
		}

		public void load()
		{
			InitShaders();
			buffObject();
		}
	}
}

