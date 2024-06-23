using Godot;
using System;

public partial class gui : CanvasLayer
{
	public void score(int val)
	{
		GetNode<Label>("Label/count").Text = val.ToString();
	}
	public void pv(int val)
	{
		GetNode<Label>("pointDeVieText/pointDeVie").Text = val.ToString();
	}
	public void level(int val)
	{
		GetNode<Label>("level/val").Text = val.ToString();
	}
}
