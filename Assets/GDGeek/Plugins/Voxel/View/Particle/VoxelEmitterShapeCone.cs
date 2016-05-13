using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class VoxelEmitterShapeCone : VoxelEmitterShape {

		public override Vector3 getPosition(Transform transform) {
			return transform.position;
			
		}

		

	}
}