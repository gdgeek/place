using UnityEngine;
using System.Collections;
using GDGeek;

namespace GDGeek{
	/*
	[ExecuteInEditMode]
	public class VoxelInstantiation : MonoBehaviour {
		//public Vector3 _fixSize = Vector3.zero;
		public VoxelLoader _loader = null;
		public Material _material;
		public Material _shadowMaterial;
		public VoxelModel _model = null;
		public VoxelMesh _mesh = null;
		public bool _building = false;
		public bool _destroying = false;
		public bool _enableShadow = false;
		public VoxelShadow _shadow = null;
		private void initModel(){
			if(_model == null){
				_model = this.gameObject.GetComponent<VoxelModel>();
			}
			
			if(_model == null){
				this._model = this.gameObject.AddComponent<VoxelModelCache>();
			}
		}
		private void initLoader(){
			
			if(_loader == null){
				_loader  = this.gameObject.GetComponent<VoxelLoader>();
			}
			
			
			if(_loader == null){
				_loader  = this.gameObject.AddComponent<VoxelFileLoader>();
			}
			_loader._model = _model;
		}
		private void initShadow(){
			
			if(_shadow == null){
				_shadow  = this.gameObject.GetComponent<VoxelShadow>();
			}
			
			
			if(_shadow == null){
				_shadow  = this.gameObject.AddComponent<VoxelShadow>();
			}
			_shadow._material = _shadowMaterial;
		}

		void initMesh ()
		{
			if(_mesh == null){
				this._mesh = this.gameObject.GetComponent<VoxelMesh>();
			}
			if(_mesh == null){
				this._mesh = this.gameObject.AddComponent<VoxelMesh>();
				
			}

			
			if(this._mesh._material == null){
				this._mesh._material = this._material;
			}
		}

		// Update is called once per frame
		void Update () {
			if (_model == null) {
				initModel();	
			}


			if (_loader == null) {
				initLoader();	
			}


			if (_building && _loader.resource()) {


				if (_mesh == null) {
					initMesh();	
				}



				_loader.read();

				if(_mesh.empty){
					_mesh.build (_model.data);
					
					VoxelFunctionManager vf = _model.gameObject.GetComponent<VoxelFunctionManager>();
					if(vf != null){
						vf.build(_mesh);
					}
				}	
				_building = false;
				if (this._enableShadow) {
					
					//this._enableShadow = false;	
					initShadow();
					_shadow.build (_mesh.voxels, _mesh._mesh.transform.localPosition);
				}
			}

			if (_destroying) {
				if(!_mesh.empty){
					_mesh.clear ();
					
					
				}	

				if(	_shadow != null &&!_shadow.empty){
					_shadow.clear ();
				}	

				
			
				_destroying = false;
			}

		}
	}*/
}
