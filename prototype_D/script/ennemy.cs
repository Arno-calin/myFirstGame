using Godot;
using System;

public partial class ennemy : CharacterBody2D
{
	public int explosion;
	public int lifePoint;
	public int damage;
	public int speed;
	public int push = -10;
	
	public treasure target; 
	public Vector2 velocity;
	
	public override void _Ready()
	{
		target = GetParent().GetParent().GetNode<treasure>("Treasure");
		velocity = new Vector2(target.Position.X - Position.X, target.Position.Y - Position.Y);
		velocity = velocity / (1000 / speed);
	}
	public void Instancier(int explo, int life, int dam, int weed)
	{
		explosion = explo;
		lifePoint = life;
		damage = dam;
		speed = weed;
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
