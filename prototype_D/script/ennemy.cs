using Godot;
using System;

public partial class ennemy : CharacterBody2D
{
	public int danger = 1;
	public int explosion = 10;
	public int lifePoint = 10;
	public int damage = 5;
	public int speed = 100;
	
	public treasure target; 
	public Vector2 velocity;
	
	public override void _Ready()
	{
		target = GetParent().GetParent().GetNode<treasure>("Treasure");
		velocity = new Vector2(target.Position.X - Position.X, target.Position.Y - Position.Y);
		velocity = velocity / (1000 / speed);
	}
	
	public override void _Process(double delta)
	{
		if (lifePoint <= 0)
		{
			QueueFree();
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		Velocity = velocity;
		MoveAndSlide();
	}
}
