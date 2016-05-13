using UnityEngine;
using System.Collections;
using System.IO;


namespace GDGeek{
	[ExecuteInEditMode]
	public class VoxelFile : MonoBehaviour {
		public TextAsset _voxFile = null;
		public int _hashcode = 0;
		public string _md5 = null;
		private VoxelStruct vs_ = null;
		private VoxelGeometry.MeshData data_ = null;


		public VoxelStruct voxel{
			get{ 
				if (vs_ == null) {
					vs_ = createStruct (_voxFile);
				}
				return vs_;
			}
		}
		public VoxelGeometry.MeshData createMeshData(string md5){
			string key = VoxelDirector.GetKey(md5);
			VoxelGeometry.MeshData data = null;
			if (!GK7Zip.FileHas (key)) {
				data = VoxelDirector.CreateMeshData (this.voxel);
				VoxelDirector.SaveToFile (key, data);
			} else {
				data = VoxelDirector.LoadFromFile (key);
			}
			return data;


		}
		public VoxelGeometry.MeshData mesh{
			get{ 
				if (data_ == null) {
					if (_md5 == null) {
						vs_ = createStruct(this._voxFile);
						vs_.arrange ();
						this._md5 = VoxelFormater.GetMd5 (vs_);
						this.data_ = createMeshData (this._md5);
					}else{
						this.data_ = createMeshData (_md5);
					}
				}
				return data_;
			}

		}
		public  VoxelStruct createStruct(TextAsset voxFile){
			Stream sw = new MemoryStream(voxFile.bytes);
			System.IO.BinaryReader br = new System.IO.BinaryReader (sw); 
			var vs =  VoxelFormater.ReadFromMagicaVoxel (br);
			return vs;
		}
#if UNITY_EDITOR
		private void readVoxFile(TextAsset voxFile){

			vs_ = createStruct(voxFile);
			vs_.arrange ();
			this._md5 = VoxelFormater.GetMd5 (vs_);
			this.data_ = createMeshData (this._md5);
			this.gameObject.name = voxFile.name;
		}

		// Update is called once per frame
		void Update () {
			if (!Application.isPlaying) {
				if (_voxFile != null && _voxFile.GetHashCode () != _hashcode) {
//					Debug.Log (_voxFile.GetHashCode ());
					_hashcode = _voxFile.GetHashCode ();
					this.readVoxFile (_voxFile);
				}
			}
		}
#endif
	}
}