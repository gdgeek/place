using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


namespace GDGeek{

	[ExecuteInEditMode]
	public class VoxelASCII : MonoBehaviour {
		public bool _refersh = false;
		public VoxelFileLoader _prototype = null;
		[Serializable]
		public class ASC{
			public char ch;
			public string file;
			public VoxelFileLoader loading = null;
		}
		public List<ASC> _ascii = null;
		public GameObject _offset = null;
		// Use this for initialization
		private void refresh(char ch){

//			Debug.Log (ch);
			ASC asc = new ASC();
			asc.ch = ch;
			asc.file = "vox/number/asc" + ((int)(ch)).ToString() + ".vox";
			asc.loading = (VoxelFileLoader)GameObject.Instantiate (_prototype);
			asc.loading.gameObject.transform.SetParent (_offset.transform);
			asc.loading.gameObject.name = "asc_" + ((int)(ch)).ToString();
			asc.loading._vodFile = asc.file;
			//asc.loading._refresh = true;
			//asc.loading.gameObject.SetActive (true);
			asc.loading.read ();
			_ascii.Add(asc);
		}
		void Start () {

		}
		
		// Update is called once per frame
		void Update () {
			if (_refersh) {
				_refersh = false;
				_ascii.Clear();
				for (int i = 32; i <= 126; i++) {
					refresh ((char)(i));			
				}
			}
		}
	}
}
