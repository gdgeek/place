using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GDGeek;

public class VoxelDrawMesh{




	private Mesh createMesh()
	{
		Mesh m = new Mesh();
		m.name = "ScriptedMesh";
		m.vertices = vertices_.ToArray ();
		
		m.colors = colors_.ToArray ();
		m.uv = this.uv_.ToArray ();
		m.uv2 = this.uv1_.ToArray ();

		m.triangles = triangles_.ToArray ();
		m.RecalculateNormals();

		return m;
	}
	public void print(){
		foreach (Vector3 n in vertices_) {
			Debug.Log(n);		
		}

		foreach (int n in triangles_) {
			Debug.Log(n);		
		}

	}

	private List<Vector3> vertices_ = new List<Vector3>();
	private List<Vector2> uv_ = new List<Vector2>();
	private List<Vector2> uv1_ = new List<Vector2>();
	private List<int> triangles_ = new List<int> ();
	private List<Color> colors_ = new List<Color> ();
	public VectorInt4 addRect(Vector3 direction, VectorInt3 position, Vector2 shadow, Vector2 light, Color color){


		Vector3 offset = new Vector3(position.x, position.z, position.y);

		
		triangles_.Add (vertices_.Count + 0);
		triangles_.Add (vertices_.Count + 1);
		triangles_.Add (vertices_.Count + 2);
		triangles_.Add (vertices_.Count + 1);
		triangles_.Add (vertices_.Count + 3);
		triangles_.Add (vertices_.Count + 2);


		colors_.Add (color);
		colors_.Add (color);
		colors_.Add (color);
		colors_.Add (color);

		if (direction == Vector3.up) {
			vertices_.Add (new Vector3 (-0.5f, 0.5f, 0.5f) + offset);
			vertices_.Add (new Vector3 (0.5f, 0.5f, 0.5f) + offset);
			vertices_.Add (new Vector3 (-0.5f, 0.5f, -0.5f) + offset);
			vertices_.Add (new Vector3 (0.5f, 0.5f, -0.5f) + offset);
			
		}if (direction == Vector3.down) {
			vertices_.Add (new Vector3 (-0.5f, -0.5f, -0.5f) + offset);
			vertices_.Add (new Vector3 (0.5f, -0.5f, -0.5f) + offset);
			vertices_.Add (new Vector3 (-0.5f, -0.5f, 0.5f) + offset);
			vertices_.Add (new Vector3 (0.5f, -0.5f, 0.5f) + offset);
			
		}else if (direction == Vector3.back) {
			vertices_.Add (new Vector3 (-0.5f, 0.5f, -0.5f) + offset);
			vertices_.Add (new Vector3 (0.5f, 0.5f, -0.5f) + offset);
			vertices_.Add (new Vector3 (-0.5f, -0.5f, -0.5f) + offset);
			vertices_.Add (new Vector3 (0.5f, -0.5f, -0.5f) + offset);
			
			
		}else if (direction == Vector3.forward) {
			vertices_.Add (new Vector3 (-0.5f, -0.5f, 0.5f) + offset);
			vertices_.Add (new Vector3 (0.5f, -0.5f, 0.5f) + offset);
			vertices_.Add (new Vector3 (-0.5f, 0.5f, 0.5f) + offset);
			vertices_.Add (new Vector3 (0.5f, 0.5f, 0.5f) + offset);
			
			
		}else if (direction == Vector3.left) {
			vertices_.Add (new Vector3 (0.5f, -0.5f, 0.5f) + offset);
			vertices_.Add (new Vector3 (0.5f, -0.5f, -0.5f) + offset);
			vertices_.Add (new Vector3 (0.5f, 0.5f, 0.5f) + offset);
			vertices_.Add (new Vector3 (0.5f, 0.5f, -0.5f) + offset);
		}else if (direction == Vector3.right) {

			vertices_.Add (new Vector3 (-0.5f, -0.5f, -0.5f) + offset);
			vertices_.Add (new Vector3 (-0.5f, -0.5f, 0.5f) + offset);
			vertices_.Add (new Vector3 (-0.5f, 0.5f, -0.5f) + offset);
			vertices_.Add (new Vector3 (-0.5f, 0.5f, 0.5f) + offset);
		}
		float magic = 0.03125f;//
		this.uv_.Add (new Vector2 (light.x +magic, light.y +magic));
		this.uv_.Add (new Vector2 (light.x, light.y +magic));
		this.uv_.Add (new Vector2 (light.x +magic, light.y+0.0625f));
		this.uv_.Add (new Vector2 (light.x, light.y+0.0625f));

		this.uv1_.Add (new Vector2 (shadow.x +magic, shadow.y +magic));
		this.uv1_.Add (new Vector2 (shadow.x, shadow.y +magic));
		this.uv1_.Add (new Vector2 (shadow.x +magic, shadow.y+0.0625f));
		this.uv1_.Add (new Vector2 (shadow.x, shadow.y+0.0625f));


		return new VectorInt4 (vertices_.Count-4, vertices_.Count-3, vertices_.Count-2, vertices_.Count-1);

	}
	public MeshFilter crateMeshFilter(string name, Material material){
		GameObject go = new GameObject(name);
		MeshFilter meshFilter = go.AddComponent<MeshFilter>();
		meshFilter.mesh = createMesh();
		MeshRenderer renderer = go.AddComponent<MeshRenderer>();
		renderer.material = material;


		return meshFilter;
	}

}
