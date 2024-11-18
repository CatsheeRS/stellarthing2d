/* utility functions */
#pragma once
#include "ptr.hpp"
#include "string.hpp"

/* strings are quite common, ptr<string>(new string("a")) is a bit much */
inline ptr<string> str(const char* s) {
    return ptr<string>(new string(s));
}