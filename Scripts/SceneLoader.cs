using Godot;
using System;

public partial class SceneLoader : Node
{
	// Called when the node enters the scene tree for the first time.
	[Export] public PackedScene NextLevel;
	[Export]public int winSize; 
	
	public void TryWin(int size)
	{
		Console.WriteLine("Size: " + size + "\nWin Size: " + winSize);
		if(size >= winSize)
		{
			GetTree().ChangeSceneToPacked(NextLevel);
		}
	}
}
