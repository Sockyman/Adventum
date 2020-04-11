using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventum.Item
{
	public class KeyCollectable : Collectable
	{
		public override string ID
		{
			get
			{
				return "key_" + sprite.AnimationName;
			}
		}

		public KeyCollectable() : base("key", "KeyDoor")
		{
			Console.WriteLine(ID);
		}
	}
}
