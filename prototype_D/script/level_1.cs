using Godot;
using System;
using System.Threading;

public partial class level_1 : Node2D
{
	[Export] private PackedScene enemyScene;
	private int count = 0;
	private int countLevel = 0;
	private int level = 100;
	private int invocation;
	private int levelInvocation = 20;
	public Random alea;
	public player joueur;
	public override void _Ready()
	{
		alea = new Random();
		this.AddChild(joueur);
	}
	public void Instancier(player p, int dif, int pv)
	{
		joueur = p;
		GetNode<gui>("GUI").level(dif);
		invocation = dif;
		GetNode<treasure>("Treasure").setPV(pv);
	}
	public override void _Process(double delta)
	{
		count += 1;
		if (count == level)
		{
			ennemy foe = enemyScene.Instantiate<ennemy>();
			foe.Instancier(alea.Next(1, 11), alea.Next(5, 15),alea.Next(5, 15),alea.Next(10, 101));
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
		if (countLevel == invocation)
		{
			countLevel = 0;
			level -= levelInvocation;
		}
		if (level <= 0 && GetNode<Node2D>("ennemies").GetChildren().Count == 0)
		{
			Thread.Sleep(1000);
			GetParent().GetNode<level_screen>("levelScreen").Visible = true;
			GetParent().GetNode<level_screen>("levelScreen").getPlayer(joueur, GetNode<treasure>("Treasure").pointLife);
			joueur.QueueFree();
			QueueFree();
		}
	}
}
