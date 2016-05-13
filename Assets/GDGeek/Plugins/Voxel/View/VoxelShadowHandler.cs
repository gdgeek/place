using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


namespace GDGeek{
	
	[Serializable]
	public class VoxelShadowHandler{

		public VectorInt3 _position;
		public bool _realSpot = false;
		public bool _ghostSpot = false;
		public bool _isGhost = false;
		private int ghost_ = 0;
		private int real_ = 0;

		public int ghost{
			get{
				return ghost_;
			}
		}
		
		
		
		
		public int real{
			get{
				return real_;
			}
		}

		public void shadowBegin(){
			ghost_ = 0;
			real_ = 0;

		}
		
		public void ghostAdd (byte i)
		{
			ghost_ |= 0x1 << i;
			
		}
		public void realAdd(byte i){
			real_ |= 0x1 << i;

		}
		public static Vector2 GetUV(int i){
			
			float unit = 1.0f / 16f;
			Vector2 uv = new Vector2 ();
			uv.x = (i&0xf) * unit;
			uv.y = 1.0f - unit *(((i >> 0x4)&0xf) +1);
			return uv;
			
		}
		public void shadowEnd(){
			
			
		}


		//public bool real = false;

	}

}
