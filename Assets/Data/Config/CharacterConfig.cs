using System;
using UnityEngine;

namespace AssemblyCSharp.AssetsData.Data.Config
{
	[Serializable]
	public sealed class CharacterConfig
	{
		public float baseSpeed;
		
		[Range(1, 50)]
		public int staminaGrowth = 2;
	}
}
