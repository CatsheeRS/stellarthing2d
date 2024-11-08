/* reference counting implemented through inheritance since it's nicer */
#pragma once
#include "nums.hpp"

/* reference counting implemented through inheritance since it's nicer. you're supposed to inherit this class when you need automatic memory management */
class ref {
private:
    uint64 refs = 0;

protected:
    virtual ~ref() = default;

public:
    /* get how many references this object has */
    uint64 get_ref_count() {
        return refs;
    }

    void* operator new(size size)
    {
        void* ptr = ::operator new(size);
        ref* lopontero = (ref*)ptr;
        lopontero->refs++;
    }

    void operator delete(void* ptr)
    {
        ref* lopontero = (ref*)ptr;
        if (lopontero->refs) {
            delete lopontero;
        }
    }
};