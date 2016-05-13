using UnityEngine;
using System.Collections;
using System.IO;

namespace GDGeek{
	public class VoxelReader{
		// this is the default palette of voxel colors (the RGBA chunk is only included if the palette is differe)
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
		public static void print(){
			string ot = "";
			foreach (ushort p in palette) {
				//i);		
				
				string co = "0x" + string.Format("{0,2:X2}", (int)((float)(p & 0x1f)/31.0f * 255.0f))
					+ string.Format("{0,2:X2}", (int)((float)(p >> 5 & 0x1f)/31.0f * 255.0f))
						+string.Format("{0,2:X2}", (int)((float)(p >> 10 & 0x1f)/31.0f * 255.0f)) ;
				ot += co + ",";
				//(c >> 10 & 0x1f)/31.0f;
				//datas[i].color.g = (float)(c >> 5 & 0x1f)/31.0f;
				//datas[i].color.b = (float)(c & 0x1f)/31.0f;
			}
			Debug.Log (ot);
		}
		private struct Data
		{
			public byte x;
			public byte y;
			public byte z;
			public byte color;
			public Data(BinaryReader stream, bool subsample)
			{

				x = (byte)(subsample ? stream.ReadByte()  : stream.ReadByte());
				y = (byte)(subsample ? stream.ReadByte()  : stream.ReadByte());
				z = (byte)(subsample ? stream.ReadByte()  : stream.ReadByte());

				color = stream.ReadByte();
			}
		}
		
		/*	public static Color ToColor(){

		}*/
		/// <summary>
		/// Load a MagicaVoxel .vox format file into the custom ushort[] structure that we use for voxel chunks.
		/// </summary>
		/// <param name="stream">An open BinaryReader stream that is the .vox file.</param>
		/// <param name="overrideColors">Optional color lookup table for converting RGB values into my internal engine color format.</param>
		/// <returns>The voxel chunk data for the MagicaVoxel .vox file.</returns>
		public static VoxelData[] FromMagica(BinaryReader stream)
		{
			// check out http://voxel.codeplex.com/wikipage?title=VOX%20Format&referringTitle=Home for the file format used below
			// we're going to return a voxel chunk worth of data
			//ushort[] data = new ushort[32 * 128 * 32];
			Vector4[] colors = null;
			Data[] voxelData = null;

			string magic = new string(stream.ReadChars(4));
			int version = stream.ReadInt32();

			Debug.Log ("version:" + version);
			// a MagicaVoxel .vox file starts with a 'magic' 4 character 'VOX ' identifier
			if (magic == "VOX ")
			{
				int sizex = 0, sizey = 0, sizez = 0;
				bool subsample = false;

				while (stream.BaseStream.Position < stream.BaseStream.Length)
				{
					// each chunk has an ID, size and child chunks
					char[] chunkId = stream.ReadChars(4);
					int chunkSize = stream.ReadInt32();
					int childChunks = stream.ReadInt32();
					string chunkName = new string(chunkId);
					
					// Debug.LogError (chunkName);
					// there are only 2 chunks we only care about, and they are SIZE and XYZI
					if (chunkName == "SIZE")
					{
						sizex = stream.ReadInt32();
						sizey = stream.ReadInt32();
						sizez = stream.ReadInt32();
						
						if (sizex > 32 || sizey > 32) subsample = true;
						
						stream.ReadBytes(chunkSize - 4 * 3);
					}
					else if (chunkName == "XYZI")
					{
						// XYZI contains n voxels
						int numVoxels = stream.ReadInt32();
						//int div = (subsample ? 2 : 1);
						// each voxel has x, y, z and color index values
						voxelData = new Data[numVoxels];
						for (int i = 0; i < voxelData.Length; i++) {
							voxelData [i] = new Data (stream, subsample);
						}
					}
					else if (chunkName == "RGBA")
					{
						int n = chunkSize / 4;
						colors = new Vector4[n];
						for (int i = 0; i < n; i++)
						{
							byte r = stream.ReadByte();
							byte g = stream.ReadByte();
							byte b = stream.ReadByte();
							byte a = stream.ReadByte();
							colors[i].x = r;
							colors[i].y = g;
							colors[i].z = b;
							colors[i].w = a;
						}
					}
					else stream.ReadBytes(chunkSize);   // read any excess bytes
				}

				

				VoxelData[] datas = new VoxelData[voxelData.Length];
				
				for(int i=0; i < voxelData.Length; ++i){
					datas[i] = new VoxelData();
					datas[i].pos.x = voxelData[i].x;
					datas[i].pos.y = voxelData[i].y;
					datas[i].pos.z = voxelData[i].z;
					datas[i].id = i;
					//ushort c =  (colors == null ? voxColors[voxelData[i].color - 1] : colors[voxelData[i].color - 1]);
					if(colors == null){

						
						ushort c = palette[voxelData[i].color - 1];

						datas[i].color.a = 1.0f;
						datas[i].color.r = (float)(c & 0x1f)/31.0f;
						datas[i].color.g = (float)(c >> 5 & 0x1f)/31.0f;
						datas[i].color.b = (float)(c >> 10 & 0x1f)/31.0f;



					}else{
						Vector4 c = colors[voxelData[i].color - 1];
						datas[i].color.r = (float)(c.x)/255.0f;
						datas[i].color.g = (float)(c.y)/255.0f;
						datas[i].color.b = (float)(c.z)/255.0f;
						datas[i].color.a = (float)(c.w)/255.0f;

						/* */
					}
						
					
					//	Debug.Log( "r:" + (c >> 10 & 0x1f)+ ",g:"+ (float)(c >> 5 & 0x1f) + ",b:"+ (float)(c & 0x1f));
					//datas[i].cindex = voxelData[i].color;
				}
				
				return datas;
				
			}
			
			return null;
		}


		public static VoxelData[] FromJsonData(VoxelJson jData)
		{
			
			
			VoxelData[] datas = new VoxelData[jData.vox.Length];
			for(int i = 0; i<jData.vox.Length; ++i){
				VoxelData data = new VoxelData();
				data.pos.x = jData.vox[i].x;
				data.pos.y = jData.vox[i].y;
				data.pos.z = jData.vox[i].z;
				data.id = i;
				if(jData.rgba != null && jData.vox[i].c-1 < jData.rgba.Length){
					
					data.color.r = (float)(jData.rgba[jData.vox[i].c-1].r)/255.0f;
					data.color.g = (float)(jData.rgba[jData.vox[i].c-1].g)/255.0f;
					data.color.b = (float)(jData.rgba[jData.vox[i].c-1].b)/255.0f;
					data.color.a = (float)(jData.rgba[jData.vox[i].c-1].a)/255.0f;
				}else{
					
					
					ushort c = palette[jData.vox[i].c - 1];
					data.color.a = 1.0f;
					data.color.r = (float)(c & 0x1f)/31.0f;
					data.color.g = (float)(c >> 5 & 0x1f)/31.0f;
					data.color.b = (float)(c >> 10 & 0x1f)/31.0f;


				}
				datas[i] = data;
				
			}
			return datas;
			
		}


	}
}