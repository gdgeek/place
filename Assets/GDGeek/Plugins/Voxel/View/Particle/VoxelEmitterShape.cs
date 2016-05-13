using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class VoxelEmitterShape : MonoBehaviour {
		public virtual Vector3 getPosition(Transform transform) {
			return transform.position;
		}
	}

}