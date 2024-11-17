#pragma once
#include <lua.hpp>

/* manages lua stuff */
namespace lua {
    lua_State* L;
    void create();
    void cleanup();
}
