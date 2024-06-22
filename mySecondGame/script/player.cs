using Godot;
using System;

public partial class player : CharacterBody2D
{
	public const float SPEED = 600.0f;
	AnimatedSprite2D animatedSprite;
	public int lifePoint = 20;
	public int damage = 5;
	public int score = 0;

	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		GetParent().GetNode<gui>("GUI").score(score);
	}
	public void Instancier(int score)
	{
		this.score = score;
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * SPEED;
			velocity.Y = direction.Y * SPEED;
			animatedSprite.Play("walk");
			if (direction.X > 0)
			{
				Rotation = (float) Math.PI/2;
				if (direction.Y > 0)
					Rotation = 3 * (float) Math.PI/4;
				if (direction.Y < 0)
					Rotation = (float) Math.PI/4;
			}
			else if (direction.X < 0)
			{
				Rotation = (float) -Math.PI/2;
				if (direction.Y > 0)
					Rotation = 3 *(float) -Math.PI/4;
				if (direction.Y < 0)
					Rotation = (float) -Math.PI/4;
			}
			else
			{
				if (direction.Y > 0)
				{
					Rotation = (float) Math.PI;
				}
				else if (direction.Y < 0)
				{
					Rotation = 0;
				}
			}
		
		}
		else
		{
			Rotation = 0;
			velocity.X = Mathf.MoveToward(Velocity.X, 0, SPEED);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, SPEED);
			animatedSprite.Play("idle");
		}
		
		var collision = MoveAndCollide(velocity * (float)delta);
		if(collision != null && collision.GetCollider() is ennemy)
		{
			ennemy foe = collision.GetCollider() as ennemy;
			lifePoint -= foe.damage;
			foe.lifePoint -= damage;
			if (foe.lifePoint <= 0)
			{
				score++;
				GetParent().GetNode<gui>("GUI").score(score);
			}
			MoveAndCollide(-10 * velocity * (float)delta);
		}
	}
}
