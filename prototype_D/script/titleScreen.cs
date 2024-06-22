using Godot;
using System;

public partial class titleScreen : CanvasLayer
{
	private void _on_jouer_pressed()
	{
		GetTree().ChangeSceneToFile("res://scene/game.tscn");
	}
	private void _on_quitter_pressed()
	{
		GetTree().Quit();
	}
}
