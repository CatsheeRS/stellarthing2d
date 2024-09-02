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
	
	private static uint shader_program;

	public static unsafe void Init(Glfw _glfw) {
		glfw = _glfw;
		gl = new GL(_glfw.Context);

		uint v_shader = gl.CreateShader(ShaderType.VertexShader);

		fixed (byte *contents = File.ReadAllBytes("./shaders/vertex.vert")) {
			gl.ShaderSource(v_shader, 1, &contents, null);

			gl.CompileShader(v_shader);
		}

		gl.GetShader(v_shader, ShaderParameterName.CompileStatus, out int vstatus);

		if (vstatus != (int) GLEnum.True) {
			// Cannot compile vertex shader, therefore can't draw anything later on.
			throw new Exception("Failed to compile vertex shader: " + gl.GetShaderInfoLog(v_shader));
		}

		uint f_shader = gl.CreateShader(ShaderType.FragmentShader);

		fixed (byte *contents = File.ReadAllBytes("./shaders/fragment.frag")) {
			gl.ShaderSource(f_shader, 1, &contents, null);

			gl.CompileShader(f_shader);
		}

		gl.GetShader(f_shader, ShaderParameterName.CompileStatus, out int fstatus);

		if (fstatus != (int) GLEnum.True) {
			// Cannot compile fragment shader, therefore can't draw anything later on.
			throw new Exception("Failed to compile fragment shader: " + gl.GetShaderInfoLog(f_shader));
		}

		shader_program = gl.CreateProgram();

		gl.AttachShader(shader_program, v_shader);
		gl.AttachShader(shader_program, f_shader);

		gl.LinkProgram(shader_program);

		gl.GetProgram(shader_program, ProgramPropertyARB.LinkStatus, out int lstatus);

		if (lstatus != (int) GLEnum.True) {
			// Cannot link shader program
			throw new Exception("Failed to link shader program: " + gl.GetProgramInfoLog(shader_program));
		}

		// the shaders arent needed anymore - the program is linked
		// and can function by itself
		gl.DetachShader(shader_program, v_shader);
		gl.DetachShader(shader_program, f_shader);

		gl.DeleteShader(v_shader);
		gl.DeleteShader(f_shader);
	}

	public static unsafe void DrawCube(Vector3 pos, Vector3 size) {
		
	}
}
