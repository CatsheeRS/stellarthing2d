using static starry.Starry;

namespace starry;

public class GraphicsTestComp {
    public GraphicsTestComp()
    {
        GLRenderer.setupTriangle([
            +0.0f, +0.0f,   0.0f, 1.0f, 0.0f,
            -1.0f, +1.0f,   0.0f, 1.0f, 0.0f,
            -1.0f, +1.0f,   0.0f, 1.0f, 0.0f,
            -1.0f, -1.0f,   0.0f, 1.0f, 0.0f,
            +1.0f, -1.0f,   0.0f, 1.0f, 0.0f,
        ],
        
        [ // indices
            0, 1, 2,
            0, 3, 4
        ]);
        GLRenderer.installShader(new VertexShader(), new FragmentShader());
    }

    public void update()
    {
        GLRenderer.drawTriangle();
    }
}