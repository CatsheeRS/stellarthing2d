/* queue collection implemented through linked lists */
#include "queue.hpp"
#include <stdio.h>

size queue::length() {
    return len;
}

void queue::push(void* data)
{
    node* n = new node();
    n->data = data;
    n->next = nullptr;

    if (rear == nullptr) {
        front = rear = n;
    } else {
        rear->next = n;
        rear = n;
    }
    len++;
}

void* queue::pop()
{
    if (front == nullptr) {
        // TODO: add error
        return nullptr;
    }
    node* tmp = front;
    void* value = tmp->data;
    front = front->next;

    if (front == nullptr) {
        rear = nullptr;
    }

    delete tmp;
    return value;
}

void* queue::peek()
{
    if (front == nullptr) {
        // TODO: add error
        return nullptr;
    }
    return front->data;
}

queue::~queue()
{
    printf("rip\n");
    while (len != 0) {
        pop();
    }
}
