/* stack collection implemented through linked lists */
#pragma once
#include "nums.hpp"

// templates break my lsp
//typedef void* T;

/* stack collection implemented through linked lists */
template <typename T>
class stack {
private:
    struct node {
        T data;
        node* next;
    };
    
    size len = 0;
    node* top = nullptr;
public:
    /* returns the size of the stack */
    size length() {
        return len;
    }

    /* puts a new item at the top (start) of the stack */
    void push(T data)
    {
        node* n = new node();
        n->data = data;
        n->next = top;
        top = n;
        len++;
    }

    /* removes the item at the top (start) of the stack */
    T pop()
    {
        if (top == nullptr) {
            // TODO: add error
            return T{};
        }
        node* tmp = top;
        T data = tmp->data;
        top = top->next;
        delete tmp;
        len--;
        return data;
    }

    /* returns the item at the top (start) without removing it */
    T peek()
    {
        if (top == nullptr) {
            // TODO: add error
            return T{};
        }
        return top->data;
    }

    ~stack()
    {
        printf("rip\n");
        while (len != 0) {
            pop();
        }
    }
};