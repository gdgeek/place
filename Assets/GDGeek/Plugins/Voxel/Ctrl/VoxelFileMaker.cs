using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace GDGeek
{
	public class VoxelFileMaker: MonoBehaviour 
	{
		private Dictionary<VectorInt3, VoxelData> dictionary_ = new Dictionary<VectorInt3, VoxelData>();
		private Dictionary<uint, byte> palette_ = new Dictionary<uint, byte>();

		public void addFile(VoxelModel model, VectorInt3 offset){

			for (int i = 0; i < model.data.Length; ++i) {
				VoxelData data = model.data [i];
				VectorInt3 p = new VectorInt3 (data.pos.x, data.pos.y, data.pos.z) + offset;
				if (!dictionary_.ContainsKey (p)) {
					dictionary_ [p] = data;
				}
			}

			Debug.Log (dictionary_.Count);
		}

		public int getColorShort(Color color){
			byte r = (byte)(Mathf.RoundToInt(color.r * 255.0f));
			byte g = (byte)(Mathf.RoundToInt(color.g * 255.0f));
			byte b = (byte)(Mathf.RoundToInt(color.b * 255.0f));
			byte a = (byte)(Mathf.RoundToInt(color.a * 255.0f));
			int c = r<<24 & g<<16 & b<<8 & a;
			return c;

		}
		public List<uint> getPalette(){
			return null;
		}
		public void save(){
			

			string file = "fly.vox";
			FileStream sw = new FileStream (file, FileMode.OpenOrCreate, FileAccess.Write);
			System.IO.BinaryWriter stream = new System.IO.BinaryWriter (sw); 
			char[] VOX =  "VOX ".ToCharArray(); 
			char[] SIZE = "SIZE".ToCharArray ();
			char[] RGBA = "RGBA".ToCharArray ();
			char[] XYZI = "XYZI".ToCharArray ();
		 	uint version = 150;
			stream.Write (VOX);
			stream.Write (version);



			int count = dictionary_.Count;
			int size = count * 4;
			int chunks = 1;

			stream.Write (XYZI);
			stream.Write (size);
			stream.Write (chunks);
			stream.Write (count);

			foreach (KeyValuePair<VectorInt3, VoxelData> kv in this.dictionary_) {
				byte x = (byte)(kv.Value.pos.x);
				byte y = (byte)(kv.Value.pos.y);
				byte z = (byte)(kv.Value.pos.z);
				//btye c = 
				stream.Write (x);
				stream.Write (y);
				stream.Write (z);
			}

			/*stream.Write (150);*/

		}
	}


}