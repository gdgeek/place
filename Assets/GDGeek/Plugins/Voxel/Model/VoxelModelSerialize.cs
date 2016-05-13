using UnityEngine;

using Pathfinding.Serialization.JsonFx;
using System.Collections;
namespace GDGeek{

	public class VoxelModelSerialize : VoxelModel {
		private VoxelData[] data_ = null;
		public string _fileName;
	
	//	private int[] aaa = null;
		private string fileName(){
			_fileName = this.gameObject.name + ".vd";
			Transform tr = this.transform;
			while(tr.parent != null){
				tr = tr.parent;
				_fileName = tr.gameObject.name +"." +  _fileName;
				Debug.Log (tr.gameObject.name);
			}
			return _fileName;

		}
		public override VoxelData[] data {
			get {
				//string json = PlayerPrefs.GetString("gdgeek."+this.fileName());
//				Debug.Log(json);
			//	data_ = JsonDataHandler.reader<VoxelData[]>(json);
				return data_;
			}
			set{
				data_ = value;


			//	string json = JsonDataHandler.write<VoxelData[]>(data_);
		//	Debug.Log(json);


			//	PlayerPrefs.SetString("gdgeek."+this.fileName(), json);
			//	PlayerPrefs.Save();
			}
		}
	}
}