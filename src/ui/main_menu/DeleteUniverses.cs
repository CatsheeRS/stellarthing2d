using Godot;
using System;

namespace stellarthing;

public partial class DeleteUniverses : Button
{
	[Export]
	public ListUniverses Iwasinthemiddlegroundlookingtofindthefountainofinfinitemirror { get; set; }
	[Export]
	public Button Treefallingnoonewouldhear { get; set; }
	[Export]
	public Button Shadowofnobodythere { get; set; }
	[Export]
	public Control Murdersofmurdererslivinginfearofit { get; set; }

    public override void _Pressed()
    {
		// DIE! THY END IS NOW!
		string die = Iwasinthemiddlegroundlookingtofindthefountainofinfinitemirror.GetItemText(Iwasinthemiddlegroundlookingtofindthefountainofinfinitemirror.GetSelectedItems()[0]);

		using var dir = DirAccess.Open($"user://universes/{die}");
		dir.IncludeHidden = true;
		dir.IncludeNavigational = false;
        dir.ListDirBegin();
        string filename = dir.GetNext();

        while (filename != "") {
            dir.Remove($"user://universes/{die}/{filename}");
            filename = dir.GetNext();
        }

		DirAccess.RemoveAbsolute($"user://universes/{die}");

		Iwasinthemiddlegroundlookingtofindthefountainofinfinitemirror.Fuck();
		Treefallingnoonewouldhear.Disabled = true;
		Shadowofnobodythere.Disabled = true;
		Murdersofmurdererslivinginfearofit.Visible = false;
    }
}
