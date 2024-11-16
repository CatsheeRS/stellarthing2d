/* queue collection implemented through linked lists */
#pragma once
#include "nums.hpp"

// templates break my lsp
//typedef void* T;

/* queue collection implemented through linked lists */
template <typename T>
class queue {
private:
    struct node {
        T data;
        node* next;
    };
    
    size len = 0;
    node* front = nullptr;
    node* rear = nullptr;
public:
    /* returns the size of the queue */
    size length() {
        return len;
    }

    /* puts a new item at the end of the queue */
    void push(T data)
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

    /* removes the item at the start of the queue */
    T pop()
    {
        if (front == nullptr) {
            // TODO: add error
            return T{};
        }
        node* tmp = front;
        T value = tmp->data;
        front = front->next;

        if (front == nullptr) {
            rear = nullptr;
        }

        delete tmp;
        len--;
        return value;
    }

    /* returns the item at the start without removing it */
    T peek()
    {
        if (front == nullptr) {
            // TODO: add error
            return T{};
        }
        return front->data;
    }

    ~queue()
    {
        printf("rip\n");
        while (len != 0) {
            pop();
        }
    }
};