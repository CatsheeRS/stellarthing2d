using Godot;

namespace stellarthing;

[GlobalClass]
public partial class Block : RigidBody2D
{
    [Export]
    public string BlockName { get; set; } = "";
    [Export]
    public Texture Preview { get; set; }
}