using UnityEngine;
using System.Collections;
using System.IO;

using Pathfinding.Serialization.JsonFx;

namespace GDGeek{
	
	//[ExecuteInEditMode]
	public class VoxelModelEditor : VoxelModel {
		public VoxelData[] _data = null;
	

		public override VoxelData[] data{
			get{
				return _data;
			}
			set{
				_data = value;
			}
		}
	
	}
}
