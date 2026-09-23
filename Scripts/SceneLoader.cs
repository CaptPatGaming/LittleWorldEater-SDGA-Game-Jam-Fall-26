using Godot;
using System;

public partial class SceneLoader : Node
{
	// Called when the node enters the scene tree for the first time.
	[Export] public PackedScene NextLevel;
	[Export]public int winSize; 
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void TryWin(int size)
	{
		if(size >= winSize)
		{
			GetTree().ChangeSceneToPacked(NextLevel);
		}
	}
}
