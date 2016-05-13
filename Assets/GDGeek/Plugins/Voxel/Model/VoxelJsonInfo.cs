using UnityEngine;
using System.Collections;

using Pathfinding.Serialization.JsonFx;
using System;

namespace GDGeek.WebVox{
	
	[Serializable]
	[JsonOptIn]
	public class VoxelJsonInfo : DataInfo {

		[JsonMember]
		public VoxelJson data = null; 
		
		public static string Save(VoxelJsonInfo info){
			string json = JsonDataHandler.write<VoxelJsonInfo>(info);
			return json;
			
		}
		public static VoxelJsonInfo Load(string json){
			VoxelJsonInfo info = JsonDataHandler.reader<VoxelJsonInfo>(json);
			return info;
		}
	}
}
