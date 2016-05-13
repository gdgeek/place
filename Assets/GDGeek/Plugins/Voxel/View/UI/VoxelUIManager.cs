using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GDGeek{
	
#if UNITY_EDITOR
	[ExecuteInEditMode]
#endif
	public class VoxelUIManager : MonoBehaviour {
		public Camera _uiCamera = null;
		public Matrix4x4 _matrix;
		public VoxelUIObject[] _objects = null;
		public Vector3[] _position = null;
		public bool _read = false;
		public bool _write = false;
		// Use this for initialization
		void Start () {
		}
		// Update is called once per frame

#if UNITY_EDITOR
		void Update () {
			if(EditorApplication.isPlaying )
				return; 
			if (_matrix != _uiCamera.projectionMatrix) {
								_matrix = _uiCamera.projectionMatrix;
								for (int i =0; i<_position.Length; ++i) {
										_objects [i].transform.position = _uiCamera.ScreenToWorldPoint (_position [i]);
								}
						} else {
								_position = new Vector3[_objects.Length];
								for (int i = 0; i<_objects.Length; ++i) {
										_position [i] = _uiCamera.WorldToScreenPoint (_objects [i].transform.position);
								}
						}
		}
#endif
	}
}