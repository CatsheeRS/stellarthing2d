using System;
namespace starry;

/// <summary>
/// exceptions suck
/// </summary>
public struct Result<T> {
    T? val;
    Error? err;
    /// <summary>
    /// if true, the result is valid
    /// </summary>
    public bool hasval { get; private set; }

    public Result(T val)
    {
        hasval = true;
        this.val = val;
    }

    public Result(Error err)
    {
        hasval = false;
        this.err = err;
    }

    /// <summary>
    /// only use if you're certain that there's a value, it throws and crashes and dies otherwise
    /// </summary>
    public readonly T unwrap()
    {
        // we check for null so c# doesn't complain
        if (val != null && hasval) {
            return val;
        }
        else {
            throw new Exception("There is, in fact, no value");
        }
    }

    /// <summary>
    /// handsome function you can chain so you don't have to check for errors every millisecond
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public Result<T> tryto(Func<Result<T>> func)
    {
        if (!hasval) return this;

        Result<T> haha = func();
        if (haha.hasval) {
            
        }
    }
}