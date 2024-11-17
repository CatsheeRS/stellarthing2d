/* tests cat++ collections and also ptr<T> too why not */
#pragma once
#include <stdio.h>
#include "catplusplus.hpp"
#include "collections/collections.hpp"

namespace sandbox {

void collections() {
    // stack
    ptr<stack<int32>> sta = ptr(new stack<int32>());
    sta->push(11);
    sta->push(22);
    sta->push(33);
    sta->push(44);

    while (sta->length() > 0) {
        printf("stack item %i\n", sta->pop());
    }

    // queue (evil stack)
    ptr<queue<int32>> que = ptr(new queue<int32>());
    que->push(11);
    que->push(22);
    que->push(33);
    que->push(44);

    while (que->length() > 0) {
        printf("queue item %i\n", que->pop());
    }

    // array
    ptr<array<int32>> arr = ptr(new array<int32>(4));
    arr->set(0, 11);
    arr->set(1, 22);
    arr->set(2, 33);
    arr->set(3, 44);

    for (size i = 0; i < arr->length(); i++) {
        printf("array item %i\n", arr->at(i));
    }
    
}

}