# Cat++

Cat++ is an alternative standard library type thing since I don't like the STL.

NOTE: DO NOT abbreviate Cat++ as "catpp", that sounds suspicious.

## Various types

Cat++ comes with versions of stdint.h numbers without the _t part, for example `int64` instead of `int64_t`

You can use these collections:
- `stack<T>`: LIFO linked list
- `queue<T>`: FIFO linked list

## Memory management

Wrap your stuff in `ptr<T>` to get reference counting (similar to `std::shared_ptr<T>`)
