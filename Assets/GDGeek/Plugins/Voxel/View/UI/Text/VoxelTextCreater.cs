using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace GDGeek{
	/*
	[ExecuteInEditMode]
	public class VoxelTextCreater : MonoBehaviour {

		public Vector3 _fontSize = Vector3.zero;
		public bool _refersh = false;
		public VoxelASCII _ascii = null;
		public VoxelCharPool _prototype = null;
		public GameObject _poolSet = null;
		[Serializable]
		public class Char{
			public char _char;
			public VoxelCharPool _pool;
		}

		public List<Char> _chars;
		private Dictionary<char, Char> map_ = new Dictionary<char, Char> ();
		private GameObject getCharObject(char ch){

			if (map_.ContainsKey (ch)) {
				VoxelPoolObject obj = map_[ch]._pool._pool.create();	
				return obj.gameObject;
			}
			for (int i = 0; i< _chars.Count; ++i) {
				if(_chars[i]._char == ch){
					VoxelPoolObject obj = _chars[i]._pool._pool.create();
					return obj.gameObject;
				}
			}		
			return null;
		}
		public bool has (char ch)
		{
			if (map_.ContainsKey (ch)) {
				return true;			
			}
			for (int i = 0; i< _chars.Count; ++i) {
				if(_chars[i]._char == ch){
					map_[ch] = _chars[i];
					return true;
				}
			}	
			return false;
		}

		public VoxelChar create(char c){
			GameObject obj = getCharObject (c);
			VoxelChar vc = obj.GetComponent<VoxelChar>();
			vc.ch = c;
			return vc;
		}

		void refresh (VoxelASCII.ASC asc)
		{
			Char ch = new Char ();
			ch._char = asc.ch;
			ch._pool = (VoxelCharPool)GameObject.Instantiate (_prototype);
			ch._pool.gameObject.transform.SetParent (_poolSet.transform);
			ch._pool.name =  "asc_" + ((int)(asc.ch)).ToString();
			ch._pool._instantiation._model = asc.loading.gameObject.GetComponent<VoxelModel>();
			ch._pool._instantiation._building = true;
//			ch._pool._instantiation._mesh._offsetEnable = false;
			ch._pool._instantiation._mesh.transform.position = (_fontSize-Vector3.one) / -2;

			//ch._pool._instantiation._fixSize = _fontSize;
			ch._pool.gameObject.SetActive (true);
			_chars.Add (ch);
		}

		void Update () {
			if (_refersh) {
				_refersh = false;
				List<VoxelASCII.ASC> asc = _ascii._ascii;
				_chars.Clear();
				for(int i =0; i< asc.Count; ++i){
					refresh(asc[i]);
					Debug.Log(asc[i].ch);

				}
			}
		}
	}*/
}