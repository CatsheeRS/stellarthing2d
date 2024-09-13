using System;
using System.Numerics;
using Raylib_cs;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages all things rendering
/// </summary>
public static partial class Renderer {
    /// <summary>
    /// how close can you see
    /// </summary>
    public static float near { get; set; } = 0;
    /// <summary>
    /// how far can you see
    /// </summary>
    public static float far { get; set; } = 1000;
    /// <summary>
    /// field of view :D
    /// </summary>
    public static float fov { get; set; } = 90;
    /// <summary>
    /// it's the aspect ratio
    /// </summary>
    public static float aspectRatio { get; set; } = 0;
    /// <summary>
    /// similar to fov but rad
    /// </summary>
    public static float fovRad { get; set; } = 0;
    /// <summary>
    /// the projection matrix
    /// </summary>
    public static Matrix4x4 matproj { get; set; }

    /// <summary>
    /// as the name implies, this sets up the projection matrix. the near and far parameters specify how far and how near you can see. i hope you already know what fov is
    /// </summary>
    public static void setupProjectionMatrix(float near, float far, float fov)
    {
        Renderer.near = near;
        Renderer.far = far;
        Renderer.fov = fov;
        aspectRatio = settings.renderSize.y / settings.renderSize.x;
        fovRad = 1f / MathF.Tan(fov * 0f / 180f * MathF.PI);

        // el matriz
        matproj = new(
            aspectRatio, 0.0f,      0.0f,                0.0f,
            0.0f,        fovRad,    0.0f,                0.0f,
            0.0f,        0.0f,      far / (far - near),  -far * near / (far - near),
            0.0f,        0.0f,      1.0f,                0.0f
        );
        /*matproj = new(
            aspectRatio, 0.0f,   0.0f,                       0.0f,
            0.0f,        fovRad, 0.0f,                       0.0f,
            0.0f,        0.0f,   far / (far - near),         1.0f,
            0.0f,        0.0f,   -far * near / (far - near), 0.0f
        );*/
    }

    /// <summary>
    /// i suck at maths :(
    /// </summary>
    public static vec3 multiplyMatrixVector(vec3 v, Matrix4x4 m)
    {
        var r = vec3(
            v.x * m[0, 0] + v.y * m[1, 0] + v.z * m[2, 0] + m[3, 0],
            v.x * m[0, 1] + v.y * m[1, 1] + v.z * m[2, 1] + m[3, 1],
            v.x * m[0, 2] + v.y * m[1, 2] + v.z * m[2, 2] + m[3, 2]
        );

        double w = v.x * m[0, 3] + v.y * m[1, 3] + v.z * m[2, 3] + m[3, 3];
        if (w != 0) {
            r /= vec3(w, w, w);
        }
        return r;
    }

    /// <summary>
    /// as the name implies, this draws a mesh
    /// </summary>
    public static void drawMesh(Mesh mesh)
    {
        foreach (Triangle tri in mesh.tris) {
            Triangle projected = new(
                multiplyMatrixVector(tri.vert1, matproj),
                multiplyMatrixVector(tri.vert2, matproj),
                multiplyMatrixVector(tri.vert3, matproj)
            );

            // scale into view so it's not 1 pixel
            projected.vert1 += vec3(1.0f, 1.0f, 0.0f);
            projected.vert2 += vec3(1.0f, 1.0f, 0.0f);
            projected.vert3 += vec3(1.0f, 1.0f, 0.0f);
            projected.vert1 *= vec3(0.5f * (float)scrw, 0.5f * (float)scrh, 1.0f);
            projected.vert2 *= vec3(0.5f * (float)scrw, 0.5f * (float)scrh, 1.0f);
            projected.vert3 *= vec3(0.5f * (float)scrw, 0.5f * (float)scrh, 1.0f);

            // actually draw the triangle :D
            Rlgl.Begin(DrawMode.Triangles);
                Rlgl.Vertex3f((float)projected.vert1.x, (float)projected.vert1.y, (float)projected.vert1.z);
                Rlgl.Vertex3f((float)projected.vert2.x, (float)projected.vert2.y, (float)projected.vert2.z);
                Rlgl.Vertex3f((float)projected.vert3.x, (float)projected.vert3.y, (float)projected.vert3.z);
            Rlgl.End();

            log(projected);
        }
    }
}
