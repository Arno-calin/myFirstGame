using Godot;
using System;

public partial class bonus : Sprite2D
{
	private void _on_area_2d_body_entered(Node2D body)
	{
		if (body is player)
		{
			player p = body as player;
			p.speed += 200.0f;
			if (p.speed >= 800.0f)
				p.speed = 800.0f;
			GetNode<Area2D>("Area2D").Monitoring = false;
			Visible = false;
		}
	}
}
