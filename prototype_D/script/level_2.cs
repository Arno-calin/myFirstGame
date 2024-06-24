using Godot;
using System;

public partial class level_2 : Node2D
{
	[Export] private PackedScene enemyScene;
	public int vieJoueur;
	public int degatJoueur;
	public float vitesseJoueur;
	public int stunJoueur;
	public bool win = false;
	public bool secondGame = false;
	public Random alea;
	public int count = 0;
	public int countSpawn;
	public int whenZoom = 10000;
	public override void _Ready()
	{
		alea = new Random();
		GetNode<player>("Player").Instancier(10, vieJoueur, degatJoueur, vitesseJoueur+200.0f, stunJoueur);
		GetNode<gui>("GUI").level(10);
	}
	public void Instancier(int pv, int life, int damage, float speed, int stun)
	{
		GetNode<treasure>("Treasure").setPV(pv);
		degatJoueur = damage;
		vieJoueur = life;
		vitesseJoueur = speed;
		stunJoueur = stun;
	}
	public override void _Process(double delta)
	{
		if (count == 30 && countSpawn < 1)
		{
			ennemy foe = enemyScene.Instantiate<ennemy>();
			foe.Scale = new Vector2(5, 5);
			foe.Instancier(5, 15, 20, 50);
			int posXY = alea.Next(4);
			if (posXY == 0)
				foe.Position = new Vector2(-4900, alea.Next(-2600, 3300));
			else if (posXY == 1)
				foe.Position = new Vector2(alea.Next(-4900, 6200), -2600);
			else if (posXY == 2)
				foe.Position = new Vector2(alea.Next(-4900, 6200), 3300);
			else if (posXY == 3)
				foe.Position = new Vector2(6200, alea.Next(-2600, 3300));
			this.GetNode<Node2D>("ennemies").AddChild(foe);
			count = 0;
			countSpawn++;
		}
		if (countSpawn == 1)
			win = true;
		if (win && GetNode<Node2D>("ennemies").GetChildren().Count == 0)
		{
			GetNode<Sprite2D>("door").Visible = false;
			GetNode("Treasure").GetNode<Camera2D>("Camera2D").Enabled = false;
			GetNode("Player").GetNode<Camera2D>("Camera2D").Enabled = true;
			countSpawn++;
			win = false;
			secondGame = true;
		}
		else
			count++;
		if (secondGame)
		{
			Vector2 pos = GetNode<player>("Player").Position;
			if (Math.Abs(pos.X) + Math.Abs(pos.Y) >= whenZoom && whenZoom < 20000)
			{
				GetNode("Player").GetNode<Camera2D>("Camera2D").Zoom += new Vector2(0.1f, 0.1f);
				whenZoom += 1000;
			}
			else if (Math.Abs(pos.X) + Math.Abs(pos.Y) >= whenZoom && whenZoom == 20000)
			{
				GetNode("Player").GetNode<Camera2D>("Camera2D").Zoom = new Vector2(-1f, -1f);
				whenZoom += 2000;
			}
			else if (Math.Abs(pos.X) + Math.Abs(pos.Y) >= whenZoom && whenZoom == 22000)
			{
				GetNode<player>("Player").isGravity = true;
				whenZoom = 0;
			}
			else if (whenZoom == 0 && Math.Abs(pos.X) + Math.Abs(pos.Y) >= whenZoom )
		}
	}
}
