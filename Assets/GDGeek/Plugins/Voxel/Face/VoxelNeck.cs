using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class VoxelNeck : MonoBehaviour {

		public Tween.Method _method = Tween.Method.Linear;
		private Task move (Vector3 to, float time)
		{
			TweenTask tt = new TweenTask (delegate{
				Tween tween = TweenRotation.Begin(this.gameObject, time, Quaternion.Euler(to));
				tween.method = _method;
				return tween;
			});
			return tt;
		}

		private Task shake(int times, float time, Vector3 original, Vector3 border1, Vector3 border2){
			TaskList tl = new TaskList ();
			int index = 0;
			float step = time / (float)(times);
			Debug.LogWarning (step);
			while (times > 0) {
				switch(index){
				case 0:
					index = 1;

					times -= 1;
						tl.push (move(border1, step));
					break;
				case 1:
					if(times >=2){
						index -= 2;
						index = -1;
						tl.push (move(border2, 2 * step));
					}else{
						times-= 1;
						index = 0;
						tl.push (move( original, 2 * step));
					}
					break;
				case -1:
					if(times >= 2){
						times -= 2;
						index = 1;
						tl.push (move(border1, 2 * step));
					}else{
						times -= 1;
						index = 0;
						
						tl.push (move(original,  step));
					}
					break;

				}
			}
			return tl;
		}
		public Task yes(int times, float time){

			return shake (times *2, time, Vector3.zero, new Vector3 (10, 0, 0), new Vector3 (-20, 0, 0));

		}
		public Task no(int times, float time){
			return shake (times *2, time, Vector3.zero, new Vector3 (0, 15, 0), new Vector3 (0, -15, 0));
		}
		public Task duang(int times, float time){
			return shake (times *2, time, Vector3.zero, new Vector3 (0, 0, 15), new Vector3 (0, 0, -15));
		}
				//sway
	}
}
