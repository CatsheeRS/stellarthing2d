using System;
using SDL2;
namespace starry;

/// <summary>
/// it's audio. supported formats are wav, mp3, ogg, and flac. powered by sdl2.
/// </summary>
public record class Audio: IAsset {
    bool iswav = false;
    nint sdlwav;
    nint sdlmus;

    vec3? pos = null;
    /// <summary>
    /// the posiiton of the audio. setting it turns your audio into spatial audio, if you want it to go back for some reason just set it to null
    /// </summary>
    public vec3? position {
        get => pos;
        set {
            pos = value;
        }
    }

    double vol = 0;
    /// <summary>
    /// volume multiplier. 1 is the normal volume. this can't go above 300%
    /// </summary>
    public double volume {
        get => vol;
        set {
            vol = Math.Clamp(value, 0, 3);
        }
    }

    double pain = 0;
    /// <summary>
    /// the stereo panning, -1 is completely on the left and 1 is completely on the right
    /// </summary>
    public double pan {
        get => pain;
        set {
            pain = Math.Clamp(value, 0, 1);
        }
    }

    bool elpauso = false;
    /// <summary>
    /// if true, the audio is paused.
    /// </summary>
    public bool paused {
        get => elpauso;
        set {
            elpauso = value;
        }
    }

    public void load(string path)
    {
        Graphics.actions.Enqueue(() => {
            // i fucking hate sdl
            if (path.EndsWith(".wav")) {
                sdlwav = SDL_mixer.Mix_LoadWAV(path);
                if (sdlwav == 0) {
                    Starry.log($"Couldn't load {path}: {SDL_mixer.Mix_GetError()}");
                    return;
                }
            }
            else {
                sdlmus = SDL_mixer.Mix_LoadMUS(path);
                if (sdlmus == 0) {
                    Starry.log($"Couldn't load {path}: {SDL_mixer.Mix_GetError()}");
                    return;
                }
            }
        });
        Graphics.actionLoopEvent.Set();
    }

    public void cleanup()
    {
        Graphics.actions.Enqueue(() => {
            if (iswav) SDL_mixer.Mix_FreeChunk(sdlwav);
            else SDL_mixer.Mix_FreeMusic(sdlmus);
        });
        Graphics.actionLoopEvent.Set();
    }

    /// <summary>
    /// it plays audio :)
    /// </summary>
    public void play()
    {
        Graphics.actions.Enqueue(() => {
            if (iswav) SDL_mixer.Mix_PlayChannel(-1, sdlwav, 0);
            else SDL_mixer.Mix_PlayMusic(sdlmus, 0);
        });
        Graphics.actionLoopEvent.Set();
    }

    /// <summary>
    /// it stops the audio :)
    /// </summary>
    public void stop() {
        Graphics.actions.Enqueue(() => {
            if (iswav)
            SDL_mixer.Mix_HaltMusic()
        });
        Graphics.actionLoopEvent.Set();
    }

    // engine stuff
    public static unsafe void create()
    {
        Graphics.actions.Enqueue(() => {
            if (SDL.SDL_Init(SDL.SDL_INIT_AUDIO) < 0) {
                throw new Exception($"Couldn't initialize SDL2 audio: {SDL.SDL_GetError()}");
            }

            if (SDL_mixer.Mix_OpenAudio(44100, SDL_mixer.MIX_DEFAULT_FORMAT, 2, 2048) < 0) {
                throw new Exception($"Couldn't initialize SDL2 mixer: {SDL_mixer.Mix_GetError()}");
            }
        });
        Graphics.actionLoopEvent.Set();
    }

    public static unsafe void cleanupButAtTheEndBecauseItCleansUpTheBackend()
    {
        Graphics.actions.Enqueue(() => {
            SDL_mixer.Mix_HaltChannel(-1);
            SDL_mixer.Mix_CloseAudio();
            SDL.SDL_Quit();
        });
        Graphics.actionLoopEvent.Set();
    }
}