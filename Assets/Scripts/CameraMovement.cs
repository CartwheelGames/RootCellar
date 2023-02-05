using AssemblyCSharp.Assets.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public sealed class CameraMovement : MonoBehaviour
	{
		private CameraConfig cameraConfig;

		private GameState gameState;

		public Camera LocalCamera { get; private set; }

		private CharacterData CharacterData => gameState.Character;

		private Vector3 Position
		{
			get => LocalCamera.transform.position;
			set => LocalCamera.transform.position = value;
		}

		public void Initialize(GameState gameState, CameraConfig cameraConfig)
		{
			this.gameState = gameState;
			this.cameraConfig = cameraConfig;
		}

		private void Awake() => LocalCamera = GetComponent<Camera>();

		private void Update()
		{
			if (CharacterData != null && cameraConfig != null)
			{
				float offset = cameraConfig.xOffset;
				if (CharacterData.IsFacingLeft)
				{
					offset *= -1;
				}
				Vector3 target = new(CharacterData.X + offset, cameraConfig.yOffset, Position.z);
				Position = Vector3.Distance(target, Position) > cameraConfig.snapDistance
					? target
					: Vector3.Lerp(Position, target, cameraConfig.speed * Time.deltaTime);
				const float xMargin = 0.5f;
				float halfScreenWidth = LocalCamera.orthographicSize * LocalCamera.aspect;
				float leftExtent = halfScreenWidth - xMargin;
				float rightExtent = gameState.Stage.Tiles.Length - halfScreenWidth - xMargin;
				if (Position.x < leftExtent)
				{
					Position = new Vector3(leftExtent, cameraConfig.yOffset, Position.z);
				}
				else if (Position.x > rightExtent)
				{
					Position = new Vector3(rightExtent, cameraConfig.yOffset, Position.z);
				}
			}
		}
	}
}
