using UnityEngine;
using System.Collections;
namespace GDGeek{
	/*
	public class VoxelEffect : MonoBehaviour {


		public Tween.Method method {
			set{
				method_ = value;
			}
		}
		public Vector3 beginScale {
			set{
				beginScale_ = value;
			}
		}
		public Vector3 endScale {
			set{
				endScale_ = value;
			}
		}

		public Vector3 position {
			set{
				this.gameObject.transform.position = value;
			}
		}


		public Color beginColor {
			set{
				beginColor_ = value;
			}
		}

		public Color endColor {
			set{
				endColor_ = value;
			}
		}

		public float time {
			set{
				time_ = value;
			}
		}

		public float speed {
			set{
				speed_ = value;
			}
		}

		public Vector3 director {

			set{
				director_ = value;
			}
		}
		private Vector3 director_ = Vector3.one;

		private float speed_ = 1.0f;
		private float time_ = 1.0f;
		private Color beginColor_;
		private Color endColor_;
		private Tween.Method method_;
		private Vector3 beginScale_;
		private Vector3 endScale_;

		public void emission ()
		{


			VoxelProperty prop = new VoxelProperty ();
			this.voxel_.color = beginColor_;
			this.transform.localScale = this.beginScale_;
			prop.color = endColor_;

			prop.position = this.transform.position + director_ * speed_ * time_;
			prop.scale = this.endScale_;
			Tween tween = TweenVoxel.Begin(this.gameObject, time_, prop);
			tween.method = this.method_;
			tween.onFinished = delegate {
				this.gameObject.SetActive(false);
			};
		}

		private Voxel voxel_ = null;
		public Voxel voxel{
			set{ 
				voxel_ = value;
			}
		}
		public void Awake(){
			if (voxel_ == null) {
				voxel_ = this.gameObject.GetComponent<Voxel>();		
			}
		}


	}*/
}