# Cat++

Cat++ is an alternative standard library type thing since I don't like the STL.

NOTE: DO NOT abbreviate Cat++ as "catpp", that sounds suspicious.

## Various types

Cat++ comes with versions of stdint.h numbers without the _t part, for example `int64` instead of `int64_t`

For strings use `string`, and for characters use `uchar`, they're unicode and reference counted and stuff

## Memory management

By inheriting `ref` you can get instant and automatic reference counting (similar to `std::shared_ptr<T>`)

Unlike the STL's smart pointers, this applies to the entire type