using Godot;
using System;

public partial class level_screen : CanvasLayer
{
	[Export] private PackedScene levelScene;
	private level_1 level;
	[Export] private PackedScene playerScene;
	private player joueur;
	public int score;
	public override void _Ready()
	{
		
	}
	public override void _Process(double delta)
	{
		
	}
	private void _on_continuer_pressed()
	{
		level = levelScene.Instantiate<level_1>();
		joueur = playerScene.Instantiate<player>();
		joueur.Instancier(score);
		level.Instancier(joueur);
		this.GetParent().AddChild(level);
		Visible = false;
		// GetTree().ChangeSceneToFile("res://scene/level_1.tscn");
	}
	public void getPlayer(player p)
	{
		joueur = p;
		Modifcore(joueur.score);
		score = joueur.score;
	}
	public void Modifcore(int val)
	{
		GetNode<Label>("ames/val").Text = val.ToString();
	}
}
