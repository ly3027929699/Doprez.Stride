# Doprez.Stride
A bundle of Stride related libraries.

# Build Status
[![CircleCI](https://dl.circleci.com/status-badge/img/gh/Doprez/Doprez.Stride/tree/master.svg?style=svg)](https://dl.circleci.com/status-badge/redirect/gh/Doprez/Doprez.Stride/tree/master)

# Examples

## Player Controller Example

```
public class PlayerController : SyncScript
{
	[DataMember(0)]
	public Keys ForwardKey { get; set; }
	[DataMember(1)]
	public Keys BackwardKey { get; set; }
	[DataMember(2)]
	public Keys RightKey { get; set; }
	[DataMember(3)]
	public Keys LeftKey { get; set; }
	[DataMember(100)]
	public Keys Interact { get; set; }
	[DataMember(200)]
	public Keys JumpKey { get; set; }

	private PlayerMover _playerMover;
	private Raycast _raycast;

	public override void Start()
	{
		_playerMover = Entity.GetComponent<PlayerMover>();
		_raycast = Entity.GetComponent<Raycast>();
		Input.Mouse.LockPosition(true);
	}

	public override void Update()
	{
		MovePlayer();
		RotateCamera();
		Jump();
		PlayerRaycast();
	}

	private void MovePlayer()
	{
		Vector2 movement = Vector2.Zero;

		if (Input.IsKeyDown(ForwardKey))
		{
			movement.Y += 1;
		}
		if (Input.IsKeyDown(BackwardKey))
		{
			movement.Y -= 1;
		}
		if (Input.IsKeyDown(RightKey))
		{
			movement.X += 1;
		}
		if (Input.IsKeyDown(LeftKey))
		{
			movement.X -= 1;
		}
		_playerMover.MovePlayer(movement);
	}

	private void RotateCamera()
	{
		_playerMover.UpdateCameraRotation(Input.Mouse.Delta * 100);
	}

	private void Jump()
	{
		if (Input.IsKeyPressed(JumpKey))
		{
			_playerMover.Jump();
		}
	}

	//Example of the Raycast Component usage to store a HitResult in the result variable
	private void PlayerRaycast()
	{
		if (Input.IsKeyPressed(Interact))
		{
			var result = _raycast.RayCast(_playerMover.CameraPivot);
		}
	}
}
```

# Helpful Extensions

## EntityComponent

 - DestroyEntity() provides a quick way to remove an entity from the scene.
 - GetYAngleToTarget(OtherEntity) get the angle to the target entity.
 - GetComponent() similar to Unity. Unlike Strides default Get() this one will also be able to grab non EntityComponent classes like Interfaces.
 - GetComponents() similar to Unity. Unlike Strides default Get() this one will also be able to grab non EntityComponent classes like Interfaces as an IEnumerable.
 - WorldPosition() an easier way to get the world position.

## ModelComponent

 - GetMeshHeight() will get the Y height of a model.

## ScriptComponent

 - DeltaTime() a faster way to get delta time.
 - GetCamera() Gets the camera with the name main in the Scene
 - GetCamera(name) Gets the camera with a custom name in the Scene
 - GetFirstCamera() Gets the first camera available in the GraphicsCompositor

## PhysicsComponent

 - All WIP as they dont seem to consitantly work.

## Game

- FPS() a quick way to get FPS
