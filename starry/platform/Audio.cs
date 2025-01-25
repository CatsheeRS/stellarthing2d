using System;
using LibVLCSharp.Shared;
namespace starry;

/// <summary>
/// it's audio. powered by libvlc so it supports pretty much every format you can open with VLC.
/// </summary>
public record class Audio: IAsset {
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
    /// volume multiplier. 1 is the normal volume
    /// </summary>
    public double volume {
        get => vol;
        set {
            vol = value;
            if (lplayer == null || rplayer == null) return;
            lplayer.Volume = (int)(vol * 100 * (1 - Math.Max(0, pain)));
            rplayer.Volume = (int)(vol * 100 * (1 + Math.Max(0, pain)));
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
            if (lplayer == null || rplayer == null) return;
            lplayer.Volume = (int)(vol * 100 * (1 - Math.Max(0, pain)));
            rplayer.Volume = (int)(vol * 100 * (1 + Math.Max(0, pain)));
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
            lplayer?.SetPause(!value);
            rplayer?.SetPause(!value);
        }
    }
    MediaPlayer? lplayer;
    MediaPlayer? rplayer;
    Media? media;

    public static LibVLC? vlc;

    public void load(string path)
    {
        Graphics.actions.Enqueue(() => {
            if (vlc == null) {
                Starry.log("Can't load audio file; libvlc hasn't been initialized yet");
                return;
            }

            // vlc doesn't have this fancy panning so we have to make 2 media players
            lplayer = new MediaPlayer(vlc);
            rplayer = new MediaPlayer(vlc);
            lplayer.SetChannel(AudioOutputChannel.Left);
            rplayer.SetChannel(AudioOutputChannel.Right);

            media = new Media(vlc, path);
        });
        Graphics.actionLoopEvent.Set();
    }

    public void cleanup()
    {
        Graphics.actions.Enqueue(() => {
            lplayer?.Dispose();
            rplayer?.Dispose();
            media?.Dispose();
        });
        Graphics.actionLoopEvent.Set();
    }

    /// <summary>
    /// it plays audio :)
    /// </summary>
    public void play()
    {
        Graphics.actions.Enqueue(() => {
            if (lplayer == null || rplayer == null || media == null) return;
            lplayer.Play(media);
            rplayer.Play(media);
        });
        Graphics.actionLoopEvent.Set();
    }

    /// <summary>
    /// it stops the audio :)
    /// </summary>
    public void stop() {
        Graphics.actions.Enqueue(() => {
            lplayer?.Stop();
            rplayer?.Stop();
        });
        Graphics.actionLoopEvent.Set();
    }

    // engine stuff
    public static unsafe void create()
    {
        // i'm not sure if libvlc requires that but im using this just in case
        Graphics.actions.Enqueue(() => {
            Core.Initialize();

            vlc = new LibVLC();
            // this fills your console with pointless crap
            //vlc.Log += (sender, e) => Starry.log($"[{e.Level}] {e.Module}:{e.Message}");

            Starry.log("Initialized audio");
        });
        Graphics.actionLoopEvent.Set();
    }

    public static unsafe void cleanupButAtTheEndBecauseItCleansUpTheBackend()
    {
        Graphics.actions.Enqueue(() => {
            vlc?.Dispose();
        });
        Graphics.actionLoopEvent.Set();
    }
}