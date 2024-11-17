# Cat++

Cat++ is an alternative standard library type thing since I don't like the STL.

NOTE: DO NOT abbreviate Cat++ as "catpp", that sounds suspicious.

## Various types

Cat++ comes with versions of stdint.h numbers without the _t part, for example `int64` instead of `int64_t`

## Collections

You can use these collections:
- `array<T>`: safe wrapper around a C array
- `vector<T>`: dynamically sized array
- `list<T>`: linked list
- `stack<T>`: LIFO linked list
- `queue<T>`: FIFO linked list

Why not just use `vector<T>` for everything? Because performance and stuff.
- `array<T>`: if the collection isn't gonna change size
- `vector<T>`: if the collection changes size and you're gonna be accessing by index
- `list<T>`: if the collection changes size but you're only gonna be accessing back and forth
- `stack<T>`: if you're gonna be accessing the newest item first
- `queue<T>`: if you're gonna be accessing the oldest item first

## Memory management

Wrap your stuff in `ptr<T>` to get reference counting (similar to `std::shared_ptr<T>`)
