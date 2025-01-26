using System;
using MiniaudioSharp;
namespace starry;

/// <summary>
/// it's audio. supported formats are wav, mp3, and flac. i would support ogg but this is just using miniaudio and i'm too stupid to add ogg support
/// </summary>
public class Audio: IAsset {
    // Field 'Audio.engine' is never assigned to, and will always have its default value 
    // shut the fuck up
    unsafe static ma_engine* engine;
    unsafe ma_sound* snd;
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
            iCantMakeAnUnsafeSetter1(vol);
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
            iCantMakeAnUnsafeSetter2(pain);
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

    public unsafe void load(string path)
    {
        Graphics.actions.Enqueue(() => {
            // i know
            if (Miniaudio.ma_sound_init_from_file(engine, Starry.string2sbytePtr(path),
            (uint)ma_sound_flags.MA_SOUND_FLAG_STREAM, null, null, snd) != ma_result.MA_SUCCESS) {
                Starry.log($"Couldn't load {path}.");
            }
        });
        Graphics.actionLoopEvent.Set();
    }

    public unsafe void cleanup()
    {
        Graphics.actions.Enqueue(() => {
            Miniaudio.ma_sound_uninit(snd);
        });
        Graphics.actionLoopEvent.Set();
    }

    /// <summary>
    /// it plays audio :)
    /// </summary>
    public unsafe void play()
    {
        Graphics.actions.Enqueue(() => {
            if (Miniaudio.ma_sound_start(snd) != ma_result.MA_SUCCESS) {
                Starry.log("Couldn't play audio.");
            }
        });
        Graphics.actionLoopEvent.Set();
    }

    /// <summary>
    /// it stops the audio :)
    /// </summary>
    public unsafe void stop() {
        Graphics.actions.Enqueue(() => {
            if (Miniaudio.ma_sound_stop(snd) != ma_result.MA_SUCCESS) {
                Starry.log("It seems audio is busted.");
            }
        });
        Graphics.actionLoopEvent.Set();
    }

    // engine stuff
    public static unsafe void create()
    {
        Graphics.actions.Enqueue(() => {
            if (Miniaudio.ma_engine_init(null, engine) != ma_result.MA_SUCCESS) {
                throw new Exception("Couldn't initialize audio engine (Miniaudio)");
            }

            Starry.log("Initialized Miniaudio");
        });
        Graphics.actionLoopEvent.Set();
    }

    public static unsafe void cleanupButAtTheEndBecauseItCleansUpTheBackend()
    {
        Graphics.actions.Enqueue(() => {
            Miniaudio.ma_engine_uninit(engine);
            Starry.log("Cleaned up Miniaudio");
        });
        Graphics.actionLoopEvent.Set();
    }

    unsafe void iCantMakeAnUnsafeSetter1(double vol) {
        if (snd == null) return;
        Miniaudio.ma_sound_set_volume(snd, (float)vol);
    }

    unsafe void iCantMakeAnUnsafeSetter2(double pan) {
        if (snd == null) return;
        Miniaudio.ma_sound_set_pan(snd, (float)pan);
    }
}