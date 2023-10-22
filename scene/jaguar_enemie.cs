using Godot;
using System;

public partial class jaguar_enemie : CharacterBody2D
{
    public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

    public bool IsMovingLeft  = false;
    
    public Vector2 velocity;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{

		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		Velocity = velocity;
		MoveAndSlide();
	}

    public override void _Process(double delta)
    {
        var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        TurnAround();
        if (IsMovingLeft)
        {
            velocity.X = -Speed;
        }
        else
        {
            velocity.X = Speed;    
        }
        Velocity = Velocity.Normalized() * Speed;

        animatedSprite2D.Play();
    }

    public void TurnAround()
    {
        var raycastGround = GetNode<RayCast2D>("RayCast2DGround");
        var raycastWall = GetNode<RayCast2D>("RayCast2DWall");
        if ((!raycastGround.IsColliding() || raycastWall.IsColliding()) && IsOnFloor() && IsMovingLeft)
        {
            IsMovingLeft = !IsMovingLeft;
            Scale = new Vector2(Scale.X * (-1), Scale.Y);
        }
        else if ((!raycastGround.IsColliding() || raycastWall.IsColliding()) && IsOnFloor() && !IsMovingLeft)
        {
            IsMovingLeft = !IsMovingLeft;
            Scale = new Vector2(Scale.X * (-1), Scale.Y);
        }

    }

}
