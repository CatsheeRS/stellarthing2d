/* stack collection implemented through linked lists */
#pragma once
#include "nums.hpp"
#include "ref.hpp"

/* stack collection implemented through linked lists */
class stack : public ref {
private:
    struct node {
        void* data;
        node* next;
    };
    
    size len = 0;
    node* top = nullptr;
public:
    /* returns the size of the stack */
    size length();

    /* puts a new item at the top (start) of the stack */
    void push(void* data);

    /* removes the item at the top (start) of the stack */
    void* pop();

    /* returns the item at the top (start) without removing it */
    void* peek();

    ~stack();
};