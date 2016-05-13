using UnityEngine;
//using UnityEditor;
using GDGeek;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;


namespace GDGeek{
	public class VoxelFormater
	{
		public static ushort[] palette_ = new ushort[] { 32767, 25599, 19455, 13311, 7167, 1023, 32543, 25375, 19231, 13087, 6943, 799, 32351, 25183, 
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


		private struct Point
		{
			public byte x;
			public byte y;
			public byte z;
			public byte i;
		
		}
		public static Color Short2Color(ushort s){

			Color c = new Color ();
			c.a  = 1.0f;
			c.r  = (float)(s & 0x1f)/31.0f;
			c.g  = (float)(s >> 5 & 0x1f)/31.0f;
			c.b  = (float)(s >> 10 & 0x1f)/31.0f;
			return c;
		}
		public static ushort Color2Short(Color c){
			ushort s = 0;
			s = (ushort)(Mathf.RoundToInt (c.r * 31.0f) | Mathf.RoundToInt (c.g * 31.0f)<<5 | Mathf.RoundToInt (c.b * 31.0f)<<10);
			return s;
		}

		public static Color Bytes2Color(VectorInt4 v){

			Color c = new Color ();
			c.r  = ((float)(v.x))/255.0f;
			c.g  = ((float)(v.y))/255.0f;
			c.b  = ((float)(v.z))/255.0f;
			c.a  = ((float)(v.w))/255.0f;
			return c;
		}
		public static VectorInt4 Color2Bytes(Color c){
			VectorInt4 v;
			v.x = Mathf.RoundToInt (c.r * 255.0f);
			v.y = Mathf.RoundToInt (c.g * 255.0f);
			v.z = Mathf.RoundToInt (c.b * 255.0f);
			v.w = Mathf.RoundToInt (c.a * 255.0f);
			return v;
		}


		private static Point CreatePoint(BinaryReader br, bool subsample){
			Point point = new Point ();
			point.x = (byte)(subsample ? br.ReadByte()  : br.ReadByte());
			point.y = (byte)(subsample ? br.ReadByte()  : br.ReadByte());
			point.z = (byte)(subsample ? br.ReadByte()  : br.ReadByte());

			point.i = (br.ReadByte());
		
			return point;
		}

		private static Point[] CreatePoint(List<VoxelData> datas, VectorInt4[] palette){

			Point[] points = new Point[datas.Count];
			for (int i = 0; i < datas.Count; ++i) {
				points[i] = new Point();
				points[i].x = (byte)datas[i].pos.x;
				points[i].y = (byte)datas[i].pos.y;
				points[i].z = (byte)datas[i].pos.z;
				Color color = datas [i].color;
				if (palette == null) {
					ushort s = Color2Short (color);
					for (int x = 0; x < palette_.Length; ++x) {
						if (palette_ [x] == s) {
							points [i].i = (byte)(x + 1);
							break;
						}
					}
				} else {
					VectorInt4 v = Color2Bytes (color);
					for (int x = 0; x < palette.Length; ++x) {
						if (palette [x] == v) {
							points [i].i =  (byte)(x + 1);
							break;
						}
					}
				}
			}
		
			return points;
		}
		private static List<VoxelData> CreateVoxelDatas(Point[] points, VectorInt4[] palette){

			List<VoxelData> datas = new List<VoxelData>();

			for(int i=0; i < points.Length; ++i){
				VoxelData data = new VoxelData();
				data.pos.x = points[i].x;
				data.pos.y = points[i].y;
				data.pos.z = points[i].z;
				data.id = i;

				if(palette == null){


					ushort c = palette_[points[i].i - 1];

					data.color = Short2Color (c);



				}else{
					VectorInt4 v = palette[points[i].i - 1];
					data.color = Bytes2Color (v);;

				}

				datas.Add (data);
			}

	

			return datas;
		
		}


		public static VoxelStruct ReadFromMagicaVoxel(System.IO.BinaryReader br){


			VoxelStruct vs = new VoxelStruct ();
			VectorInt4[] palette = null;
			Point[] points = null;

			string vox = new string(br.ReadChars(4));
			if (vox != "VOX ") {

				return vs;
			}

			int version = br.ReadInt32();
			vs.version = version;
			VectorInt3 box = new VectorInt3 ();
			bool subsample = false;


			while (br.BaseStream.Position < br.BaseStream.Length)
			{
				// each chunk has an ID, size and child chunks
				string name = new string(br.ReadChars(4));
				int size = br.ReadInt32();
				int chunks = br.ReadInt32();
				// Debug.LogError (chunkName);
				// there are only 2 chunks we only care about, and they are SIZE and XYZI
				if(name == "MAIN"){
					vs.main = new VoxelStruct.Main ();
					vs.main.size = size;
					vs.main.name = name;
					vs.main.chunks = chunks;
				}
				if (name == "SIZE")
				{
					
					box.x = br.ReadInt32();
					box.y = br.ReadInt32();
					box.z = br.ReadInt32();


					vs.size = new VoxelStruct.Size ();
					vs.size.size = 12;
					vs.size.name = name;
					vs.size.chunks = chunks;
					vs.size.box = box;

					if (box.x > 32 || box.y > 32) {
						subsample = true;
					}

					br.ReadBytes(size - 4 * 3);
				}
				else if (name == "XYZI")
				{
					
					int count = br.ReadInt32();
					points = new Point[count];
					for (int i = 0; i < points.Length; i++) {
						points [i] = VoxelFormater.CreatePoint (br, subsample);//new Data (stream, subsample);
					}

				}
				else if (name == "RGBA")
				{



					int n = size / 4;
					palette = new VectorInt4[n];
					for (int i = 0; i < n; i++)
					{
						byte r = br.ReadByte();
						byte g = br.ReadByte();
						byte b = br.ReadByte();
						byte a = br.ReadByte();
						palette[i].x = r;
						palette[i].y = g;
						palette[i].z = b;
						palette[i].w = a;
					}

					vs.rgba = new VoxelStruct.Rgba ();
					vs.rgba.size = size;
					vs.rgba.name = name;
					vs.rgba.chunks = chunks;
					vs.rgba.palette = palette;
				}
				else {
					br.ReadBytes(size);   // read any excess bytes
				}
			}
			vs.datas = CreateVoxelDatas(points, palette);
			return vs;

		}
		public static string GetMd5(VoxelStruct vs){
			
			MemoryStream memoryStream = new MemoryStream ();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			WriteToMagicaVoxel (vs, binaryWriter);
			byte[] data = memoryStream.GetBuffer();
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] result = md5.ComputeHash(data);
			string fileMD5 = "";
			foreach (byte b in result)
			{
				fileMD5 += Convert.ToString(b, 16);
			}
			return fileMD5; 
		}
		public static void WriteToMagicaVoxel(VoxelStruct vs, System.IO.BinaryWriter bw){

			bw.Write ("VOX ".ToCharArray());
			bw.Write ((int)vs.version);


			if (vs.main != null) {
				bw.Write (vs.main.name.ToCharArray ());
				bw.Write ((int)vs.main.size);
				bw.Write ((int)vs.main.chunks);
			}

			if (vs.size != null) {
				bw.Write (vs.size.name.ToCharArray ());
				bw.Write ((int)vs.size.size);
				bw.Write ((int)vs.size.chunks);
				bw.Write ((int)vs.size.box.x);
				bw.Write ((int)vs.size.box.y);
				bw.Write ((int)vs.size.box.z);
			}
		

			if (vs.rgba != null && vs.rgba.palette != null) {

				int length = vs.rgba.palette.Length;
				bw.Write (vs.rgba.name.ToCharArray ());
				bw.Write ((int)(length * 4));
				bw.Write ((int)vs.size.chunks);


				for (int i = 0; i < length; i++)
				{
					VectorInt4 c = vs.rgba.palette [i];
					bw.Write ((byte)(c.x));
					bw.Write ((byte)(c.y));
					bw.Write ((byte)(c.z));
					bw.Write ((byte)(c.w));
				}
			}
			Point[] points = CreatePoint (vs.datas, vs.rgba.palette);
			bw.Write ("XYZI".ToCharArray ());

			bw.Write ((int)(points.Length * 4) + 4);
			bw.Write ((int)0);

			bw.Write ((int)points.Length);

			//Point[] points = CreatePoint (vs.datas, vs.rgba.palette);
			for (int i = 0; i < points.Length; ++i) {
				Point p = points[i];
				bw.Write ((byte)(p.x));
				bw.Write ((byte)(p.y));
				bw.Write ((byte)(p.z));
				bw.Write ((byte)(p.i));
			
			}

		}
	}
}

