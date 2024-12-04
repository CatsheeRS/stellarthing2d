using System;
using System.Threading.Tasks;
using Silk.NET.GLFW;
using Silk.NET.OpenGL;
namespace starry;

public static partial class Graphics {
    internal static GL? gl;
    internal static Shader shader;
    internal static uint quadVao;

    public static unsafe void create()
    {
        // shut up
        if (Window.glfw == null) return;

        gl = GL.GetApi(Window.glfw.GetProcAddress);
        Window.glfw.GetWindowSize(Window.window, out int wx, out int wy);
        gl.Viewport(0, 0, (uint)wx, (uint)wy);

        Starry.log("OpenGL has been initialized");

        setupSpriteStuff();
    }

    static unsafe void setupSpriteStuff()
    {
        if (gl == null) return;
        // i stole this from https://learnopengl.com/In-Practice/2D-Game/Rendering-Sprites :D
        float[] vertices = [
            // pos      // tex
            0.0f, 1.0f, 0.0f, 1.0f,
            1.0f, 0.0f, 1.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f,

            0.0f, 1.0f, 0.0f, 1.0f,
            1.0f, 1.0f, 1.0f, 1.0f,
            1.0f, 0.0f, 1.0f, 0.0f
        ];

        gl.GenVertexArrays(1, out quadVao);
        gl.GenBuffers(1, out uint vbo);
        
        gl.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);
        gl.BufferData<float>(BufferTargetARB.ArrayBuffer, vertices.AsSpan(),
            BufferUsageARB.StaticDraw);

        gl.BindVertexArray(quadVao);
        gl.EnableVertexAttribArray(0);
        gl.VertexAttribPointer(0, 4, GLEnum.Float, false, 4 * sizeof(float), null);
        gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);  
        gl.BindVertexArray(0);

        Starry.log("Sprite renderer has been initialized");
    }

    public static void clear(color color)
    {
        if (gl == null) return;
        gl.ClearColor(color.r / 256, color.g / 256, color.b / 256, color.a / 256);
        gl.Clear(ClearBufferMask.ColorBufferBit);
    }

    public static unsafe void endDrawing()
    {
        Window.glfw?.SwapBuffers(Window.window);
    }

    public static void cleanup() {}

    public static void drawSprite(Sprite sprite, rect2 rect, double rotation, color color)
    {
        shader.Use();
        glm::mat4 model = glm::mat4(1.0f);
        model = glm::translate(model, glm::vec3(position, 0.0f));  

        model = glm::translate(model, glm::vec3(0.5f * size.x, 0.5f * size.y, 0.0f)); 
        model = glm::rotate(model, glm::radians(rotate), glm::vec3(0.0f, 0.0f, 1.0f)); 
        model = glm::translate(model, glm::vec3(-0.5f * size.x, -0.5f * size.y, 0.0f));

        model = glm::scale(model, glm::vec3(size, 1.0f)); 

        this->shader.SetMatrix4("model", model);
        this->shader.SetVector3f("spriteColor", color);

        glActiveTexture(GL_TEXTURE0);
        texture.Bind();

        glBindVertexArray(this->quadVAO);
        glDrawArrays(GL_TRIANGLES, 0, 6);
        glBindVertexArray(0);
    }
}