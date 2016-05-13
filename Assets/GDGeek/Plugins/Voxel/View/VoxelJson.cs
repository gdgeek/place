using UnityEngine;
using System.Collections;
using System;
using Pathfinding.Serialization.JsonFx;



namespace GDGeek
{
	[Serializable]
	[JsonOptIn]
	public class VoxelJson {


		[Serializable]
		[JsonOptIn]
		public class Vox
		{
			
			[JsonMember]
			public int x;
			[JsonMember]
			public int y;
			[JsonMember]
			public int z;
			[JsonMember]
			public int c;
		};

		
		[Serializable]
		[JsonOptIn]
		public class Rgba
		{
			
			[JsonMember]
			public int r;
			[JsonMember]
			public int g;
			[JsonMember]
			public int b;
			[JsonMember]
			public int a;
		};
		[Serializable]
		[JsonOptIn]
		public class Size
		{
			[JsonMember]
			public int x;
			[JsonMember]
			public int y;
			[JsonMember]
			public int z;
		};

		[JsonMember]
		public int version;
		[JsonMember]
		public Vox[] vox;
		[JsonMember]
		public Rgba[] rgba;
		[JsonMember]
		public Size size;
	}


	
}



