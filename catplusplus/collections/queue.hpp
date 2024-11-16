/* queue collection implemented through linked lists */
#pragma once
#include "nums.hpp"

/* queue collection implemented through linked lists */
class queue {
private:
    struct node {
        void* data;
        node* next;
    };
    
    size len = 0;
    node* front = nullptr;
    node* rear = nullptr;
public:
    /* returns the size of the queue */
    size length();

    /* puts a new item at the end of the queue */
    void push(void* data);

    /* removes the item at the start of the queue */
    void* pop();

    /* returns the item at the start without removing it */
    void* peek();

    ~queue();
};