/* tests cat++ strings */
#include <stdio.h>
#include "catplusplus.hpp"
#include "sandbox.hpp"

void sandbox::string_tests() {
    ptr<string> s = str("¡Hola mamá!");
    printf("str: %s", s->as_cstr());
    /*string str = L"¡Hi móm!";
    string* str2 = &str;
    printf("str references: %" PRIu64 "\n", str2->get_ref_count()); // should be 2
    printf("str = %s\n", str.as_cstr()); // should be "str = ¡Hi móm!"
    printf("6th character = %" PRIuLEAST32 "\n", str.at(6)); // should be ó*/
}