using AssemblyCSharp.Assets.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public sealed class CameraMovement : MonoBehaviour
	{
		private Camera localCamera;

		private CameraConfig cameraConfig;

		private CharacterData character;

		private void Awake() => localCamera = GetComponent<Camera>();

		public void Initialize(CharacterData character, CameraConfig cameraConfig)
		{
			this.character = character;
			this.cameraConfig = cameraConfig;
		}

		private Vector3 Position
		{
			get => localCamera.transform.position;
			set => localCamera.transform.position = value;
		}

		private void Update()
		{
			if (character != null && cameraConfig != null)
			{
				float offset = cameraConfig.xOffset;
				if (character.IsFacingLeft)
				{
					offset *= -1;
				}
				Vector3 target = new(character.X + offset, cameraConfig.yOffset, Position.z);
				Position = Vector3.Distance(target, Position) > cameraConfig.snapDistance
					? target
					: Vector3.Lerp(Position, target, cameraConfig.speed * Time.deltaTime);
			}
		}
	}
}
