# Animation

To make an animation you can use `AnimationSprite`

```cs
AnimationSprite spr = new(duration,
    await Starry.load<Sprite>("frame1"),
    await Starry.load<Sprite>("frame2"),
    await Starry.load<Sprite>("frame3"),
    await Starry.load<Sprite>("frame4")
    // more frames here
);
```

You can then use that in any place that expects `ISprite`