using UnityEngine;
using UnityEditor;
using System.Collections;
namespace GDGeek{
	
	public class VoxelEditor : Editor {
		/*
		private static VoxelColorMaster GetMaster(){
			
			
			VoxelColorMaster master = GameObject.FindObjectOfType<VoxelColorMaster> ();

			return master;
		}*/
		[MenuItem ("GDGeek/Voxel/Create Mesh Maker")]
		static void CreateMesh(){
			Debug.Log("New");
			GameObject obj = new GameObject("VoxelMaker");
			obj.AddComponent<VoxelMaker> ();
		}
		/*
		[MenuItem ("GDGeek/Voxel/Refresh Color Master")]
		static void  RefreshMaster(){
			VoxelColorMaster master = GetMaster();
			if (master != null) {
				master.refresh ();
			} else {
				Debug.LogWarning("Can't Find VoxelColormaster!");			
			
			}
		}*/
		
		
	}
}
