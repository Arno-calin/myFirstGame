using Godot;
using System;

public partial class border : Area2D
{
	public Random alea;
	
	public override void _Ready()
	{
		alea = new Random();
	}
	private void _on_body_entered(Node2D body)
	{
		if (body is player)
			(body as player).Position = new Vector2(alea.Next(10, 1270), alea.Next(10, 758));
		if (body is ennemy)
			(body as ennemy).QueueFree();
	}
}
