using Godot;
using System;

namespace stellarthing;

public partial class ListUniverses : ItemList {
	[Export]
	public Button FuckingButton { get; set; }
	[Export]
	public Button OtherFuckingButton { get; set; }

    public override void _Ready()
    {
		Fuck();
    }

	public void Fuck()
	{
		Clear();
		DirAccess.MakeDirRecursiveAbsolute("user://universes/");
        using var dir = DirAccess.Open("user://universes");
		dir.IncludeHidden = true;
		dir.IncludeNavigational = false;
        dir.ListDirBegin();
        string filename = dir.GetNext();

        while (filename != "") {
            AddItem(filename);
            filename = dir.GetNext();
        }
	}

	void Hdgfhsdjtdu5ujtdghs(int idx)
	{
		FuckingButton.Disabled = false;
		OtherFuckingButton.Disabled = false;
	}
}
