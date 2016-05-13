using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GDGeek{
	/*
	[ExecuteInEditMode]
	public class VoxelText : MonoBehaviour {
		public enum Aligned
		{
			Left,
			Center,
			Right,
		}

		public string _text = null;

		public Aligned _aligend = Aligned.Left;
		public VoxelTextCreater _create;
		public int _offset = -4;
		public Color _maincolor = Color.red;
		public Color _lightColor = Color.red;
		private bool locked_ = false;
		private List<VoxelChar> chars_ = new List<VoxelChar>();
		private Task run(string text){
			Task task = setTextTask (text);
			TaskManager.PushFront (task, delegate {
				locked_ = true;
						});
			TaskManager.PushBack (task, delegate {
				locked_ = false;
						});
			return task;
		}
		public void Start(){
			this.text = _text;		
		}
		private string text_ = null;
		private void refersh ()
		{
			if (!locked_ && text_ != null) {
				TaskManager.Run (run (text_));
				text_ = null;		
			}
		}

 
		public string text{
			set{
				text_ = value;
				refersh();
			}
				
		}

		public void clear(){
			if (chars_.Count != 0) {
				for(int i = 0; i < chars_.Count; ++i){
					chars_[i].gameObject.SetActive(false);
				}	
				chars_.Clear();
			}
		}
		public string compress(string text){
			string str = "";
			for (int i = 0; i< text.Length; ++i) {
				Debug.Log (_create);
				Debug.Log (text[i]);
				if(_create.has (text[i])){

					str += text[i];

				}	
			}
			return str;
		}

		private Vector3 getOffset (int count, int n)
		{

			Vector3 ret = Vector3.zero;
			if (_aligend == Aligned.Left) {
				ret = new Vector3 (n * _offset, 0, 0);
			} else if (_aligend == Aligned.Right) {
				ret = new Vector3 ((count - n) * -_offset, 0, 0);
			} else {
				
				ret =new Vector3 ((n * _offset) - ((count-1) * _offset)/2.0f , 0, 0);
						
			}
			
//			Debug.LogWarning ("count:" + count + "; n" + n + _aligend + ret);
			return ret;
		}

		Task changeTask (int i, char ch, int count)
		{


			TaskList tl = new TaskList ();

			Task remove = new TweenTask (delegate{
				chars_[i].gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
				return TweenRotation.Begin(chars_[i].gameObject, 0.15f, Quaternion.Euler(new Vector3(270, 0, 0)));
			});
			TaskManager.PushBack (remove, delegate {
				chars_[i].gameObject.SetActive(false);
			});
			tl.push (remove);



			Task add = new TweenTask (delegate{
				
				VoxelChar vc = _create.create(ch);
				if(vc != null){
					vc.gameObject.SetActive(true);
					vc.setLayer(this.gameObject.layer);
					vc.setMainColor(_maincolor);
					vc.setLightColor(_lightColor);
					vc.gameObject.transform.SetParent(this.transform);
					
					vc.gameObject.transform.localScale = Vector3.one;
					vc.gameObject.transform.localPosition = this.getOffset(count, i);
					chars_[i] = vc;
					vc.gameObject.transform.eulerAngles = new Vector3(-270, 0, 0);
					return TweenRotation.Begin(vc.gameObject, 0.15f, Quaternion.Euler(new Vector3(0, 0, 0)));
					
				}
				return null;
			});
			
			
			
			tl.push (add);


			return tl;
		}

		Task removeTask (int i)
		{
			Task task = new TweenTask (delegate{
				chars_[i].gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
				return TweenRotation.Begin(chars_[i].gameObject, 0.15f, Quaternion.Euler(new Vector3(270, 0, 0)));
			});
			TaskManager.PushBack (task, delegate {
				chars_[i].gameObject.SetActive(false);
			});
		
			return task;
		}

		Task addTask (char ch, int count)
		{
			TaskList tl = new TaskList ();
			tl.push (new TaskWait (0.15f));
			Task add = new TweenTask (delegate{

				VoxelChar vc = _create.create(ch);
				if(vc != null){
					vc.gameObject.SetActive(true);
					
					vc.setLayer(this.gameObject.layer);
					vc.gameObject.transform.SetParent(this.transform);
					
					vc.gameObject.transform.localScale = Vector3.one;
					vc.gameObject.transform.localPosition = getOffset(count, chars_.Count);//ew Vector3(chars_.Count * _offset, 0, 0);
					chars_.Add(vc);
					vc.setMainColor(_maincolor);
					vc.setLightColor(_lightColor);
					vc.gameObject.transform.eulerAngles = new Vector3(-270, 0, 0);
					return TweenRotation.Begin(vc.gameObject, 0.15f, Quaternion.Euler(new Vector3(0, 0, 0)));
					
				}
				return null;
			});
		
		

			tl.push (add);
			return tl;
		}
		public Task setTextTask(string text){
			return new TaskPack (delegate{ return setTextTask_(text);});		
		}
		private Task setTextTask_(string text){

			TaskSet ts = new TaskSet ();
			string comp = compress (text);
			int n = Mathf.Min (chars_.Count, comp.Length);
			for(int i = 0; i < n; ++i){
				if(chars_[i].ch != comp[i])
				{
					ts.push (changeTask(i, comp[i], comp.Length));
					
				}
			}
			if (chars_.Count > comp.Length) {
			
				for(int i=n; i<chars_.Count; i++){
				
					Task remove = removeTask(i);
					TaskManager.PushBack(ts, delegate{
						chars_.RemoveRange(n, chars_.Count - n);
					});
					ts.push (remove);
				}
			
			}
			
			
			if (comp.Length > chars_.Count) {
				for (int i = n; i<comp.Length; ++i) {
					ts.push (addTask(comp[i], comp.Length));
				}	
			}


		


			return ts;
		}
		public void setText(string text){
			string comp = compress (text);
			int n = Mathf.Min (chars_.Count, comp.Length);
			for(int i = 0; i < n; ++i){
				if(chars_[i].ch != comp[i])
				{
					chars_[i].gameObject.SetActive(false);
					VoxelChar vc = _create.create(comp[i]);
					vc.gameObject.SetActive(true);
					vc.setLayer(this.gameObject.layer);
					vc.setMainColor(_maincolor);
					vc.setLightColor(_lightColor);
					vc.gameObject.transform.SetParent(this.transform);
					
					vc.gameObject.transform.localScale = Vector3.one;
					chars_[i] = vc;

				}
			}
			if (chars_.Count > comp.Length) {
				for(int i = n; i < chars_.Count; ++i ){
					chars_[i].gameObject.SetActive(false);
				}		
				chars_.RemoveRange(n, chars_.Count - n);
			}


			if (comp.Length > chars_.Count) {
				for (int i = n; i<comp.Length; ++i) {
					VoxelChar vc = _create.create(comp[i]);
					if(vc != null){
						vc.gameObject.SetActive(true);
						vc.setLayer(this.gameObject.layer);
						vc.gameObject.transform.SetParent(this.transform);
						vc.gameObject.transform.localScale = Vector3.one;
						chars_.Add(vc);
						vc.setMainColor(_maincolor);
						vc.setLightColor(_lightColor);
						
					}
				}	
			}

			for (int i = 0; i< chars_.Count; ++i) {
				chars_[i].gameObject.transform.localPosition = getOffset(comp.Length, i);//new Vector3(i * _offset, 0, 0);			
			}
		}
		
	
	}*/
}
