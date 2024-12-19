using System;
namespace starry;

/// <summary>
/// timer. must be manually started :)
/// </summary>
public class Timer(double duration, bool loop) {
    /// <summary>
    /// how long the timer lasts, in seconds
    /// </summary>
    public double duration { get; set; } = duration;
    /// <summary>
    /// if true the timer loops
    /// </summary>
    public bool loop { get; set; } = loop;
    /// <summary>
    /// how much is left for the timer to end, in seconds
    /// </summary>
    public double timeLeft { get; internal set; } = duration;
    public bool playing { get; internal set; } = false;
    public delegate void TimeoutEvent();
    /// <summary>
    /// invoked when the timer ends
    /// </summary>
    public event TimeoutEvent? timeout;

    static ConcurrentHashSet<Timer> timers = [];

    /// <summary>
    /// starts the timer :)
    /// </summary>
    public void start()
    {
        timers.Add(this);
        playing = true;
    }

    /// <summary>
    /// stops the timer :)
    /// </summary>
    public void stop()
    {
        timers.Remove(this);
        playing = false;
    }

    internal static void update()
    {
        foreach (Timer why in timers) {
            why.timeLeft -= Window.deltaTime;
            if (why.timeLeft < 0.01) {
                why.timeout?.Invoke();
                if (why.loop) why.timeLeft = why.duration;
            }
        }
    }
}