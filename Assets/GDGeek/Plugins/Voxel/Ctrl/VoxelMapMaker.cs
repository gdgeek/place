using UnityEngine;
using System.Collections;
namespace GDGeek{
	[ExecuteInEditMode]
	public class VoxelMapMaker : MonoBehaviour {
		

		public VoxelStruct building(){
			
			VoxelMaker[] makers = this.gameObject.GetComponentsInChildren<VoxelMaker> ();
			JoinVoxel join = new JoinVoxel ();
			for (int i = 0; i < makers.Length; ++i) {
				Vector3 offset = makers [i].gameObject.transform.localPosition;
				offset.x = Mathf.Round (offset.x);
				offset.y = Mathf.Round (offset.y);
				offset.z = Mathf.Round (offset.z);
				makers [i].gameObject.transform.localPosition = offset;
				makers [i]._loader.read ();
				Debug.Log (offset);
				join.addVoxel (makers [i]._model.vs, new VectorInt3((int)offset.x,  (int)offset.z, (int)offset.y));
			}
			VoxelStruct vs = join.doIt ();
			return vs;
		}
	}

}