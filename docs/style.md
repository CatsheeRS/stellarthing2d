## C Coding Style Conventions

Shout to https://github.com/raysan5/raylib-game-template/blob/main/CONVENTIONS.md (i stole it directly)

Code element | Convention | Example
--- | :---: | ---
Defines | ALL_CAPS | `#define PLATFORM_DESKTOP`
Macros | ALL_CAPS | `#define MIN(a, b) (((a) < (b)) ? (a) : (b))`
Variables | snake_case | `int64 screen_width = 0;`, `float64 target_frame_time = 0.016f;`
Local variables | snake_case | `vec2 player_position = { 0 };`
Constants | snake_case | `const uint64 max_value = 8;`
Pointers | my_type* pointer | `texture2d* array = nullptr;`
float values | always x.xf | `float64 gravity = 10.0f`
Operators | value1 * value2 | `int64 product = value * 6;`
Operators | value1 / value2 | `int64 division = value / 4;`
Operators | value1 + value2 | `int64 sum = value + 10;`
Operators | value1 - value2 | `int64 res = value - 5;`
Enum | snake_case | `enum texture_format`
Enum members | ALL_CAPS | `PIXELFORMAT_UNCOMPRESSED_R8G8B8`
Struct/class | snake_case | `class texture2d`, `struct material`
Struct members | lowerCase | `texture.width`, `color.r`
Functions | snake_case | `init_window()`, `load_image_from_memory()`
Functions params | snake_case | `width`, `height`
Ternary Operator | condition ? result1 : result2 | `log("Value is 0: %s", (value == 0)? "yes" : "no");`
Documentation comments | /\* Documentation \*/ | `/* This function does something. */`

Other conventions:
 - All defined variables are ALWAYS initialized
 - Four spaces are used, instead of TABS
 - Trailing spaces are always avoided
 - Control flow statements are followed **by a space**:
```c
if (condition) value = 0;

while (!window_should_close()) {

}

for (int i = 0; i < NUM_VALUES; i++) printf("%i", i);

switch (value) {
    case 0: {

    } break;
    case 2: break;
    default: break;
}
```
 - Braces and curly brackets open-close in aligned mode in functions with more than 1 line:
```c
int main()
{
    while (!window_should_close()) {
        // do something
    }
    return 0;
}

int get_five() {
    return 5;
}
```

## Files and Directories Naming Conventions

  - Directories are named using `snake_case`: `resources/models`, `resources/fonts`
  - Files are named using `snake_case`: `main_title.png`, `cubicmap.png`, `sound.wav`

_NOTE: Spaces and special characters are always avoided in the files/dir naming!_

Directories should have header files that include the entire directory, e.g. `modules/thing_module/thing_module.h`