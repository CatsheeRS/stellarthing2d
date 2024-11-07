workspace "stellarthing"  
    configurations { "Debug", "Release" }
    startproject "stellarthing"

project "starry"
    location "starry"
    kind "SharedLib"
    language "C++"   
    targetdir "bin/%{cfg.buildcfg}-%{cfg.system}-%{cfg.architecture}" 
    files { "%{prj.name}/**.hpp", "%{prj.name}/**.cpp" } 

    filter "configurations:*"
		defines { "SFML_STATIC" }
		includedirs { "dependencies/SFML/include" }
		libdirs { "dependencies/SFML/lib" }
		links
		{
			"opengl32",
			"freetype",
			"winmm",
			"gdi32",
			"flac",
			"vorbisenc",
			"vorbisfile",
			"vorbis",
			"ogg",
			"ws2_32"
		}


    filter "configurations:Debug"
        defines { "STARRY_DEBUG" }  
        symbols "On" 

        links {
			"sfml-graphics-s-d",
			"sfml-window-s-d",
			"sfml-system-s-d",
			"sfml-audio-s-d",
			"sfml-network-s-d"
		}

    filter "configurations:Release"  
        defines { "STARRY_RELEASE" }    
        optimize "On"

        links {
			"sfml-graphics-s",
			"sfml-window-s",
			"sfml-system-s",
			"sfml-audio-s",
			"sfml-network-s"
		}
    
    filter "system:Windows"
        systemversion "latest"
        defines { "STARRY_WINDOWS" }

        postbuildcommands {
            ("{COPY} %{cfg.buildtarget.relpath} ../bin/" ..
                "%{cfg.buildcfg}-%{cfg.system}-%{cfg.architecture}" .. "/stellarthing")
        }

    filter "system:Unix"
        defines { "STARRY_LINUXBSD" }

        postbuildcommands {
            ("{COPY} %{cfg.buildtarget.relpath} ../bin/" ..
                "%{cfg.buildcfg}-%{cfg.system}-%{cfg.architecture}" .. "/stellarthing")
        }

project "stellarthing"
    location "stellarthing"
    kind "ConsoleApp"
    language "C++"   
    targetdir "bin/%{cfg.buildcfg}-%{cfg.system}-%{cfg.architecture}" 
    files { "%{prj.name}/**.hpp", "%{prj.name}/**.cpp" } 

    filter "configurations:Debug"
        defines { "DEBUG" }  
        symbols "On" 

    filter "configurations:Release"  
        defines { "NDEBUG" }    
        optimize "On"
