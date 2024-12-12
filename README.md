# Stellarthing

A game about space things

# Starry Engine

starry is the next biggest ever replacement of unity unreal godot game maker cryengine frostbite source 2 rpg maker lumberyard defold panda3d flax monogame libgdx ogre bevy phaser armory gdevelop cocos2d renpy urho3d stride haxeflixel pygame blender game engine fucking idk

best game engine in the world awards 1921 winner

10th best game engine when it comes to usability awards 3000 BC winner

## Epic features
- Modern C# without being batshit insane
- Overkill graphics (it uses [skia](https://skia.org/) (but [the c# version](https://github.com/mono/SkiaSharp)))
- Acceptable math structs
- Assets work
- Async await everywhere
- Tilemap.
- Blazingly fast and very handsome entity system
- COMING SOON: Sick sound effect and music system
- COMING SOON: Multiplayer probably
- COMING SOON: Acceptable UI
- COMING SOON: Modding through lua

# How to build

just install .net 8.

there's these dependencies but i'm pretty sure nuget handles that
- GLFW (through Silk.NET)
- SkiaSharp
- Newtonsoft.Json

to release:
```sh
dotnet publish --os win -c Release --sc
dotnet publish --os linux -c Release --sc
```

it's somewhere in `stellarthing/bin/Release/` then the publish folder

if you're using windows you can try [this tutorial](https://www.google.com/search?q=how+to+install+linux)