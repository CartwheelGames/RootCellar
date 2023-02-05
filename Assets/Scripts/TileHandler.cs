using AssemblyCSharp.AssetsData.Data.State;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public class TileHandler : MonoBehaviour
	{
		public Tile data;

		[SerializeField]
		private SpriteRenderer mainRenderer;

		[SerializeField]
		private SpriteRenderer topRenderer;

		public Sprite MainSprite
		{
			set
			{
				mainRenderer.sprite = value;
				mainRenderer.enabled = value != null;
			}
		}

		public Sprite TopSprite
		{
			set
			{
				topRenderer.sprite = value;
				topRenderer.enabled = value != null;
			}
		}

		public void Awake()
		{
			mainRenderer.enabled = topRenderer.enabled = false;
			mainRenderer.sprite = topRenderer.sprite = null;
		}
	}
}
