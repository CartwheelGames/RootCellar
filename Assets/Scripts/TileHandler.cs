using AssemblyCSharp.AssetsData.Data.State;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public class TileHandler : MonoBehaviour
	{
		public Tile data;

		[SerializeField]
		private SpriteRenderer frontRenderer;

		[SerializeField]
		private SpriteRenderer mainRenderer;

		[SerializeField]
		private SpriteRenderer topRenderer;

		public Sprite FrontSprite
		{
			set
			{
				frontRenderer.sprite = value;
				frontRenderer.enabled = value != null;
			}
		}

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
			frontRenderer.enabled = mainRenderer.enabled = topRenderer.enabled = false;
			frontRenderer.sprite = mainRenderer.sprite = topRenderer.sprite = null;
		}
	}
}
