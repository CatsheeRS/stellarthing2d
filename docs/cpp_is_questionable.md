# C++ is questionable: top 10 legal and illegal features

## The STL

Avoid it.

Use Cat++ instead. (see [catplusplus.md](catplusplus.md))

## Structs and classes

They're pretty much the same in C++, but don't use classes for everything.

If it's meant to be used with pointers (e.g. entities), use classes, otherwise (e.g. vectors) use structs.

## Templates

Avoid templates, the editor simply gives up when I write them

Use `void*` and casts instead.