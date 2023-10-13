using System.Threading.Tasks;
using Godot;

namespace Test;

public partial class Main : Node2D
{
	public const bool InstantiateOnMainThread = false;
	private static readonly Vector2 InitialPosition = new(250, 250);
	
	private PackedScene _packedScene;
	private double Timer { get; set; } = 2f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_packedScene = GD.Load<PackedScene>("res://NumberLabel.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Timer -= delta;
		if (Timer > 0) return;
		Timer = 2f;

		if (InstantiateOnMainThread)
		{
			var node = _packedScene.Instantiate<NumberLabel>();
			node.Position = InitialPosition;
			AddChild(node);
		}
		else
		{
			Task.Run(() =>
			{
				var node = _packedScene.Instantiate<NumberLabel>();
				node.Position = InitialPosition;
				CallDeferred(Node.MethodName.AddChild, node);
			});	
		}
	}
}