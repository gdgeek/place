using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class VoxelEmitterShapeBox : VoxelEmitterShape {

		public BoxCollider _box = null;

		public override Vector3 getPosition(Transform transform) {
			return (_box.bounds.center + new Vector3(Random.Range(-_box.bounds.size.x/2, _box.bounds.size.x/2), 
			                                         Random.Range(-_box.bounds.size.y/2, _box.bounds.size.y/2),
			                                         Random.Range(-_box.bounds.size.z/2, _box.bounds.size.z/2)));
			
		}



	}
}