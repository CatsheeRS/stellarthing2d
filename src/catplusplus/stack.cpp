/* stack collection implemented through linked lists */
#include "stack.hpp"
#include <stdio.h>

size stack::length() {
    return len;
}

void stack::push(void* data)
{
    node* n = new node();
    n->data = data;
    n->next = top;
    top = n;
    len++;
}

void* stack::pop()
{
    if (top == nullptr) {
        // TODO: add error
    }
    node* tmp = top;
    void* data = tmp->data;
    top = top->next;
    delete tmp;
    len--;
    return data;
}

void* stack::peek()
{
    if (top == nullptr) {
        // TODO: add error
    }
    return top->data;
}

stack::~stack()
{
    printf("rip\n");
    while (len != 0) {
        pop();
    }
}
