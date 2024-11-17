/* dynamically-sized array */
#pragma once
#include "nums.hpp"

// templates break my lsp
typedef void* T;

/* dynamically-sized array */
//template <typename T>
class vector {
private:
    T* arr;
    size len;
    size allocsize;

public:
    vector(size size)
    {
        // idk how to do that with new
        arr = (T*)malloc(size * sizeof(T));
        len = size;
        allocsize = size * sizeof(T);
    }

    /* returns the length of the vector */
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

    /* sets the item at that index if it exists */
    void set(size idx, T val)
    {
        // TODO add error
        if (idx >= len) {
            return;
        }
        arr[idx] = val;
    }

    ~vector() {
        delete arr;
    }
};