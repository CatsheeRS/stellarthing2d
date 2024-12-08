using System;
using NAudio.Wave;

namespace starry;

public class AudioManager
{
    internal static WaveOutEvent defaultOut = new();

    public WaveOutEvent audOut { get; private set; } = new();
    private Audio currentlyPlaying;
    private WaveChannel32 channel;

    public float volume { get; private set; }
    public float pan { get; private set; }

    public AudioManager()
    {
        volume = 1;
        pan = 0;
    }
    
    public void play(Audio aud)
    {
        Graphics.actions.Enqueue(() =>
        {
            if (currentlyPlaying != null)
                audOut.Stop();

            channel = new(aud.audioreader, volume, pan);
            audOut.Init(channel);

            currentlyPlaying = aud;
            audOut.Play();
        });
        Graphics.actionLoopEvent.Set();
    }

    public void setPosition(vec2 position, vec2 listenerPosition)
    {
        float deltaX = (float)(position.x - listenerPosition.x);
        float deltaY = (float)(position.y - listenerPosition.y);

        float distance = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);
        float pan = System.Math.Clamp(deltaX / distance, -1.0f, 1.0f);

        this.pan = pan;

        if (channel != null)
        {
            channel.Pan = pan;
            channel.Volume = volume / (volume + distance);
        }
    }

    public void cleanup()
    {
        audOut.Dispose();
        channel?.Dispose();
    }

    public void pause() => audOut.Pause();

    public void stop() => audOut.Stop();

    public void resume() => audOut.Play();

    public void setVolume(float volume)
    {
        this.volume = volume;
        if (channel != null)
        {
            channel.Volume = volume;
        }
    }
}