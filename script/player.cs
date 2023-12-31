using Godot;
using System;

public partial class player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	public int Helth = 100;

	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
			velocity.X = direction.X * Speed;
		else
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);

		Velocity = velocity;
		MoveAndSlide();

		if (velocity.X != 0)
		{
			animatedSprite2D.Animation = "run";
			animatedSprite2D.FlipH = velocity.X < 0;
		}
		else if (velocity.Y != 0)
			animatedSprite2D.Animation = "jump";
		else
			animatedSprite2D.Animation = "idle";
	}

	public void _on_hitbox_body_entered (Node2D body)
	{
		Helth -= 10;
		GD.Print("enter");
		GD.Print(body.Name);
		GD.Print(Helth);
	}
}
