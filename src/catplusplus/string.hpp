/* one string type to rule them all */
#pragma once
#include "ref.hpp"
#include "nums.hpp"
#include <string>

/* unicode character */
typedef char32_t uchar;

/* one string type to rule them all */
class string : public ref {
private:
    std::string data;
public:
    string(std::string s);
    /* gets the character at that index */
    uchar at(size idx);
};
