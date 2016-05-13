using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GDGeek{

	public class VoxelMesh : MonoBehaviour{
		public BoxCollider collider = null;
		//[SerializeField]
		public VoxelStruct vs = null;
		public MeshFilter filter = null;
	}

}
