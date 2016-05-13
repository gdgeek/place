using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GDGeek{
	
	public class VoxelProduct{
		
		public class Product{
			public Dictionary<VectorInt3, VoxelHandler> voxels = null;
			public VoxelDrawData draw = null;
		}

		private Vector3 min_ = new Vector3(999, 999, 999);
		public Vector3 min{
			get{ 
				return min_;
			}
			set{ 
				min_ = value;
			}
		}
		private Vector3 max_ = new Vector3(-999, -999, -999);

		public Vector3 max{
			get{ 
				return max_;
			}
			set{ 
				max_ = value;
			}
		}

		private Product main_ = new Product();

		public Product main{
			get{ 
				return main_;
			}
			set{ 
				main_ = value;

				Mesh mesh;

			}
		}



		public Product[] sub_ = null;


		public Product[] sub{
			get{ 
				return sub_;
			}
			set{ 
				sub_ = value;
			}
		}


		private VoxelGeometry.MeshData data_ = null;
		public VoxelGeometry.MeshData getMeshData(){
			if (data_ == null) {
				data_ = new VoxelGeometry.MeshData ();
			}
				
			data_.max = this.max;
			data_.min = this.min;

			for (int i = 0; i < main.draw.vertices.Count; ++i) {
				data_.vertices.Add (main.draw.vertices [i].position);
				data_.colors.Add (main.draw.vertices [i].color);
			}
			//data_.triangles = main.draw.triangles;

			for (int i = 0; i < main.draw.triangles.Count; i += 3) {
				int n0 = main.draw.triangles[i];
				int n1 = main.draw.triangles[i+1];
				int n2 = main.draw.triangles[i+2];
				Vector3 v0 = data_.vertices [n0];
				Vector3 v1 = data_.vertices [n1];
				Vector3 v2 = data_.vertices [n2];
				Vector3 normal = Vector3.Cross((v1-v0),(v2-v1)).normalized;
				if (normal != Vector3.zero) {
					data_.triangles.Add (n0);
					data_.triangles.Add (n1);
					data_.triangles.Add (n2);

				}

			}

		
			return data_;
		}

	}

}