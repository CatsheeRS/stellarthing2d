using System.IO;
using System;
using System.Collections.Generic;
using Silk.NET.GLFW;
using Silk.NET.OpenGL;
using GlmNet;
using static starry.Starry;

namespace starry;

// UNTESTED
public static class Renderer3D {
	private static Glfw? glfw;
	private static GL? gl;

	unsafe private static WindowHandle *whandle;
	
	private static uint shaderProgram;

	public static unsafe void init(Glfw _glfw, WindowHandle *handle)
	{
		whandle = handle;
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
		if (glfw == null || gl == null) {
			throw new Exception("🤔🔥🤮➡️🤔🟥🤓⭐🤑🔴🔴🤑🤣🤔⬆️⬆️😇😂😊😊😊🤮");
		}

		glfw.GetWindowSize(whandle, out int width, out int height);

		mat4 proj = glm.perspective(glm.radians(45.0f), (float) width / (float) height, 0.1f, 100.0f);

		vec4[] vecs = {
			new vec4((float) pos.x + (float) size.x, 0.0f, 0.0f, 0.0f),
			new vec4(0.0f, (float) pos.y + (float) size.y, 0.0f, 0.0f),
			new vec4(0.0f, 0.0f, (float) pos.z + (float) size.z, 0.0f),
			new vec4(0.0f, 0.0f, 0.0f, 1.0f)
		};

		mat4 model = new mat4(vecs);

		model = glm.rotate(model, glm.radians(-55.0f), new vec3(1.0f, 0.0f, 0.0f));

		mat4 view = new mat4(1.0f);

		view = glm.translate(view, new vec3(0.0f, 0.0f, -3.0f));

		int modelLoc = gl.GetUniformLocation(shaderProgram, "model");

		fixed (float *modelShit = model.to_array()) {
			gl.UniformMatrix4(modelLoc, 1, false, (double *) modelShit);
		}

		int viewLoc = gl.GetUniformLocation(shaderProgram, "view");

		fixed (float *viewShit = view.to_array()) {
			gl.UniformMatrix4(viewLoc, 1, false, (double *) viewShit);
		}

		int projLoc = gl.GetUniformLocation(shaderProgram, "projection");

		fixed (float *projShit = proj.to_array()) {
			gl.UniformMatrix4(projLoc, 1, false, (double *) projShit);
		}
	}
}
