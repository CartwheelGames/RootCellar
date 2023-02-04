using AssemblyCSharp.AssetsData.Data.Config;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public sealed class CharacterMovement : MonoBehaviour
	{
		private float speed = 1f;

		public void Initialize(CharacterConfig characterConfig)
		{
			speed = characterConfig.baseSpeed;
		}

		public void Update()
		{
			float horizontalInput = Input.GetAxis("Horizontal");
			float verticalInput = Input.GetAxis("Vertical");
			if (horizontalInput < -float.Epsilon)
			{
				transform.Translate(Vector2.left * speed);
			}
			else if (horizontalInput > float.Epsilon)
			{
				transform.Translate(Vector2.right * speed);
			}
			else if (verticalInput < -float.Epsilon)
			{
				Debug.Log("Down");
				// TODO: Context sensitive action based on the current tile
			}
		}
	}
}
