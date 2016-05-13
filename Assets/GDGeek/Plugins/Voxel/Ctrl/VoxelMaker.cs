using UnityEngine;
using System.Collections;
namespace GDGeek{
	[ExecuteInEditMode]
	public class VoxelMaker : MonoBehaviour {
		public TextAsset _voxFile = null;
		public bool _building = true;
		public VoxelTextAssetLoader _loader = null;	
		public VoxelModel _model = null;
		public VoxelDirector _director = null;


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
				_loader  = this.gameObject.GetComponent<VoxelTextAssetLoader>();
			}
			
			
			if(_loader == null){
				_loader  = this.gameObject.AddComponent<VoxelTextAssetLoader>();
			}
			_loader._voxFile = this._voxFile;
			_loader._model = _model;
		}

		void initMesh ()
		{
			if(_director == null){
				this._director = this.gameObject.GetComponent<VoxelDirector>();
			}
			if(_director == null){
				this._director = this.gameObject.AddComponent<VoxelDirector>();
				
			}


			#if UNITY_EDITOR
			if(this._director._material == null){
				this._director._material = UnityEditor.AssetDatabase.LoadAssetAtPath<Material>("Assets/GdGeek/Media/Material/VoxelMesh.mat");
			}
			#endif
		}

		// Update is called once per frame
		void Update () {
			if (_building == true && _voxFile != null) {

	
				initModel();
				initLoader();
				initMesh();
/*				if(!_director.empty){
					_director.clear ();
				}	

*/
				_loader.read();

				//if(_director.empty){
//				_director.init();
				_director.build (_model.vs);
				
			//	}	


				_building = false;	
			}
		}
	}

}