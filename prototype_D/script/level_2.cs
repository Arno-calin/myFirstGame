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
	public int countSpawn = 0;
	public int whenZoom = -10000;
	public bool isTime = true;
	public int countTime = 0;
	public override void _Ready()
	{
		alea = new Random();
		GetNode<player>("Player").Instancier(10, vieJoueur, degatJoueur, vitesseJoueur+2200.0f, stunJoueur);
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
		if (count == 30 && countSpawn < 100 && isTime)
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
			if (countSpawn % 20 == 0)
				isTime = false;
		}
		if (!isTime && GetNode<Node2D>("ennemies").GetChildren().Count == 0)
		{
			isTime = true;
			count = 0;
		}
		if (countSpawn == 100)
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
			if (whenZoom == -10000 && pos.Y < -3500)
				GetNode<player>("Player").up = true;
			if (whenZoom > -19000 && pos.Y <= whenZoom)
			{
				GetNode("Player").GetNode<Camera2D>("Camera2D").Zoom += new Vector2(0.1f, 0.1f);
				whenZoom -= 1000;
			}
			else if (whenZoom == -19000)
			{
				GetNode("Player").GetNode<Camera2D>("Camera2D").Zoom = new Vector2(-1f, -1f);
				GetNode<player>("Player").up = false;
				whenZoom -= 500;
			}
			else if (whenZoom == -19500 && Math.Abs(pos.X) >= 10000)
			{
				GetNode<player>("Player").isGravity = true;
				whenZoom -= 500;
			}
			else if (whenZoom == -20000 && pos.Y > 10000)
			{
				GetNode("Player").GetNode<Camera2D>("Camera2D").Zoom = new Vector2(1f, 1f);
				whenZoom -= 1000;
				secondGame = false;
			}
		}
	}
	private void _on_area_2d_body_entered(Node2D body)
	{
		GetNode("Player").GetNode<Camera2D>("Camera2D").Zoom = new Vector2(0.1f, 0.1f);
	}
	private void _on_theend_body_entered(Node2D body)
	{
		GetTree().ChangeSceneToFile("res://scene/end.tscn");
	}
}






