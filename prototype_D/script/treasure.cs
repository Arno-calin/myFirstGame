using Godot;
using System;

public partial class treasure : Sprite2D
{
	public int pointLife = 100;
	
	public override void _Process(double delta)
	{
		if (pointLife <= 0)
		{
			GetTree().ChangeSceneToFile("res://scene/title_screen.tscn");
		}
	}
	private void _on_area_2d_body_entered(Node2D body)
	{
		if (body is player)
		{
			player p = body as player;
			p.lifePoint = p.lifePointMax;
		}
		else if (body is ennemy)
		{
			ennemy foe = body as ennemy;
			pointLife -= foe.explosion;
			GetParent().GetNode<gui>("GUI").pv(pointLife);
			foe.QueueFree();
		}
	}
	public void setPV(int val)
	{
		pointLife = val;
		GetParent().GetNode<gui>("GUI").pv(pointLife);
	}
}
