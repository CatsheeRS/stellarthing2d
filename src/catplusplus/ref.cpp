/* reference counting implemented through inheritance since it's nicer */
#include "ref.hpp"
#include "nums.hpp"

uint64 ref::get_ref_count() {
    return refs;
}

void* ref::operator new(size size)
{
    void* ptr = ::operator new(size);
    ref* lopontero = (ref*)ptr;
    lopontero->refs++;
}

void ref::operator delete(void* ptr)
{
    ref* lopontero = (ref*)ptr;
    if (lopontero->refs) {
        delete lopontero;
    }
}