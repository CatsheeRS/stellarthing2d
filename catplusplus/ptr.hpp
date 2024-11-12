/* literally just std::shared_ptr */
#pragma once
#include "nums.hpp"

/* literally just std::shared_ptr (i stole this) */
template <typename T>
class ptr {
private:
	T* val = nullptr;
	size* refs = nullptr;

public:
	ptr(T* ptr) : val(ptr), refs(0) {}

	ptr(const ptr& obj)
	{
		val = obj.val;
		refs = obj.refs;
		if (obj.val != nullptr) {
			(*this->refs)++;
		}
	}

	ptr& operator=(const ptr& obj)
	{
		cleanup();
		
		val = obj.val;
		refs = obj.refs;
		if (obj.val != nullptr) {
			(*refs)++;
		}
	}

	ptr(ptr&& obj)
	{
		val = obj.val;
		refs = obj.refs;
		obj.val = obj.refs = nullptr;
	}

    size get_ref_count() const {
		return *refs;
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
        printf("delete");
	    cleanup();
	}

private:
	void cleanup()
	{
		(*refs)--;
		if (*refs == 0) {
			if (val != nullptr) {
				delete val;
            }
			delete refs;
		}
	}
};

// templates break my lsp, so i made a version with void* instead
/* literally just std::shared_ptr (i stole this) */
/*class ptr {
private:
	void* val = nullptr;
	size* refs = nullptr;

public:
	ptr(void* ptr) : val(ptr), refs(0) {}

	ptr(const ptr& obj)
	{
		val = obj.val;
		refs = obj.refs;
		if (obj.val != nullptr) {
			(*this->refs)++;
		}
	}

	ptr& operator=(const ptr& obj)
	{
		cleanup();
		
		val = obj.val;
		refs = obj.refs;
		if (obj.val != nullptr) {
			(*refs)++;
		}
	}

	ptr(ptr&& obj)
	{
		val = obj.val;
		refs = obj.refs;
		obj.val = obj.refs = nullptr;
	}

    size get_ref_count() const {
		return *refs;
	}

	void* get() const {
		return val;
	}

	void* operator->() const {
		return val;
	}

	void* operator*() const {
		return val;
	}

	~ptr() {
	    cleanup();
	}

private:
	void cleanup()
	{
		(*refs)--;
		if (*refs == 0) {
			if (val != nullptr) {
				delete val;
            }
			delete refs;
		}
	}
};*/