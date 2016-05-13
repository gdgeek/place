using UnityEngine;
using System.Collections;
namespace GDGeek{
	/*
	public class VoxelChangeColor : MonoBehaviour {

		public VoxelMesh _voxelMesh;
		public VoxelModel[] _model;
		public void Awake(){
			if (_voxelMesh == null) {
				_voxelMesh = this.gameObject.GetComponent<VoxelMesh>();			
			}
		}
		public void change(int index){
			
			Debug.Log (index);
			VoxelModel model = _model [index];
			VoxelData[] datas = model.data;
			Color[] colors = _voxelMesh._mesh.mesh.colors;
			Debug.LogWarning ("datas" + datas);
			for (int i = 0; i < datas.Length; ++i) {
				VoxelHandler handler = _voxelMesh.getVoxel(new VectorInt3(datas[i].pos.x, datas[i].pos.y, datas[i].pos.z));
				if(handler != null){
					foreach(VectorInt4 vertice in handler.vertices){
						
						colors[vertice.x] = datas[i].color;
						colors[vertice.y] = datas[i].color;
						colors[vertice.z] = datas[i].color;
						colors[vertice.w] = datas[i].color;
					}
				}
			}
			_voxelMesh._mesh.mesh.colors = colors;

		}

	}*/
}