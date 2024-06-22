using Godot;
using System;
using System.Threading;

public partial class level_1 : Node2D
{
	[Export] private PackedScene enemyScene;
	private int count = 0;
	private int countLevel = 0;
	private int level = 10;
	public Random alea;
	public player joueur;
	public override void _Ready()
	{
		alea = new Random();
		this.AddChild(joueur);
	}
	public void Instancier(player p)
	{
		joueur = p;
	}
	public override void _Process(double delta)
	{
		count += 1;
		if (count == level)
		{
			ennemy foe = enemyScene.Instantiate<ennemy>();
			int posXY = alea.Next(4);
			if (posXY == 0)
				foe.Position = new Vector2(0, alea.Next(0, 768));
			if (posXY == 1)
				foe.Position = new Vector2(alea.Next(0, 1280), 768);
			if (posXY == 2)
				foe.Position = new Vector2(1280, alea.Next(0, 768));
			if (posXY == 3)
				foe.Position = new Vector2(alea.Next(0, 1280), 0);
			count = 0;
			countLevel++;
			this.GetNode<Node2D>("ennemies").AddChild(foe);
		}
		if (countLevel == 2)
		{
			countLevel = 0;
			level -= 10;
		}
		if (level == 0 && GetNode<Node2D>("ennemies").GetChildren().Count == 0)
		{
			Thread.Sleep(1000);
			GetParent().GetNode<level_screen>("levelScreen").Visible = true;
			GetParent().GetNode<level_screen>("levelScreen").getPlayer(joueur);
			joueur.QueueFree();
			QueueFree();
			// GetTree().ChangeSceneToFile("res://scene/level_screen.tscn");
		}
	}
}
