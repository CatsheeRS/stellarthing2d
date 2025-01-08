# Starry core

The fundamentals of this handsome engine.

## Math stuff

Use the following structs:
- vec2: 2D position, (0, 0) is the top-left
- vec2i: vec2 but with integers
- vec3: 3D position, OpenGL-style with Y being up
- vec3i: vec3 but with integers
- color: 24-bit colors with an alpha parameter as well

You can use tuple syntax to initialize those because why not

```cs
vec3 pos = (1, 2, 3);
```

They also support math operators
```cs
pos += (1, 0, 0);
```

## Logging

You can use `Starry.log()` to print stuff

Please note that if you run it at the update loop it's gonna run at 10 FPS, but it only runs on debug mode or when verbose mode is enabled