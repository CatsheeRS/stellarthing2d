/* one string type to rule them all */
#include "string.hpp"

uchar string::at(size idx) {
    return data.at(idx);
}

const char *string::as_cstr() {
    return data.c_str();
}
