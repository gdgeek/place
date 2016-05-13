using UnityEngine;
//using UnityEditor;
using GDGeek;
using System.IO;
using System.Collections.Generic;
using System;


namespace GDGeek{
	
	public class VoxelStruct
	{
		public class Main
		{
			public int size;
			public string name;
			public int chunks;

		}

		public class Size
		{
			public int size;
			public string name;
			public int chunks;
			public VectorInt3 box;

		}
		public class Rgba
		{
			public int size;
			public string name;
			public int chunks;
			public VectorInt4[] palette;

		}




		private static ushort[] palette = new ushort[] { 32767, 25599, 19455, 13311, 7167, 1023, 32543, 25375, 19231, 13087, 6943, 799, 32351, 25183, 
			19039, 12895, 6751, 607, 32159, 24991, 18847, 12703, 6559, 415, 31967, 24799, 18655, 12511, 6367, 223, 31775, 24607, 18463, 12319, 6175, 31, 
			32760, 25592, 19448, 13304, 7160, 1016, 32536, 25368, 19224, 13080, 6936, 792, 32344, 25176, 19032, 12888, 6744, 600, 32152, 24984, 18840, 
			12696, 6552, 408, 31960, 24792, 18648, 12504, 6360, 216, 31768, 24600, 18456, 12312, 6168, 24, 32754, 25586, 19442, 13298, 7154, 1010, 32530, 
			25362, 19218, 13074, 6930, 786, 32338, 25170, 19026, 12882, 6738, 594, 32146, 24978, 18834, 12690, 6546, 402, 31954, 24786, 18642, 12498, 6354, 
			210, 31762, 24594, 18450, 12306, 6162, 18, 32748, 25580, 19436, 13292, 7148, 1004, 32524, 25356, 19212, 13068, 6924, 780, 32332, 25164, 19020, 
			12876, 6732, 588, 32140, 24972, 18828, 12684, 6540, 396, 31948, 24780, 18636, 12492, 6348, 204, 31756, 24588, 18444, 12300, 6156, 12, 32742, 
			25574, 19430, 13286, 7142, 998, 32518, 25350, 19206, 13062, 6918, 774, 32326, 25158, 19014, 12870, 6726, 582, 32134, 24966, 18822, 12678, 6534, 
			390, 31942, 24774, 18630, 12486, 6342, 198, 31750, 24582, 18438, 12294, 6150, 6, 32736, 25568, 19424, 13280, 7136, 992, 32512, 25344, 19200, 
			13056, 6912, 768, 32320, 25152, 19008, 12864, 6720, 576, 32128, 24960, 18816, 12672, 6528, 384, 31936, 24768, 18624, 12480, 6336, 192, 31744, 
			24576, 18432, 12288, 6144, 28, 26, 22, 20, 16, 14, 10, 8, 4, 2, 896, 832, 704, 640, 512, 448, 320, 256, 128, 64, 28672, 26624, 22528, 20480, 
			16384, 14336, 10240, 8192, 4096, 2048, 29596, 27482, 23254, 21140, 16912, 14798, 10570, 8456, 4228, 2114, 1  };


		public List<VoxelData> datas = new List<VoxelData>();

		public int version = 0;
		public Main main = null;
		public Size size = null;
		public Rgba rgba = null;
		public string md5 = null;
		public Task arrangeTask(bool normal = false){
			return new Task ();
		
		}
		public void arrange(bool normal = false){


			HashSet<Color> palette = new HashSet<Color>();

			VectorInt3 min = new VectorInt3(9999, 9999, 9999);
			VectorInt3 max = new VectorInt3(-9999, -9999,-9999);

			for (int i = 0; i < this.datas.Count; ++i) {
				palette.Add (this.datas[i].color);

				VectorInt3 pos = this.datas [i].pos;

				min.x = Mathf.Min (pos.x, min.x);
				min.y = Mathf.Min (pos.y, min.y);
				min.z = Mathf.Min (pos.z, min.z);
				max.x = Mathf.Max (pos.x, max.x);
				max.y = Mathf.Max (pos.y, max.y);
				max.z = Mathf.Max (pos.z, max.z);

			}

			if (normal) {
				max = max - min;
				for (int i = 0; i < this.datas.Count; ++i) {
					palette.Add (this.datas[i].color);

					VectorInt3 pos = this.datas [i].pos;
					this.datas [i].pos = pos - min;

				}
				min = new VectorInt3 (0, 0, 0);
			}

			this.main = new VoxelStruct.Main ();
			this.main.name = "MAIN";
			this.main.size = 0;


			this.size = new VoxelStruct.Size ();
			this.size.name = "SIZE";
			this.size.size = 12;
			this.size.chunks = 0;

			this.size.box = new VectorInt3 ();


			this.size.box.x = max.x - min.x +1;
			this.size.box.y = max.y - min.y +1;
			this.size.box.z = max.z - min.z +1;


			this.rgba = new VoxelStruct.Rgba ();

			int size = Mathf.Max (palette.Count, 256);
			this.rgba.palette = new VectorInt4[size];
			int n = 0;
			foreach (Color c in palette)
			{
				this.rgba.palette [n] = VoxelFormater.Color2Bytes (c);
				++n;
			}
		



			this.rgba.size = this.rgba.palette.Length * 4;
			this.rgba.name = "RGBA";
			this.rgba.chunks = 0;

			this.version = 150;

			this.main.chunks = 52 + this.rgba.palette.Length *4 + this.datas.Count *4;
		}
	}

}
