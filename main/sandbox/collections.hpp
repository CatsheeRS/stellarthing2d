/* tests cat++ strings */
#pragma once
#include <stdio.h>
#include "catplusplus.hpp"

namespace sandbox {

void collections() {
    auto sta = new stack();
    int32 stuff1 = 69, stuff2 = 420, stuff3 = 69420, stuff4 = 42069;
    sta->push(&stuff1);
    sta->push(&stuff2);
    sta->push(&stuff3);
    sta->push(&stuff4);

    while (sta->length() > 0) {
        printf("item %i\n", *((int*)(sta->pop())));
    }
}

}