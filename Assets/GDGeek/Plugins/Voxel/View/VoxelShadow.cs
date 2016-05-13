using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GDGeek{
	/*
	public class VoxelShadow : MonoBehaviour {

		
		public Material _material = null;
		public GameObject _offset = null;
		
		public MeshFilter _mesh;
		
		private Dictionary<VectorInt3, VoxelShadowHandler> real_ = new Dictionary<VectorInt3, VoxelShadowHandler>();

		
		public void setLayer (int layer)
		{
			this.gameObject.layer = layer;
			this._mesh.gameObject.layer = layer;

			
		}

	
		
		public bool empty{
			get{
				return _mesh == null;
			}
		}
		public void build (Dictionary<VectorInt3, VoxelHandler> voxs, Vector3 position)
		{
			
			if (empty) {

				this.shadowBuild (voxs);
				this.buildMesh (position);
			}
			
		}
		
		
		
		public void load (VoxelData[] to)
		{
			throw new System.NotImplementedException ();
		}
		
		public void clear ()
		{
			
			if (!empty) {

				this.clearMesh ();
			}
		}
		
		

		public bool colliderIt(GDGeek.VoxelMesh voxelMesh){
			return true;
		}
		
			

		
		public void showMesh (Vector3 position)
		{
			_mesh.gameObject.SetActive (true);

			refersh (position);
			
		}
		private void refersh(Vector3 position){
		
			this._mesh.transform.localPosition = position;
		}
	
		
		
		public void setLightColor(Color color){
			Renderer renderer = _mesh.GetComponent<Renderer> ();
			renderer.material.SetColor("_LightColor", color);
		}
		public void setMainColor(Color color){
			Renderer renderer = _mesh.GetComponent<Renderer> ();
			renderer.material.SetColor("_MainColor", color);
		}
		public void clearMesh(){
			
			
			if (_mesh) {
				GameObject.DestroyImmediate (_mesh.gameObject);
			}
			_mesh = null;
			
		}
		public void buildMesh (Vector3 position)
		{	
			VoxelDrawMesh draw = new VoxelDrawMesh ();
			Debug.Log (real_.Count + "count");
			foreach (KeyValuePair<VectorInt3, VoxelShadowHandler> kv in this.real_) {
				VectorInt4 v = draw.addRect (Vector3.up, kv.Key + new VectorInt3(0, 0, -1) , VoxelShadowHandler.GetUV(kv.Value.real), VoxelShadowHandler.GetUV(kv.Value.ghost), Color.black);
			}

			_mesh = draw.crateMeshFilter ("Shadow", _material);
			if (_offset != null) {
				_mesh.gameObject.transform.SetParent (this._offset.transform);
			} else {
				_mesh.gameObject.transform.SetParent (this.transform);		
			}
			
			_mesh.gameObject.transform.localPosition = Vector3.zero;
			_mesh.gameObject.transform.localScale = Vector3.one;
			_mesh.gameObject.transform.localRotation = Quaternion.Euler (Vector3.zero);
			
			_mesh.gameObject.SetActive (true);
			Renderer renderer = _mesh.GetComponent<Renderer> ();
			renderer.material = this._material;
			showMesh (position);
		}
	

		private void toShadow(VectorInt3 position, VoxelHandler handler, bool spot = false){
			
			VoxelShadowHandler vsh = null;

			bool glost = (handler.position.z != 0);
			position.z = 0;

			if (real_.ContainsKey (position)) {
				vsh = real_ [position];
			} else {
				vsh = new VoxelShadowHandler ();
				vsh._position = position;
				vsh._realSpot = false;
				vsh._ghostSpot = false;
				real_ [position] = vsh;	
			}

			if (!glost) 
			{
				vsh._realSpot = vsh._realSpot || spot;	
				vsh._ghostSpot = vsh._ghostSpot || spot;
			} else 
			{
				vsh._ghostSpot = vsh._ghostSpot || spot;
			}
		

		}

		private void shadowTest(VectorInt3 offset, VoxelShadowHandler handler, byte index){


			var voxs = this.real_;
			if (handler._realSpot) {
				handler.realAdd (index);
			}
		
			VectorInt3 key = offset;
			if (voxs.ContainsKey (key)) {
				VoxelShadowHandler o = voxs[key];
				if(o._realSpot){
					handler.realAdd (index);	
				}

			} 

			if (handler._ghostSpot) {
					if (!voxs.ContainsKey (key)) {
							handler.ghostAdd (index);	
					} else {
							VoxelShadowHandler o = voxs [key];
							if (!o._ghostSpot) {
									handler.ghostAdd (index);	
							}

					}

			} else {
				handler.ghostAdd (index);	
			}
		}

		public void shadowBuild(Dictionary<VectorInt3, VoxelHandler> voxs){

			this.real_.Clear ();
			foreach (KeyValuePair<VectorInt3, VoxelHandler> kv in voxs) {
				VectorInt3 pos = kv.Value.position;
				toShadow(pos, kv.Value, true);
				if(kv.Value.position.z == 0){
					toShadow(pos + new VectorInt3(0, -1 , 0), kv.Value);
					toShadow(pos + new VectorInt3(0, 1 , 0), kv.Value);
					toShadow(pos + new VectorInt3(1, 0 , 0), kv.Value);
					toShadow(pos + new VectorInt3(1, -1 , 0), kv.Value);
					toShadow(pos + new VectorInt3(1, 1 , 0), kv.Value);
					toShadow(pos + new VectorInt3(-1, 0 , 0), kv.Value);
					toShadow(pos + new VectorInt3(-1, -1 , 0), kv.Value);
					toShadow(pos + new VectorInt3(-1, 1 , 0), kv.Value);
				}
			}

			foreach (KeyValuePair<VectorInt3, VoxelShadowHandler> kv in this.real_) {
				VectorInt3 pos = kv.Key;

				kv.Value.shadowBegin();
				
				shadowTest(pos + new VectorInt3(-1, 1 , 0), kv.Value, 0);
				shadowTest(pos + new VectorInt3(0, 1 , 0), kv.Value, 1);
				shadowTest(pos + new VectorInt3(1, 1 , 0), kv.Value, 2);
				shadowTest(pos + new VectorInt3(-1, 0 , 0), kv.Value, 3);
				shadowTest(pos + new VectorInt3(1, 0 , 0), kv.Value, 4);
				shadowTest(pos + new VectorInt3(-1, -1 , 0), kv.Value,5);
				shadowTest(pos + new VectorInt3(0, -1 , 0), kv.Value, 6);
				shadowTest(pos + new VectorInt3(1, -1 , 0), kv.Value, 7);
		
				kv.Value.shadowEnd();
			}
			
		}
		
		
	}*/
}
