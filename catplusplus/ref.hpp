/* reference counting implemented through inheritance since it's nicer */
#pragma once
#include "nums.hpp"

/* reference counting implemented through inheritance since it's nicer. you're supposed to inherit this class when you need automatic memory management */
class ref {
private:
    uint64 refs = 0;

public:
    /* get how many references this object has */
    uint64 get_ref_count();

    void* operator new(size size);
    void operator delete(void* ptr);
};