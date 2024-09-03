using System.IO;
using System;
using Silk.NET.GLFW;
using Silk.NET.OpenGL;
using static starry.Starry;

namespace starry;

// UNTESTED
public static class Renderer3D {
	private static Glfw? glfw;
	private static GL? gl;
	
	private static uint shaderProgram;

	public static unsafe void init(Glfw _glfw)
	{
		glfw = _glfw;
		gl = new GL(_glfw.Context);

		uint vshader = gl.CreateShader(ShaderType.VertexShader);

		fixed (byte *contents = File.ReadAllBytes(Path.GetFullPath("../renderer/shaders/vertex.vert"))) {
			gl.ShaderSource(vshader, 1, &contents, null);
			gl.CompileShader(vshader);
		}

		gl.GetShader(vshader, ShaderParameterName.CompileStatus, out int vstatus);

		if (vstatus != (int)GLEnum.True) {
			// cannot compile vertex shader, therefore can't draw anything later on.
			throw new Exception("Failed to compile vertex shader: " + gl.GetShaderInfoLog(vshader));
		}

		uint fshader = gl.CreateShader(ShaderType.FragmentShader);

		fixed (byte *contents = File.ReadAllBytes(Path.GetFullPath("../renderer/shaders/fragment.frag"))) {
			gl.ShaderSource(fshader, 1, &contents, null);
			gl.CompileShader(fshader);
		}

		gl.GetShader(fshader, ShaderParameterName.CompileStatus, out int fstatus);

		if (fstatus != (int) GLEnum.True) {
			// cannot compile fragment shader, therefore can't draw anything later on.
			throw new Exception("Failed to compile fragment shader: " + gl.GetShaderInfoLog(fshader));
		}

		shaderProgram = gl.CreateProgram();

		gl.AttachShader(shaderProgram, vshader);
		gl.AttachShader(shaderProgram, fshader);

		gl.LinkProgram(shaderProgram);

		gl.GetProgram(shaderProgram, ProgramPropertyARB.LinkStatus, out int lstatus);

		if (lstatus != (int) GLEnum.True) {
			// Cannot link shader program
			throw new Exception("Failed to link shader program: " + gl.GetProgramInfoLog(shaderProgram));
		}

		// the shaders aren't needed anymore - the program is linked
		// and can function by itself
		gl.DetachShader(shaderProgram, vshader);
		gl.DetachShader(shaderProgram, fshader);

		gl.DeleteShader(vshader);
		gl.DeleteShader(fshader);
	}

	public static unsafe void drawCube(Vector3 pos, Vector3 size) {
		
	}
}
