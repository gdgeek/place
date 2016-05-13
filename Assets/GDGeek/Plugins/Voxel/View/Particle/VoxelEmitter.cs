using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GDGeek{

	public class VoxelEmitter : MonoBehaviour {
		[System.Serializable]
		public class Parameter{
			public Vector2 _interval = Vector2.one;
			public Vector2 _speed = Vector2.one;
			public Vector2 _time = Vector2.one;
			public Vector3 _beginDirector = Vector3.one;
			public Vector3 _endDirector = Vector3.zero;
			public Vector3 _beginScale = Vector3.one;
			public Vector3 _endScale = Vector3.one;
			public Tween.Method _method = Tween.Method.Linear;
			public VoxelEmitterShape _shape;
			public VoxelEmitterColor _color = null;
			public Color _beginColor;
			public Color _endColor;
			public int _count = 1;
		}

		public VoxelPool _pool = null;

		public Parameter _parameter;
		private float next_ = 0.0f;

		public void emission(){

			for(int i = 0; i<_parameter._count; ++i){

				VoxelPoolObject obj = _pool.create ();
				VoxelParticle particle = obj.GetComponent<VoxelParticle>();
				particle.gameObject.SetActive(true);
				
				
				
				Vector3 director = new Vector3(Random.Range(_parameter._beginDirector.x/2, _parameter._endDirector.x/2), 
				                               Random.Range(_parameter._beginDirector.y/2, _parameter._endDirector.y/2),
				                               Random.Range(_parameter._beginDirector.z/2, _parameter._endDirector.z/2));
				
				
				particle.director = director.normalized;
				if( _parameter._shape == null){
					particle.position = this.transform.position;
				}else{
					particle.position = _parameter._shape.getPosition(this.transform);
				}
				
				particle.speed = Random.Range (_parameter._speed.x, _parameter._speed.y);
				particle.time = Random.Range (_parameter._time.x, _parameter._time.y);

				if(_parameter._color == null){
					particle.beginColor = _parameter._beginColor;
					particle.endColor = _parameter._endColor;
				}else{
					VoxelEmitterColor.Pair pair = _parameter._color.color();
					particle.beginColor = pair.begin;
					particle.endColor = pair.end;
				}
				particle.beginScale = _parameter._beginScale;
				particle.endScale = _parameter._endScale;
				particle.method = _parameter._method;
				particle.gameObject.layer = this.gameObject.layer;
				particle.emission();
			}
		}
		void Update () {

			if (Application.isPlaying) {
					next_ -= Time.deltaTime;
					while (next_ <= 0) {

							next_ += Random.Range (_parameter._interval.x, _parameter._interval.y);
							emission ();

					}
			}
		}
	}
}
