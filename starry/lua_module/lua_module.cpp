#include "lua_module.hpp"
#include <lua.hpp>

void lua::create()
{
    L = luaL_newstate();
    luaL_openlibs(L);
}

void lua::cleanup()
{
    lua_close(L);
}
