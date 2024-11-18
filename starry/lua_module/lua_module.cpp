#include "lua_module.hpp"
#include <lua.hpp>

void lua::create()
{
    L = luaL_newstate();
    luaL_openlibs(L);
    created = true;
}

void lua::cleanup()
{
    created = false;
    lua_close(L);
}
