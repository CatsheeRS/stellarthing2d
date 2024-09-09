namespace starry;

/// <summary>
/// base of graphic instructions
/// </summary>
public interface IGraphicInstruction {
    public void call();
    public bool is3d();
}