using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class TweenVoxelBlow : Tween
	{
		/*
		private class Item{
			
			public Vector3 localPosition = Vector3.one;
			
		}
		
		private Item from = new Item();
		private Item to = new Item();
		private Voxel vox = null;
		*/
		override protected void OnUpdate (float factor, bool isFinished)
		{   
			
		//	vox.transform.localPosition = from.localPosition * (1f - factor) + to.localPosition * factor;
		//	vox.color = from.color * (1f - factor) + to.color * factor;
		}
		/*
		
		/// <summary>
		/// Start the tweening operation.
		/// </summary>
		//13801889517 zhou ping
		static public TweenVoxel Begin (Voxel vox, float duration, VoxelData to, Vector3 offset)
		{
			TweenVoxel comp = Tween.Begin<TweenVoxel>(vox.gameObject, duration);
			comp.from.localPosition = vox.gameObject.transform.localPosition;// = comp.alpha;
			comp.from.color = vox.color;
			comp.vox = vox;
			comp.to.localPosition.x = to.x + offset.x;//-1;
			comp.to.localPosition.y = to.y + offset.y;//-4;
			comp.to.localPosition.z = to.z + offset.z;//-0.5f;
			comp.to.color = to.color;
			//comp.to = alpha;
			/*
			if (duration <= 0f)
			{
				comp.Sample(1f, true);
				comp.enabled = false;
			}*/
		/*
			return comp;
		}
		*/
	}
	
	
}
