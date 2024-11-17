/* dynamically-sized array */
#pragma once
#include "nums.hpp"
#include "string.h"

// templates break my lsp
//typedef void* T;

/* dynamically-sized array. T has to be a pointer */
template <typename T>
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
        len = 0;
        allocsize = size;
    }

    /* returns the length of the vector */
    size length() {
        return len;
    }

    void resize(size capacity)
    {
        T* newdata = (T*)realloc(arr, capacity * sizeof(T));
        // it's not gonna work if the size is 0
        // TODO add error
        if (newdata == nullptr && capacity > 0) {
            return;
        }
        arr = newdata;
        allocsize = capacity;
        if (len > capacity) {
            len = capacity;
        }
    }

    void add(T val)
    {
        if (len == allocsize) {
            size newca = allocsize > 0 ? allocsize * 2 : 1;
            resize(newca);
        }
        // man
        memcpy((char*)arr + len * sizeof(T), val, sizeof(T));
        len++;
    }

    /* returns the item at that index */
    T at(size idx)
    {
        // TODO add error
        if (idx >= len) {
            return nullptr;
        }
        return (T)((char*)arr + idx * sizeof(T));
    }

    /* sets the item at that index if it exists */
    void set(size idx, T val)
    {
        // TODO add error
        if (idx >= len) {
            return;
        }
        memcpy((char*)arr + idx * sizeof(T), val, sizeof(T));
    }

    ~vector() {
        delete arr;
    }
};