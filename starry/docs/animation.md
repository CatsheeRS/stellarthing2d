# Animation

To make an animation you can use `AnimationSprite`
\
\
It will try load frame then a number (starting at 1)
If the file extension is `.png` you don't need to provide the file extension
```cs
AnimationSprite spr = new(duration, "frame", ".jpg");

//same as the legacy method
AnimationSprite spr = new(duration,
    await Starry.load<Sprite>("frame1"),
    await Starry.load<Sprite>("frame2"),
    await Starry.load<Sprite>("frame3"),
    await Starry.load<Sprite>("frame4")
    // more frames here
);
```



You can then use that in any place that expects `ISprite`