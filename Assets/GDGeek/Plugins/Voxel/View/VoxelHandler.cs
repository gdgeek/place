using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


namespace GDGeek{
[Serializable]
public class VoxelHandler {
	public int id = 0;
	public Color color = Color.red;
	public List<VectorInt4> vertices = new List<VectorInt4> (); 
	public VectorInt3 position;

/*	public void setup (Voxel_ vox)
	{
	//	_vox = vox;
	//	_vox.setup (position, color, id);

	}
	
	private int back_ = 0;
	private int front_ = 0;
	private int down_ = 0;
	private int up_ = 0;
	private int left_ = 0;
	private int right_ = 0;


	private int lback_ = 0;
	private int lfront_ = 0;
	private int ldown_ = 0;
	private int lup_ = 0;
	private int lleft_ = 0;
	private int lright_ = 0;


	public int right{
		get{
			return right_;
		}
	}
	public int left{
		get{
			return left_;
		}
	}
	public int up{
		get{
			return up_;
		}
	}
	public int down{
		get{
			return down_;
		}
	}
	public int back{
		get{
			return back_;
		}
	}
	public int front{
		get{
			return front_;
		}
	}




	public int lright{
		get{
			return lright_;
		}
	}
	public int lleft{
		get{
			return lleft_;
		}
	}
	public int lup{
		get{
			return lup_;
		}
	}
	public int ldown{
		get{
			return ldown_;
		}
	}
	public int lback{
		get{
			return lback_;
		}
	}
	public int lfront{
		get{
			return lfront_;
		}
	}

	public void lightShadowBegin(){
		back_ = 0;
		up_ = 0;
		front_ = 0;
		down_ = 0;
		left_ = 0;
		right_ = 0;

		lback_ = 0;
		lup_ = 0;
		lfront_ = 0;
		ldown_ = 0;
		lleft_ = 0;
		lright_ = 0;
	}

	public void lightAdd (byte i, Vector3 face)
	{
		if( face == Vector3.back){
			lback_ |= 0x1 << i;
		}if( face == Vector3.forward){
			lfront_ |= 0x1 << i;
		}else if(face == Vector3.up){
			lup_ |= 0x1 <<i;

		}else if(face == Vector3.down){
			ldown_ |= 0x1 <<i;

		}else if(face == Vector3.left){
			lleft_ |= 0x1 <<i;

		}else if(face == Vector3.right){
			lright_ |= 0x1 <<i;

		}

	}
	public void shadowAdd(byte i, Vector3 face){
		if(face == Vector3.back){
			back_ |= 0x1 << i;
		}if(face == Vector3.forward){
			front_ |= 0x1 << i;
		}else if(face == Vector3.up){
			up_ |= 0x1 <<i;

		}else if(face == Vector3.down){
			down_ |= 0x1 <<i;

		}else if(face == Vector3.left){
			left_ |= 0x1 <<i;

		}else if(face == Vector3.right){
			right_ |= 0x1 <<i;
		}




	}
	public static Vector2 GetUV(int i){

		float unit = 1.0f / 16f;
		Vector2 uv = new Vector2 ();
		uv.x = (i&0xf) * unit;
		uv.y = 1.0f - unit *(((i >> 0x4)&0xf) +1);
		return uv;

	}
	public void lightShadowEnd(){


	}
*/


}
}