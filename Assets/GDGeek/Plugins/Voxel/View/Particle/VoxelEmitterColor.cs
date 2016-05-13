using UnityEngine;
using System.Collections;

namespace GDGeek{
	public abstract class VoxelEmitterColor : MonoBehaviour {
		public struct Pair{
			public Pair(Color b, Color e){
				begin = b;
				end = e;
			}
			public Color begin;
			public Color end;
		};
		public abstract Pair color ();
	}
}