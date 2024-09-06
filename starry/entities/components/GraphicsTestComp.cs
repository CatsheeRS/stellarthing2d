using static starry.Starry;

namespace starry;

public class GraphicsTestComp {
    public GraphicsTestComp()
    {
        GLRenderer.setupTriangle([
            +0.0f, +1.0f,
            -1.0f, -1.0f,
            +1.0f, -1.0f,
        ]);
    }

    public void update()
    {
        GLRenderer.drawTriangle();
    }
}