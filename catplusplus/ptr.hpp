/* literally just std::shared_ptr */
#pragma once
#include "nums.hpp"

// templates break my lsp
//typedef void T;

/* literally just std::shared_ptr (i stole this) */
template <typename T>
class ptr {
private:
	/* i think this is why im getting a segfault */
	struct numptr {
		size val;
	};
	
	T* val = nullptr;
	numptr* refs = nullptr;

public:
	ptr(T* ptr) : val(ptr) {
		refs = new numptr();
		refs->val = 1;
	}

	ptr(const ptr& obj)
	{
		val = obj.val;
		refs = obj.refs;
		if (obj.val != nullptr) {
			refs->val++;
		}
	}

	ptr& operator=(const ptr& obj)
	{
		cleanup();
		
		val = obj.val;
		refs = obj.refs;
		if (obj.val != nullptr) {
			refs->val++;
		}
	}

    size get_ref_count() const {
		return refs->val;
	}

	T* get() const {
		return val;
	}

	T* operator->() const {
		return val;
	}

	T* operator*() const {
		return val;
	}

	~ptr() {
	    cleanup();
	}

private:
	void cleanup()
	{
		refs->val--;
		if (refs->val == 0) {
			if (val != nullptr) {
				delete val;
            }
			delete refs;
		}
	}
};