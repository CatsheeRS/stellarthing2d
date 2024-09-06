using System;
using static starry.Starry;

namespace starry;

public class FragmentShader : IShader {
    public string getSource() =>
        @"#version 330

        out vec4 color;ZZ
        
        void main() {
            gl_FragColor = vec4(0.0, 1.0, 0.0, 1.0);;
        }";
    
    public ShaderType getType() => ShaderType.fragment;
}