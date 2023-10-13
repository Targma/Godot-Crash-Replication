using System.Threading.Tasks;
using Godot;

namespace Test;

public partial class Main : Node2D
{
	public const bool InstantiateOnMainThread = false;
	public const bool SkipLabel = false;
	
    
	private static readonly Vector2 InitialPosition = new(50, 350);
	private static readonly Vector2 InitialPosition2 = new(350, 350);
	
	private PackedScene _packedScene;
	private PackedScene _packedScene2;
	private double Timer { get; set; } = 2f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_packedScene = GD.Load<PackedScene>("res://NumberLabel.tscn");
		_packedScene2 = GD.Load<PackedScene>("res://SpriteNode.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Timer -= delta;
		if (Timer > 0) return;
		Timer = 2f;

		if (InstantiateOnMainThread)
		{
			if (!SkipLabel)
			{
				var node = _packedScene.Instantiate<NumberLabel>();
				node.Position = InitialPosition;
				AddChild(node);	
			}
			
			var node2 = _packedScene2.Instantiate<SpriteNode>();
			node2.Position = InitialPosition2;
			CallDeferred(Node.MethodName.AddChild, node2);
		}
		else
		{
			Task.Run(() =>
			{
				if (!SkipLabel)
				{
					var node = _packedScene.Instantiate<NumberLabel>();
					node.Position = InitialPosition;
					CallDeferred(Node.MethodName.AddChild, node);
				}

				var node2 = _packedScene2.Instantiate<SpriteNode>();
				node2.Position = InitialPosition2;
				CallDeferred(Node.MethodName.AddChild, node2);
			});	
		}
	}
}