/* one string type to rule them all */
#pragma once
#include "nums.hpp"
#include "ptr.hpp"

/* one string type to rule them all (they're just lua strings) */
class string {
private:
    /* reference to a lua string duh */
    int luastrref;
public:
    string(const char* s);
    
    /* returns a C string version */
    const char* as_cstr();

    ~string();
};