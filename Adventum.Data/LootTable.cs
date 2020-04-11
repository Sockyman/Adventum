using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using MonoGame.Framework.Content.Pipeline;

namespace Adventum.Data
{
	public class LootTable
	{
		[ContentSerializer(FlattenContent = true)]
		public LootEntry[] entries = new LootEntry[0];
	}

	public class LootEntry
	{
		[ContentSerializer(FlattenContent = true)]
		public string type;
		[ContentSerializer(Optional = true)]
		public float weight = 1;

		public Point range;
	}
}
