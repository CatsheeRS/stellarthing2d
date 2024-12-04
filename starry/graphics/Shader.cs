using Silk.NET.OpenGL;

namespace starry;

/// <summary>
/// its a shader :D
/// </summary>
public class Shader {
    internal uint id = 0;

    public void use()
    {

    }

    public void compile(string vertex, string frag)
    {
        if (Graphics.gl == null) return;
        GL gl = Graphics.gl;

        uint sVertex, sFragment, gShader;
        // vertex shader
        sVertex = gl.CreateShader(GLEnum.VertexShader);
        gl.ShaderSource(sVertex, vertex);
        gl.CompileShader(sVertex);
        checkCompileErrors(sVertex, "VERTEX");
        // fragment Shader
        sFragment = glCreateShader(GL_FRAGMENT_SHADER);
        glShaderSource(sFragment, 1, &fragmentSource, NULL);
        glCompileShader(sFragment);
        checkCompileErrors(sFragment, "FRAGMENT");
        // if geometry shader source code is given, also compile geometry shader
        if (geometrySource != nullptr)
        {
            gShader = glCreateShader(GL_GEOMETRY_SHADER);
            glShaderSource(gShader, 1, &geometrySource, NULL);
            glCompileShader(gShader);
            checkCompileErrors(gShader, "GEOMETRY");
        }
        // shader program
        this->ID = glCreateProgram();
        glAttachShader(this->ID, sVertex);
        glAttachShader(this->ID, sFragment);
        if (geometrySource != nullptr)
            glAttachShader(this->ID, gShader);
        glLinkProgram(this->ID);
        checkCompileErrors(this->ID, "PROGRAM");
        // delete the shaders as they're linked into our program now and no longer necessary
        glDeleteShader(sVertex);
        glDeleteShader(sFragment);
        if (geometrySource != nullptr)
            glDeleteShader(gShader);
    }
}