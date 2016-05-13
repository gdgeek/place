using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek
{
	public class VoxelMerge {

		public VoxelMerge(){
			
		}
		public void build(VoxelProduct product){
			VoxelProduct.Product[] sub = product.sub;
			VoxelProduct.Product main = new VoxelProduct.Product ();
			main.draw = new VoxelDrawData ();
			main.draw.vertices.Clear ();
			main.draw.triangles.Clear ();

			for (int i = 0; i < sub.Length; ++i) {
				int offset = main.draw.vertices.Count;
				for (int j = 0; j < sub [i].draw.vertices.Count; ++j) {
					main.draw.vertices.Add (sub [i].draw.vertices[j]);
				}

				for (int n = 0; n < sub [i].draw.triangles.Count; ++n) {
					main.draw.triangles.Add (sub [i].draw.triangles[n]+ offset);
				}

			}
			product.sub = null;
			product.main = main;

		}
		public Task task(VoxelProduct product){
			Task task = new Task ();
			TaskManager.PushFront (task, delegate {
				build(product);	
			});
			return task;
		}

	}
}