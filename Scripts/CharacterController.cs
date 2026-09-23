using Godot;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

public partial class CharacterController : CharacterBody3D
{
	// Called when the node enters the scene tree for the first time.
	[Export]public int speed = 5;
	[Export][Range(1, 5)]int size = 1;
	[Export]int mass = 0;
	[Export]int[] massGoal;
	[Export]int[] sizeScalar;
	[Export]Node3D collision;
	[Export]Node3D model;
	[Export] SceneLoader sceneLoader;
	public override void _Ready()
	{
		sceneLoader = GetTree().CurrentScene as SceneLoader;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public Vector3 GetDirection()
	{
		Vector3 direction = Vector3.Zero;
		if (Input.IsActionPressed("move_up"))
		{
			direction.Z--;
		}
		if (Input.IsActionPressed("move_down"))
		{
			direction.Z++;
		}
		if (Input.IsActionPressed("move_left"))
		{
			direction.X--;
		}
		if (Input.IsActionPressed("move_right"))
		{
			direction.X++;
		}
		//Debug.WriteLine(direction);
		return direction.Normalized();
	}
	public override void _PhysicsProcess(double delta)
	{
		Velocity = GetDirection() * speed;
		MoveAndSlide();

		for(int i = 0; i < GetSlideCollisionCount(); i++)
		{
			Node collider = GetSlideCollision(i).GetCollider() as Node;
			if(collider != null && collider.HasMeta("size"))
			{
				TryEat(collider, (int)collider.GetMeta("size"), (int)collider.GetMeta("mass"));
			}
		}
	}
	public void TryEat(Node _object, int s, int m)
	{
		if( s < size)
		{
			mass+=m;
			_object.QueueFree();
			if(mass >= massGoal[size - 1])
			{
				size++;
				sceneLoader.TryWin(size);
				Resize();
			}
		}
	}
	public void Resize()
	{
		model.Scale *= sizeScalar[size-1];
		collision.Scale *= sizeScalar[size-1];
	}
}
