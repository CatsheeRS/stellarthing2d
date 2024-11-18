#pragma once
#include <lua.hpp>

/* manages lua stuff */
namespace lua {
    bool created = false;
    lua_State* L;
    void create();
    void cleanup();
}
