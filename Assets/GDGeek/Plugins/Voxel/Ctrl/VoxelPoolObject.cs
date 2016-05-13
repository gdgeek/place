using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class VoxelPoolObject : MonoBehaviour {
		public delegate void DoAction(VoxelPoolObject obj);
		
		public event DoAction doEnable;
		public event DoAction doDisable;


		public void OnDisable(){
			if (doDisable != null) {
				doDisable(this);			
			}
		}

		public void OnEnable(){


			if (doEnable != null) {
				doEnable(this);			
			}
		}
	}
}