using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GDGeek{
	public class VoxelPool_ : MonoBehaviour {
		public delegate void DoAction(VoxelPoolObject obj);
		public int _reserve = -1;
		public event DoAction doEnable;
		public event DoAction doDisable;
		public static VoxelPool_ inctance_ = null;
		public static VoxelPool_ GetInstance(){
			if (inctance_ == null) {
				inctance_ = Component.FindObjectOfType<VoxelPool_> ();
				if (inctance_ == null) {
					GameObject obj = new GameObject ();
					obj.name = "VoxelPool";
					inctance_ = obj.AddComponent<VoxelPool_> ();
				}
			}
			return inctance_;

		}
		public VoxelPoolObject _prototype = null;
		private Stack<VoxelPoolObject> _pool = new Stack<VoxelPoolObject>();
		public void init(){
			if (_prototype == null) {
				GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
				cube.transform.parent = this.transform;
				_prototype = cube.AddComponent<VoxelPoolObject> ();

			}
			if (_reserve == -1) {
				_reserve = 10;
			}

			_prototype.gameObject.SetActive (false);
			VoxelPoolObject[] list = new VoxelPoolObject[_reserve];
			for (int i =0; i <_reserve; ++i) {
				list[i] = this.create();	
				list[i].gameObject.SetActive(true);	
			}
			for (int i =0; i <_reserve; ++i) {
				list[i].gameObject.SetActive(false);		
			}

		}
		public void Awake(){
			init ();
		
		}
		public VoxelPoolObject create(){
			VoxelPoolObject obj = null;
			if (_pool.Count == 0) {
				obj = GameObject.Instantiate (_prototype);
				obj.transform.parent = this.transform;
				obj.doDisable += delegate(VoxelPoolObject po){
					if(doDisable != null){
						doDisable(po);
					}
					this.destory(po);
				};


				obj.doEnable += delegate(VoxelPoolObject po){
					if(doEnable != null){
						doEnable(po);
					}
				};


			} else {
				obj = _pool.Pop ();
			}
			return obj;
		}  
		public void destory(VoxelPoolObject obj){
			_pool.Push (obj);
		}

	}
}