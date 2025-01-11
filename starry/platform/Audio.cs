using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.OpenAL;
namespace starry;

/// <summary>
/// it's audio. please note only .ogg is supported
/// </summary>
public record class Audio: IAsset {
    uint source = 0;
    uint buffer = 0;
    internal static AL? al;
    internal static ALContext? alc;
    internal static unsafe Device* device;
    internal static unsafe Context* context;

    public static unsafe void create()
    {
        Graphics.actions.Enqueue(() => {
            alc = ALContext.GetApi();
            al = AL.GetApi();
            device = alc.OpenDevice("");
            if (device == null) {
                Starry.log("Couldn't create device");
                return;
            }

            context = alc.CreateContext(device, null);
            alc.MakeContextCurrent(context);
            al.GetError();

            Starry.log("OpenAL has been initialized");
        });
        Graphics.actionLoopEvent.Set();
    }

    /// <summary>
    /// there's no good library for this lmao
    /// </summary>
    internal static unsafe Task<(uint, uint)> parsewav(string path)
    {
        TaskCompletionSource<(uint, uint)> mate = new();
        Graphics.actions.Enqueue(() => {
            // stolen from https://github.com/dotnet/Silk.NET/blob/main/examples/CSharp/OpenAL%20Demos/WavePlayer/Program.cs
            // im not smart
            ReadOnlySpan<byte> file = File.ReadAllBytes(path);
            int i = 0;
            if (file[i++] != 'R' || file[i++] != 'I' || file[i++] != 'F' || file[i++] != 'F') {
                Starry.log($"{path} is not in RIFF format");
                mate.SetResult((0, 0));
                return;
            }

            var chunkSize = BinaryPrimitives.ReadInt32LittleEndian(file.Slice(i,  4));
            i += 4;

            if (file[i++] != 'W' || file[i++] != 'A' || file[i++] != 'V' || file[i++] != 'E') {
                Starry.log($"{path} is not in WAVE format");
                mate.SetResult((0, 0));
                return;
            }

            short numChannels = -1;
            int sampleRate = -1;
            int byteRate = -1;
            short blockAlign = -1;
            short bitsPerSample = -1;
            BufferFormat format = 0;
            
            var source = al!.GenSource();
            var buffer = al.GenBuffer();
            al.SetSourceProperty(source, SourceBoolean.Looping, true);

            while (i + 4 < file.Length) {
                var identifier = "" + (char)file[i++] + (char)file[i++] + (char)file[i++] + (char)file[i++];
                var size = BinaryPrimitives.ReadInt32LittleEndian(file.Slice(i, 4));
                i += 4;
                if (identifier == "fmt ") {
                    if (size != 16) {
                        Starry.log($"Unknown Audio Format with subchunk1 size {size}");
                    }
                    else {
                        var audioFormat = BinaryPrimitives.ReadInt16LittleEndian(file.Slice(i, 2));
                        i += 2;
                        if (audioFormat != 1) {
                            Starry.log($"Unknown Audio Format with ID {audioFormat}");
                        }
                        else {
                            numChannels = BinaryPrimitives.ReadInt16LittleEndian(file.Slice(i, 2));
                            i += 2;
                            sampleRate = BinaryPrimitives.ReadInt32LittleEndian(file.Slice(i, 4));
                            i += 4;
                            byteRate = BinaryPrimitives.ReadInt32LittleEndian(file.Slice(i, 4));
                            i += 4;
                            blockAlign = BinaryPrimitives.ReadInt16LittleEndian(file.Slice(i, 2));
                            i += 2;
                            bitsPerSample = BinaryPrimitives.ReadInt16LittleEndian(file.Slice(i, 2));
                            i += 2;
                    
                            if (numChannels == 1) {
                                if (bitsPerSample == 8) format = BufferFormat.Mono8;
                                else if (bitsPerSample == 16) format = BufferFormat.Mono16;
                                else {
                                    Starry.log($"Can't Play mono {bitsPerSample} sound.");
                                }
                            }
                            else if (numChannels == 2) {
                                if (bitsPerSample == 8) format = BufferFormat.Stereo8;
                                else if (bitsPerSample == 16) format = BufferFormat.Stereo16;
                                else {
                                    Starry.log($"Can't Play stereo {bitsPerSample} sound.");
                                }
                            }
                            else {
                                Starry.log($"Can't play audio with {numChannels} sound");
                            }
                        }
                    }
                } 
                else if (identifier == "data") {
                    var data = file.Slice(i, size);
                    i += size;
                    
                    fixed (byte* pData = data) {
                        al.BufferData(buffer, format, pData, size, sampleRate);
                    }
                    //Starry.log($"Read {size} bytes Data");
                }
                else if (identifier == "JUNK") {
                    // this exists to align things
                    i += size;
                }
                else if (identifier == "iXML") {
                    var v = file.Slice(i, size);
                    var str = Encoding.ASCII.GetString(v);
                    //Starry.log($"iXML Chunk: {str}");
                    i += size;
                }
                else {
                    //Starry.log($"Unknown Section: {identifier}");
                    i += size;
                }
            }

            /*Starry.log (
                $"Success. Detected RIFF-WAVE audio file, PCM encoding. {numChannels} Channels, {sampleRate} Sample Rate, {byteRate} Byte Rate, {blockAlign} Block Align, {bitsPerSample} Bits per Sample"
            );*/

            mate.SetResult((source, buffer));
        });
        Graphics.actionLoopEvent.Set();
        return mate.Task;
    }

    public static unsafe void cleanupButAtTheEndBecauseItCleansUpOpenAl()
    {
        Graphics.actions.Enqueue(() => {
            alc!.DestroyContext(context);
            alc.CloseDevice(device);
            al!.Dispose();
            alc.Dispose();
        });
        Graphics.actionLoopEvent.Set();
    }

    public async void load(string path) => (source, buffer) = await parsewav(path);

    public void cleanup()
    {
        Graphics.actions.Enqueue(() => {
            al!.DeleteSource(source);
            al.DeleteBuffer(buffer);
        });
        Graphics.actionLoopEvent.Set();
    }

    /// <summary>
    /// it plays audio :)
    /// </summary>
    public void play()
    {
        Graphics.actions.Enqueue(() => {
            al!.SetSourceProperty(source, SourceInteger.Buffer, buffer);
            al.SourcePlay(source);
        });
        Graphics.actionLoopEvent.Set();
    }

    /// <summary>
    /// pauses the audio. you can resume the audio with <c>play()</c>
    /// </summary>
    public void pause()
    {
        Graphics.actions.Enqueue(() => {
            al!.SetSourceProperty(source, SourceInteger.Buffer, buffer);
            al.SourcePause(source);
        });
        Graphics.actionLoopEvent.Set();
    }

    /// <summary>
    /// it stops the audio :)
    /// </summary>
    public void stop() {
        Graphics.actions.Enqueue(() => {
            al!.SetSourceProperty(source, SourceInteger.Buffer, buffer);
            al.SourceStop(source);
        });
        Graphics.actionLoopEvent.Set();
    }
}