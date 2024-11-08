/* tests cat++ strings */
#pragma once
#include <stdio.h>
#include <inttypes.h>
#include "catplusplus/catplusplus.hpp"

namespace sandbox {

void string_tests() {
    string str = L"¡Hi móm!";
    string* str2 = &str;
    printf("str references: %" PRIu64 "\n", str2->get_ref_count()); // should be 2
    printf("str = %s\n", str.as_cstr()); // should be "str = ¡Hi móm!"
    printf("6th character = %" PRIuLEAST32 "\n", str.at(6)); // should be ó
}

}