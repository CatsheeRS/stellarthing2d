/* one string type to rule them all */
#include "string.hpp"
#include "lua_module/lua_module.hpp"
#include <stdio.h>
#include <lua.hpp>

string::string(const char *s)
{
    // there's no error because errors themselves require strings
    if (!lua::created) {
        printf("The Lua module must be initialized to use Starry strings.");
        exit(1);
        return;
    }

    lua_pushstring(lua::L, s);
    luastrref = luaL_ref(lua::L, LUA_REGISTRYINDEX);
}

const char* string::as_cstr()
{
    lua_rawgeti(lua::L, LUA_REGISTRYINDEX, luastrref);
    const char* str = lua_tostring(lua::L, -1);
    lua_pop(lua::L, 1);
    return str;
}

string::~string() {
    luaL_unref(lua::L, LUA_REGISTRYINDEX, luastrref);
}
