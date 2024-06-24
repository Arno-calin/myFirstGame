using Godot;
using System;

public partial class bonus2 : Sprite2D
{
	private void _on_area_2d_body_entered(Node2D body)
	{
		if (body is player)
		{
			player p = body as player;
			p.damage += 5;
			if (p.damage >= 15)
				p.damage = 15;
			GetNode<Area2D>("Area2D").Monitoring = false;
			Visible = false;
		}
	}
}
