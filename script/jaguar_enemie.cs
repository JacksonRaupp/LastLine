using Godot;
using System;

public partial class jaguar_enemie : CharacterBody2D
{
    public const float Speed = 200.0f;
	public const float JumpVelocity = -400.0f;

    public bool IsMovingLeft  = false;
    
    public Vector2 velocity;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public bool IsAttack = false;
	public override void _PhysicsProcess(double delta)
	{

		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		Velocity = velocity;
		MoveAndSlide();
	}

    public override void _Process(double delta)
    {
        if (!IsAttack)
        {
             Movement();
            TurnAround();
        }
  }

    public void Movement()
    {
             var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
            if (IsMovingLeft)
            {
                velocity.X = -Speed;
            }
            else
            {
                velocity.X = Speed;    
            }
            Velocity = Velocity.Normalized() * Speed;

            animatedSprite2D.Play("walk");
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

    private void _on_player_detector_body_entered(Node2D body)
    {
        var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite2D.Play("attack");
        velocity.X = 0;
        IsAttack = !IsAttack;
        Velocity = velocity; 
    }

    private void _on_hit_box_body_entered(CharacterBody2D body)
    {
        GD.Print("TESTETASEDAS");
        Vector2 velocity = body.Velocity; 
        

        velocity.Y = JumpVelocity;
        body.Velocity = velocity;
     }

}
