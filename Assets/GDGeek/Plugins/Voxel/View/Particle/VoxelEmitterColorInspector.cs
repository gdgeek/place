using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class VoxelEmitterColorInspector : VoxelEmitterColor {
		public Color _begin = Color.white;
		
		public Color _end = Color.white;

		public override Pair color (){
			return new Pair (_begin, _end);
		}
	
	}
}