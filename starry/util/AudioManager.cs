using System;
using ManagedBass;
namespace starry;

public class AudioManager
{
    private Audio? currentlyPlaying;
    private int currentStream = 0;
    
    public float volume { get; private set; } = 1;
    public float pan { get; private set; } = 0;
    
    public void play(Audio aud)
    {
        Graphics.actions.Enqueue(() =>
        {
            if (Bass.Init())
            {
                currentStream = Bass.CreateStream(aud.path);
                if (currentStream != 0)
                {
                    Bass.ChannelPlay(currentStream);
                    currentlyPlaying = aud;
                    
                    setVolume(volume);
                    setPan(pan);
                }
                else
                {
                    Starry.log(Bass.LastError);
                }
            }
            else
            {
                Starry.log("no more bass");
            }
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
        //setPan(pan);
        //setVolume(volume / (volume + distance));
    }

    public void pause()
    {
        if (currentStream != 0)
        {
            Bass.ChannelPause(currentStream);
        }
    }
    
    public void unpause()
    {
        if (currentStream != 0)
        {
            Bass.ChannelPlay(currentStream);
        }
    }
    
    public void stop()
    {
        if (currentStream != 0)
        {
            Bass.ChannelStop(currentStream);
        }
    }
    
    public void cleanup()
    {
        if (currentStream != 0)
        {
            Bass.StreamFree(currentStream);
            Bass.Free();
        }
    }

    public void setVolume(float volume)
    {
        this.volume = volume;
        if (currentStream != 0)
        {
            Bass.ChannelSetAttribute(currentStream, ChannelAttribute.Volume, volume);
        }
    }

    public void setPan(float pan)
    {
        if (currentStream != 0)
        {
            Bass.ChannelSetAttribute(currentStream, ChannelAttribute.Pan, pan);
        }
    }

    public static void setGlobalVolume(int volume)
    {
        Bass.GlobalStreamVolume = volume;
    }
    
    public static void stopAllAudio(int volume)
    {
        Bass.Stop();
    }
    
    public static void pauseAllAudio(int volume)
    {
        Bass.Pause();
    }
    

    public static void restartAllAudio(int volume)
    {
        Bass.Start();
    }
}