using Godot;

namespace Test;

public partial class NumberLabel : Node2D
{
	private static readonly Vector2 Movement = new(0, -120);
	
	private Label Label { get; set; }
	private double Duration { get; set; } = 3f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Label = GetNode<Label>("%Label");
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