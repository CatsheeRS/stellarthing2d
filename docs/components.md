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

## lib

Dependencies i've included in the source code

- [lua](https://lua.org) v5.4.7: scripting language for modding, the engine also uses lua's string type