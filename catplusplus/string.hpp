/* one string type to rule them all */
#pragma once
#include "nums.hpp"
#include <lua.hpp>

/* unicode character */
typedef wchar_t uchar;

/* one string type to rule them all */
class string {
private:
    std::wstring data;
public:
    string(std::wstring s) : data(s) {};
    string(const uchar* s) : data(s) {};
    
    /* gets the character at that index */
    uchar at(size idx);
    /* returns a C string version */
    const uchar* as_cstr();
};
