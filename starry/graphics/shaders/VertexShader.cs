using System;
using static starry.Starry;

namespace starry;

public class VertexShader : IShader {
    public string getSource() =>
        @"#version 330
        
        in layout(location = 0) vec2 position;
        
        void main() {
            gl_Position = vec4(position, 0.0, 1.0);
        }";

    public ShaderType getType() => ShaderType.vertex;
}