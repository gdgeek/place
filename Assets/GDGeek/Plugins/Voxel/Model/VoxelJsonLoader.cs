using UnityEngine;
using System.Collections;


namespace GDGeek{
	[ExecuteInEditMode]
	public class VoxelJsonLoader : MonoBehaviour {


		public VoxelModelEditor _model = null;
		public string _json = null;

		#if UNITY_EDITOR
			
		public bool _refresh = true;
		void Update () {
			if (_refresh && !string.IsNullOrEmpty(_json)) {
				var jData = JsonDataHandler.reader<VoxelJson>(_json);
				_refresh = false;
				if(_model != null){
					_model.data = VoxelReader.FromJsonData (jData);
				}
				VoxelReader.print();
			}
			
		}
		
		#endif
	}
}
