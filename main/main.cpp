//#include "raylib.h"
//#include "resource_dir.hpp"	// utility header for SearchAndSetResourceDir
#include "sandbox/sandbox.hpp"
#include "lua_module/lua_module.hpp"

int main()
{
	// init modules
	// lua is important since the whole engine just uses its strings
	lua::create();

	sandbox::collections();
	sandbox::raylib();
	return 0;

	// annihilate modules
	lua::cleanup();
}
