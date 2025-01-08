# Particles

Starry has an epic particle system. For example here's how to make 5 gazillion particles from an entity:

```cs
Particles? particles;

public async void create()
{
    lasparticulas = new() {
        particle = await Starry.load<Sprite>("white.png"),
        amountFunc = () => (uint)StMath.randint(200, 6000),
        durationFunc = () => StMath.randfloat(1, 5),
        // in pixels, for tiles use tile!.globalPosition
        positionStartFunc = () => position,
        // added to the start position
        positionEndFunc = () => StMath.randvec2((-200, -200), (200, 200)),
        rotationStartFunc = () => 0,
        rotationEndFunc = () => StMath.randfloat(-360, 360),
        colorStartFunc = () => color.white,
        colorEndFunc = () => (255, 255, 255, 0),
    };
}

public void draw()
{
    particles!.draw();
}
```

You can then either adjust the parameters to get the right effect, or change the functions to do something different. Since they're functions you can do any math stuff you want.

You can then use `particles!.emit()` to emit particles when needed.