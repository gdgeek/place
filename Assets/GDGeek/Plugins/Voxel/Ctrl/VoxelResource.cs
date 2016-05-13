using UnityEngine;
using System.Collections;
using GDGeek;
using System.Collections.Generic;

public class VoxelResource : MonoBehaviour {

	public List<VoxelMesh> _list = new List<VoxelMesh>();

	public void add(VoxelMesh mesh){
		_list.Add (mesh);
//		Debug.Log("voxel resource name is ..." + mesh);
	}
	public void show(int n){
		for (int i = 0; i < _list.Count; ++i) {
			if (_list [i].filter != null) {
				_list [i].filter.gameObject.SetActive (n == i);
			}
		}
	}

}
