using UnityEngine;
using System.Collections;
namespace GDGeek{
	public abstract class VoxelLoader : MonoBehaviour {
	

		public VoxelModel _model = null;
		public abstract void read();
		public abstract bool resource();
	}
}