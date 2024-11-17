/* safe array */
#pragma once
#include "nums.hpp"

// templates break my lsp
//typedef void* T;

/* safe array :D */
template <typename T>
class array {
private:
    T* arr;
    size len;

public:
    array(size size)
    {
        // idk how to do that with new
        arr = (T*)malloc(size * sizeof(T));
        len = size;
    }

    /* returns the length of the array */
    size length() {
        return len;
    }

    /* returns the item at that index */
    T at(size idx)
    {
        // TODO add error
        if (idx >= len) {
            return T{};
        }
        return arr[idx];
    }

    /* sets the item at that index */
    void set(size idx, T val)
    {
        // TODO add error
        if (idx >= len) {
            return;
        }
        arr[idx] = val;
    }

    ~array() {
        delete arr;
    }
};