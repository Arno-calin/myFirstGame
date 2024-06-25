using Godot;
using System;

public partial class level_screen : CanvasLayer
{
	[Export] private PackedScene levelScene;
	private level_1 level;
	private int wave = 0; // ne doit pas etre négatif
	[Export] private PackedScene playerScene;
	private player joueur;
	public int scoreJoueur = 10;
	public int vieJoueur = 50;
	public int degatJoueur = 5;
	public float vitesseJoueur = 300.0f;
	public int stunJoueur = 300;
	private int vie = 100;
	[Export] private PackedScene escapeScene;
	
	public override void _Ready()
	{
		
	}
	public override void _Process(double delta)
	{
		
	}
	private void _on_continuer_pressed()
	{
		wave++;
		if (wave == 10)
		{
			level_2 lvl = escapeScene.Instantiate<level_2>();
			lvl.Instancier(vie, vieJoueur, degatJoueur, vitesseJoueur, stunJoueur);
			this.GetParent().AddChild(lvl);
		}
		else
		{
			level = levelScene.Instantiate<level_1>();
			joueur = playerScene.Instantiate<player>();
			joueur.Instancier(scoreJoueur, vieJoueur, degatJoueur, vitesseJoueur, stunJoueur);
			level.Instancier(joueur, wave, vie);
			this.GetParent().AddChild(level);
		}
		Visible = false;
	}
	public void getPlayer(player p, int vie)
	{
		joueur = p;
		nextLevelInfo(joueur.score, wave);
		scoreJoueur = joueur.score;
		vieJoueur = joueur.lifePointMax;
		degatJoueur = joueur.damage;
		vitesseJoueur = joueur.speed;
		stunJoueur = joueur.stunMax;
		this.vie = vie;
	}
	public void nextLevelInfo(int score, int level)
	{
		GetNode<Label>("ames/val").Text = score.ToString();
		GetNode<Label>("level/val").Text = level.ToString();
	}
	public void nextLevelInfo(int score)
	{
		GetNode<Label>("ames/val").Text = score.ToString();
	}
	private void _on_vie_pressed()
	{
		if (scoreJoueur >=  getSoul("vie/ames/val") && vieJoueur < 100)
		{
			scoreJoueur -= getSoul("vie/ames/val");
			vieJoueur += 10;
			soul("vie/ames/val", getSoul("vie/ames/val") + 5);
		}
		else if (vieJoueur >= 100)
		{
			GetNode<Button>("vie").Text = "Max atteint";
		}
		nextLevelInfo(scoreJoueur);
	}
	private void _on_degat_pressed()
	{
		if (scoreJoueur >=  getSoul("degat/ames/val") && degatJoueur < 15)
		{
			scoreJoueur -= getSoul("degat/ames/val");
			degatJoueur += 1;
			soul("degat/ames/val", getSoul("degat/ames/val") + 5);
		}
		else if (degatJoueur >= 15)
		{
			GetNode<Button>("degat").Text = "Max atteint";
		}
		nextLevelInfo(scoreJoueur);
	}
	private void _on_vitesse_pressed()
	{
		if (scoreJoueur >=  getSoul("vitesse/ames/val") && vitesseJoueur < 800.0f)
		{
			scoreJoueur -= getSoul("vitesse/ames/val");
			vitesseJoueur += 50.0f;
			soul("vitesse/ames/val", getSoul("vitesse/ames/val") + 5);
		}
		else if (vitesseJoueur >= 800.0f)
		{
			GetNode<Button>("vitesse").Text = "Max atteint";
		}
		nextLevelInfo(scoreJoueur);
	}
	private void _on_stun_pressed()
	{
		if (scoreJoueur >=  getSoul("stun/ames/val") && stunJoueur > 50)
		{
			scoreJoueur -= getSoul("stun/ames/val");
			stunJoueur -= 25;
			soul("stun/ames/val", getSoul("stun/ames/val") + 5);
		}
		else if (stunJoueur <= 50)
		{
			GetNode<Button>("stun").Text = "Max atteint";
		}
		nextLevelInfo(scoreJoueur);
	}
	private void soul(string path, int newVal)
	{
		GetNode<Label>(path).Text = newVal.ToString();
	}
	private int getSoul(string path)
	{
		return Convert.ToInt16(GetNode<Label>(path).Text);
	}
}
