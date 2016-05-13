using UnityEngine;
using System.Collections;
using System;


namespace GDGeek{
	/*
	public class VoxelFace : MonoBehaviour {
		public VoxelNeck _neck;
		public VoxelLookat _lookat;
		public VectorInt3 _offset = VectorInt3.zero;
		[Serializable]
		public class Point
		{
			public VectorInt3 position;
			public Color color = Color.red;
		}
		
		[Serializable]
		public class Rect
		{
			public VectorInt3 min;
			public VectorInt3 max;
			public Color color = Color.red;
		}
		[Serializable]
		public class Powder{
			public Rect[] rect;
			public Point[] point;
		}
		[Serializable]
		public class Organ
		{	
			public VectorInt3 max;
			public VectorInt3 min;
			public Powder background;
			public Powder[] action;
		}
		
		public enum EyeAction
		{
			Normal = 0,
			LeftUp = 1,
			Up = 2,
			RightUp = 3,
			Left = 4,
			Right = 5,
			LeftDown = 6,
			Down = 7,
			RightDown = 8,
		};
		
		
		public enum EyebrowAction
		{
			Normal = 0,
			Happy = 1,
			Cry = 2,
			Angry = 3,
			Sorry =4,
		};
		public enum EyelidAction{
			Normal = 0,
			Up = 1,
			Down = 2,
			Mid = 4,
			One = 1,
			Two = 3,
			Three = 7,
			Narrow = 5,
		}
		public enum MouseAction
		{
			Cry = 0,
			Normal = 1,
			Surpridsed = 2,
			Sorry =3,
			Ah = 4,
			HaHa = 5,
			Greedy = 6,
			Oh = 7,
			SmallLeft = 8,
			SmallRight = 9,
			Fiu = 10,
			
			
		};
		
		public Organ _eyelid;
		public Organ _eyebrow;
		public Organ _eye;
		public Organ _mouth;
		
		public VoxelMesh _mesh;//

		public void doMouseAction(MouseAction act){
			doEmotions (_mouth, (int)act);
		}
		
		public void doMouseAction(Color[] colors, MouseAction act){
			doEmotions (colors, _mouth, (int)act);
		}
		
		public void doEyelidAction(EyelidAction act){
			
			Color[] colors = this.beginEmotions();

			doEyelidAction (colors, act);
			this.endEmotions (colors);
		}
		
		public void doEyelidAction(Color[] colors, EyelidAction act){
			//Debug.Log ((int)act);
			//Debug.Log ((int)EyelidAction.Up);
			//Debug.Log ((int)EyelidAction.Down);
			//Debug.Log ((int)EyelidAction.Mid);

			if (((int)act & (int)EyelidAction.Up) != 0) {
				//Debug.Log (1);
				doEmotions(colors, this._eyelid, 1);			
			}
			if (((int)act & (int)EyelidAction.Down) != 0) {
				//Debug.Log (2);
				doEmotions(colors, this._eyelid,2);			
			}
			if (((int)act & (int)EyelidAction.Mid) != 0) {
				//Debug.Log (3);
				doEmotions(colors, this._eyelid, 0);			
			}
		}
		
		public void doEyeAction(EyeAction act){
			doEmotions (_eye, (int)act);
		}
		public void doEyeAction(Color[] colors, EyeAction act){
			doEmotions (colors, _eye, (int)act);
		}
		public void doPowder(Powder powder, VectorInt3 min, VectorInt3 max){
			Color[] colors = this.beginEmotions();
			doPowder (colors, powder, min, max);
			this.endEmotions (colors);
		}
		public void doPowder(Color[] colors, Powder powder, VectorInt3 min, VectorInt3 max){

			
			VectorInt3 mi = min + this._offset;
			//VectorInt3 mx = max + this._offset;
			foreach (VoxelFace.Rect r in powder.rect) {
				
				for (int x = r.min.x; x<r.max.x; ++x) {
					for(int y = r.min.y; y< r.max.y; ++y){
						for(int z = r.min.z;z<r.max.z; ++z ){
							VoxelHandler handler = _mesh.getVoxel(new VectorInt3(x, y, z) + mi);
//							Debug.Log ("x" + x + "y" +y +"z" + z);
							if(handler != null){
								foreach(VectorInt4 vertice in handler.vertices){
									
									colors[vertice.x] = r.color;
									colors[vertice.y] = r.color;
									colors[vertice.z] = r.color;
									colors[vertice.w] = r.color;
								}
							}
						}
					}			
				}
			}
			
			foreach (VoxelFace.Point p in powder.point) {
				
				
				VoxelHandler handler = _mesh.getVoxel(p.position + mi);
				if(handler != null){
					foreach(VectorInt4 vertice in handler.vertices){
						
						colors[vertice.x] = p.color;
						colors[vertice.y] = p.color;
						colors[vertice.z] = p.color;
						colors[vertice.w] = p.color;
					}
				}
				
				
			}
			
		}
		public Color[] beginEmotions(){
			Mesh mesh = _mesh._mesh.mesh;
			
			Color[] colors = mesh.colors;
			return colors;
		}
		public void endEmotions(Color[] colors){
			
			Mesh mesh = _mesh._mesh.mesh;
			mesh.colors = colors;
		}
		public void doEmotions(Color[] colors, Organ organs, int act){

			doPowder (colors, organs.background, organs.min, organs.max);
			doPowder (colors, organs.action[act], organs.min, organs.max);		
		}
		
		public void doEmotions(Organ organs, int act){
			Color[] colors = this.beginEmotions ();
			doPowder (colors, organs.background, organs.min, organs.max);
			doPowder (colors, organs.action[act],organs.min, organs.max);	
			this.endEmotions (colors);
		}
		
		public void doEyebrowAction (EyebrowAction act)
		{
			
			doEmotions (_eyebrow, (int)act);
		}
		
		public void doEyebrowAction (Color[] colors, EyebrowAction act)
		{
			
			doEmotions (colors, _eyebrow, (int)act);
		}
		/*
		public void Start(){
		
			TaskList tl = new TaskList ();

			tl.push (TaskWait.Create(0.5f, delegate{
				Color[] colors = this.beginEmotions();
				doEyeAction (colors, EyeAction.LeftUp);
				this.endEmotions(colors);
			}));
			
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyeAction (EyeAction.Normal);
			}));
			
			
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyeAction (EyeAction.Up);
			}));
			
			
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyeAction (EyeAction.Normal);
				this.doEmotions (this._eyelid, 1);
			}));
			
			
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyeAction (EyeAction.RightUp);
			}));
			
			
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyeAction (EyeAction.Normal);
				this.doEmotions (this._eyelid, 1);
			}));
			
			
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyeAction (EyeAction.Left);
				this.doEmotions (this._eyelid, 1);
			}));
			
			
			
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyeAction (EyeAction.Right);
			}));
			
			
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyeAction (EyeAction.LeftDown);
			}));
			
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyeAction (EyeAction.Down);
			}));
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyeAction (EyeAction.RightDown);
			}));
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyeAction (EyeAction.Normal);
			}));
			
			
			
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyebrowAction (EyebrowAction.Happy);
			}));
			
			
			
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyebrowAction (EyebrowAction.Normal);
			}));
			
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyebrowAction (EyebrowAction.Sorry);
			}));
			
			
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyebrowAction (EyebrowAction.Normal);
			}));
			tl.push (TaskWait.Create(0.1f, delegate{
				this.doEyelidAction (EyelidAction.One);
				
			}));
			tl.push (TaskWait.Create(0.1f, delegate{
				this.doEyelidAction (EyelidAction.Two);
				
			}));
			tl.push (TaskWait.Create(0.1f, delegate{
				this.doEyelidAction (EyelidAction.Three);
				
			}));
			tl.push (TaskWait.Create(0.1f, delegate{
				this.doEyeAction (EyeAction.Normal);
				
			}));
			
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyebrowAction (EyebrowAction.Cry);
			}));
			
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyebrowAction (EyebrowAction.Normal);
			}));
			
			
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyebrowAction (EyebrowAction.Angry);
			}));
			
			
			tl.push (TaskWait.Create(0.5f, delegate{
				doEyebrowAction (EyebrowAction.Normal);
			}));
			
			
			tl.push (TaskWait.Create(0.5f, delegate{
				//this.doEmotions (this._mouth,0);
				doMouseAction (MouseAction.Cry);
			}));
			
			tl.push (TaskWait.Create(0.5f, delegate{
				this.doEmotions (this._mouth,1);
				//doMouseAction (EyebrowAction.Normal);
			}));
			
			tl.push (TaskWait.Create(0.5f, delegate{
				this.doEmotions (this._mouth,2);
				//doMouseAction (EyebrowAction.Normal);
			}));
			
			tl.push (TaskWait.Create(0.5f, delegate{
				this.doEmotions (this._mouth,3);
				//doMouseAction (EyebrowAction.Normal);
			}));
			tl.push (_neck.yes (3,0.9f));
			tl.push (TaskWait.Create(0.2f, delegate{}));

			tl.push (_neck.no (2,0.6f));
			tl.push (TaskWait.Create(0.2f, delegate{}));
			tl.push (_neck.duang (4,1.2f));

			tl.push (TaskWait.Create(0.5f, delegate{
				this.doEmotions (this._mouth,4);
				//doMouseAction (EyebrowAction.Normal);
			}));
			
			/*



			tl.push (TaskWait.Create(0.5f, delegate{
				this.doEmotions (this._mouth,5);
			}));
			
			
			tl.push (TaskWait.Create(0.5f, delegate{
				this.doEmotions (this._mouth,6);
			}));
			
			
			tl.push (TaskWait.Create(0.5f, delegate{
				this.doEmotions (this._mouth,7);
			}));
			
			
			tl.push (TaskWait.Create(0.5f, delegate{
				this.doEmotions (this._mouth,8);
			}));
			
			
			tl.push (TaskWait.Create(0.5f, delegate{
				this.doEmotions (this._mouth,9);
			}));
			tl.push (TaskWait.Create(0.1f, delegate{
				this.doEyelidAction (EyelidAction.One);
				
			}));
			tl.push (TaskWait.Create(0.1f, delegate{
				this.doEyelidAction (EyelidAction.Two);
				
			}));
			tl.push (TaskWait.Create(0.1f, delegate{
				this.doEyelidAction (EyelidAction.Three);
				
			}));
			tl.push (TaskWait.Create(0.1f, delegate{
				this.doEyeAction (EyeAction.Normal);
				
			}));

			tl.push (TaskWait.Create(0.1f, delegate{
				this.doEyelidAction (EyelidAction.One);
				
			}));
			tl.push (TaskWait.Create(0.1f, delegate{
				this.doEyelidAction (EyelidAction.Two);
				
			}));
			tl.push (TaskWait.Create(0.1f, delegate{
				this.doEyelidAction (EyelidAction.Three);
				
			}));
			tl.push (TaskWait.Create(0.1f, delegate{
				this.doEyeAction (EyeAction.Normal);
				
			}));
		
			TaskManager.PushFront (tl,delegate{
				Debug.Log ("!!!!!");
			});
			//TaskManager.Run (tl);
			//doEyebrowAction (EyebrowAction.Sorry);
			//doEyeAction (EyeAction.Normal);

		}*/
		
/*		
	}
*/
}
