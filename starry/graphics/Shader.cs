using System;
using System.Numerics;
using Silk.NET.OpenGL;

namespace starry;

/// <summary>
/// its a shader :D
/// </summary>
public class Shader {
    internal uint id = 0;

    /// <summary>
    /// uses this shader.
    /// </summary>
    public void use()
    {
        if (Graphics.gl == null) return;
        GL gl = Graphics.gl;

        gl.UseProgram(id);
    }

    /// <summary>
    /// compiles the shader with vertex and fragment code. must be in glsl because we're using opengl
    /// </summary>
    public void compile(string vertex, string frag)
    {
        if (Graphics.gl == null) return;
        GL gl = Graphics.gl;

        uint sVertex, sFragment;

        // vertex shader
        sVertex = gl.CreateShader(GLEnum.VertexShader);
        gl.ShaderSource(sVertex, vertex);
        gl.CompileShader(sVertex);
        //checkCompileErrors(sVertex, "VERTEX");
        
        // fragment shader
        sFragment = gl.CreateShader(GLEnum.FragmentShader);
        gl.ShaderSource(sFragment, frag);
        gl.CompileShader(sFragment);
        //checkCompileErrors(sFragment, "FRAGMENT");
        
        // shader program
        id = gl.CreateProgram();
        gl.AttachShader(id, sVertex);
        gl.AttachShader(id, sFragment);
        gl.LinkProgram(id);
        //checkCompileErrors(this->ID, "PROGRAM");
        
        // delete the shaders as they're linked into our program now and no longer necessary
        gl.DeleteShader(sVertex);
        gl.DeleteShader(sFragment);
    }

    // monstrosities
    public void setFloat(string name, float v) =>
        Graphics.gl?.Uniform1(Graphics.gl?.GetUniformLocation(id, name) ?? 0, v);
    public void setInt(string name, int v) =>
        Graphics.gl?.Uniform1(Graphics.gl?.GetUniformLocation(id, name) ?? 0, v);
    public void setVec2(string name, vec2 v) =>
        Graphics.gl?.Uniform2(Graphics.gl?.GetUniformLocation(id, name) ?? 0, v.x, v.y);
    public void setVec3(string name, vec3 v) =>
        Graphics.gl?.Uniform3(Graphics.gl?.GetUniformLocation(id, name) ?? 0, v.x, v.y, v.z);
    public void setVec4(string name, rect2 v) =>
        Graphics.gl?.Uniform4(Graphics.gl?.GetUniformLocation(id, name) ?? 0, v.x, v.y, v.w, v.h);
    public void setMat4(string name, ReadOnlySpan<float> v) =>
        Graphics.gl?.UniformMatrix4(Graphics.gl?.GetUniformLocation(id, name) ?? 0, 1, false, v);
}