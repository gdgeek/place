using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace GDGeek{
	public class VoxelDirector : MonoBehaviour {
		public Material _material = null;


		public delegate void GeometryResult(VoxelMesh geometry);
		public delegate void MeshDataResult(VoxelGeometry.MeshData data);
		public Task buildData(VoxelStruct vs, MeshDataResult result){
			VoxelProduct product = new VoxelProduct ();
			TaskList tl = new TaskList ();
			VoxelData[] datas = vs.datas.ToArray ();
			tl.push(TaskLog.Logger(Build.Task (new VoxelData2Point (datas), product),"d2p"));
			tl.push(TaskLog.Logger(Build.Task (new VoxelSplitSmall (new VectorInt3(8, 8, 8)), product),"vss"));
			tl.push(TaskLog.Logger(Build.Task (new VoxelMeshBuild (), product),"vmb"));//43%
			tl.push(TaskLog.Logger(Build.Task (new VoxelRemoveSameVertices (), product),"vrv"));
			tl.push(TaskLog.Logger(Build.Task (new VoxelRemoveFace (), product),"vrf"));//47%
			tl.push(TaskLog.Logger(Build.Task (new VoxelMerge (), product),"vm"));//47%
			tl.push(TaskLog.Logger(Build.Task (new VoxelRemoveSameVertices (), product),"vrv"));//47%


			TaskManager.PushBack (tl, delegate {
				result(product.getMeshData());	
			});
			return tl;
		}
		public Task buildTask(string name, VoxelStruct vs, GeometryResult cb){
			

			VoxelGeometry.MeshData data = null;
			TaskPack tp = new TaskPack(delegate(){
				vs.arrange ();
				string md5 = VoxelFormater.GetMd5 (vs);
				data = this.LoadFromFile (GetKey(md5));
				if(data == null){
					return buildData(vs, delegate(VoxelGeometry.MeshData result) {
						data = result;
//						Debug.Log(md5);

						this.SaveToFile (GetKey(md5), data);
					});
				}
				return new Task();

			});


			TaskManager.PushBack (tp, delegate {
				if(this.gameObject.GetComponent<VoxelMesh>() == null){
					this.gameObject.AddComponent<VoxelMesh>();
				}
				VoxelMesh mesh = VoxelGeometry.Draw (name, data, this.gameObject, this._material);
				mesh.vs = vs;
				cb(mesh);
			});
			return tp;
		}

		public static VoxelGeometry.MeshData LoadFromFile(string key){
			
			VoxelGeometry.MeshData data = null;
			if (GK7Zip.FileHas (key)) {
				var json = GK7Zip.GetFromFile (key);
				data = JsonUtility.FromJson<VoxelGeometry.MeshData> (json);
			}

			return data;
		}
		public static string GetKey(string md5){
			return "vs7z_" + md5;;
		}
		public static void SaveToFile(string key, VoxelGeometry.MeshData data){
			
		
			GK7Zip.SetToFile (key, JsonUtility.ToJson(data));
			/*var json = JsonUtility.ToJson(data);
			var zip = GK7Zip.Compression (json);
			PlayerPrefs.SetString (key,zip);
			PlayerPrefs.Save ();*/


		}
		public  VoxelMesh draw(string name, VoxelStruct vs, VoxelGeometry.MeshData data ,GameObject obj = null){

			if (obj == null) {
				obj = this.gameObject;
			}
			VoxelMesh mesh = VoxelGeometry.Draw (name, data, obj, this._material);
			mesh.vs = vs;
			return mesh;
		
		}
		public static VoxelGeometry.MeshData CreateMeshData(VoxelStruct vs){
			VoxelProduct product = new VoxelProduct();
			VoxelData[] datas = vs.datas.ToArray ();
			Build.Run (new VoxelData2Point (datas), product);
			Build.Run (new VoxelSplitSmall (new VectorInt3(8, 8, 8)), product);
			Build.Run (new VoxelMeshBuild (), product);
			Build.Run (new VoxelRemoveSameVertices (), product);
			Build.Run (new VoxelRemoveFace (), product);
			Build.Run (new VoxelMerge(), product);
			Build.Run (new VoxelRemoveSameVertices (), product);
			var data = product.getMeshData ();
			return data;
		}
		public static VoxelGeometry.MeshData BuildMeshData(VoxelStruct vs){
			
			vs.arrange ();
			string md5 = VoxelFormater.GetMd5 (vs);
			VoxelGeometry.MeshData data = LoadFromFile (GetKey(md5));
			if(data == null){
				data = CreateMeshData (vs);
				SaveToFile (GetKey(md5), data);
			}
			return data;
		}
		public VoxelMesh build (VoxelStruct vs, GameObject obj = null)
		{

			//VoxelData[] datas = vs.datas.ToArray ();
			if (obj == null) {
				obj = this.gameObject;
			}
			VoxelGeometry.MeshData data = BuildMeshData(vs);
			VoxelMesh mesh = VoxelGeometry.Draw ("Mesh", data, obj, this._material);
			mesh.vs = vs;
			return mesh;

		}

	}

}