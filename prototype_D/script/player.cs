using Godot;
using System;

public partial class player : CharacterBody2D
{
	public AnimatedSprite2D animatedSprite;
	public int lifePoint;
	public int lifePointMax;
	public int damage;
	public int score;
	public float speed;
	
	private int stun;
	public int stunMax;
	
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public bool isGravity = false;
	public bool isonFloor = false;
	public bool up = false;
	
	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		GetParent().GetNode<gui>("GUI").score(score);
	}
	public void Instancier(int score, int life, int damage, float speed, int stun)
	{
		this.score = score;
		this.damage = damage;
		this.lifePoint = life;
		this.lifePointMax = life;
		this.speed = speed;
		this.stun = stun;
		this.stunMax = stun;
	}
	public override void _PhysicsProcess(double delta)
	{

		if (lifePoint <= 0)
		{
			animatedSprite.Play("dead");
			stun--;
			if (stun == 0)
			{
				lifePoint = lifePointMax;
				GetParent().GetNode<Label>("GUI/fullHeal").Visible = true;
				stun = stunMax;
			}
		}

		else
		{
			Vector2 velocity = Velocity;
			Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
			
			if (isonFloor)
			{
				velocity.Y = gravity * (float) delta * speed;
			}
			else if (direction != Vector2.Zero)
			{
				if (isGravity)
					velocity.X = direction.X * speed;
				else if (up)
					velocity.Y = -Math.Abs(direction.Y * speed);
				else
				{
					velocity.X = direction.X * speed;
					velocity.Y = direction.Y * speed;
				} 
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
				velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
				velocity.Y = Mathf.MoveToward(Velocity.Y, 0, speed);
				animatedSprite.Play("idle");
			}
			
			var collision = MoveAndCollide(velocity * (float)delta);
			if(collision != null && collision.GetCollider() is ennemy)
			{
				ennemy foe = collision.GetCollider() as ennemy;
				GetParent().GetNode<Label>("GUI/fullHeal").Visible = false;
				lifePoint -= foe.damage;
				foe.lifePoint -= damage;
				if (foe.lifePoint <= 0)
				{
					score++;
					GetParent().GetNode<gui>("GUI").score(score);
				}
				MoveAndCollide(foe.push * velocity * (float)delta);
			}
			if (isGravity)
			{
				if(collision != null && collision.GetCollider() is TileMap)
					isonFloor = false;
				else
					isonFloor = true;
			}
		}
	}
}
