using UnityEngine;

using Pathfinding.Serialization.JsonFx;
using System.Collections;
namespace GDGeek{
	
	public class VoxelModelCache : VoxelModel {
		private VoxelData[] data_ = null;
		public override VoxelData[] data {
			get {

				return data_;
			}
			set{
				data_ = value;
				
				
				
			}
		}
	}
}