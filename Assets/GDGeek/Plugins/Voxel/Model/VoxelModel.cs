using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class VoxelModel : MonoBehaviour {

		public VoxelStruct vs = null;
		public virtual VoxelData[] data {
			get {
					return null;
			}
			set{

			}
		}
	}
}
