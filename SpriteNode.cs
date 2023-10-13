using Godot;

namespace Test;

public partial class SpriteNode : Node2D
{
	private static readonly Vector2 Movement = new(0, -80);
	private double Duration { get; set; } = 3f;
	
	private Sprite2D Sprite2D { get; set; }
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Sprite2D = GetNode<Sprite2D>("%Sprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Duration -= delta;
		Position += Movement * (float) delta;
		
		if (Duration > 0) return;
		QueueFree();
	}
}