# Components

All of these are folders in the src folder, each component is based on the lower component

## stellarthing

This is the actual game

## starry

This is the higher level part of the engine, mostly connecting all of the systems and having nice APIs for you to use

## sleep

This is a layer of abstraction over raylib, which allows me to change frameworks and makes me need less type conversions

## catplusplus

A standard library type thing since I don't like C++'s STL

## dependencies

- [Raylib](https://raylib.com): it's a game library, important since stellarthing is a game
- [Lua](https://lua.org): embedded scripting language, I also stole their strings