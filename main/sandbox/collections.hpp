/* tests cat++ collections and also ptr<T> too why not */
#pragma once
#include <stdio.h>
#include "catplusplus.hpp"
#include "collections/collections.hpp"

namespace sandbox {

void collections() {
    // stack
    ptr<stack> sta = ptr(new stack());
    int32 stuff1 = 11, stuff2 = 22, stuff3 = 33, stuff4 = 44;
    sta->push(&stuff1);
    sta->push(&stuff2);
    sta->push(&stuff3);
    sta->push(&stuff4);

    while (sta->length() > 0) {
        printf("stack item %i\n", *((int*)(sta->pop())));
    }

    // queue (evil stack)
    ptr<queue> que = ptr(new queue());
    que->push(&stuff1);
    que->push(&stuff2);
    que->push(&stuff3);
    que->push(&stuff4);

    while (que->length() > 0) {
        printf("queue item %i\n", *((int*)(que->pop())));
    }
}

}