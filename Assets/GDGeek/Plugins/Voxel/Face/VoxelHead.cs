using UnityEngine;
using System.Collections;
using System;


namespace GDGeek{
	/*
	public class VoxelHead : MonoBehaviour {
		public bool isOpen ()
		{
			return this._neck.gameObject.activeSelf;
		}

		
		public VoxelFace _face = null;
		public VoxelNeck _neck = null;
		private bool refersh_ = false;
		private bool isTalk_ = false;
		public VoxelFace.MouseAction[] _talks = null;
		private VoxelFace.MouseAction talk_ = VoxelFace.MouseAction.Normal;
		public enum Emotions{
			Normal,
			Small,
			Surprise,
		}
		[Serializable]
		public class Emotion{
			public Emotions emotion = Emotions.Normal;
			public VoxelFace.EyeAction eye = VoxelFace.EyeAction.Normal;
			public VoxelFace.MouseAction mouse = VoxelFace.MouseAction.Normal;
			public VoxelFace.EyebrowAction eyebrow = VoxelFace.EyebrowAction.Normal;
		
		}
		public Emotion[] _emotions = null;
		private Emotion normal_ = new Emotion();

		private float blinkTime_ = 0.0f;
		private float talkTime_ = 0.0f;
		private float blinkAllTime_ = 2.0f;

		private VoxelFace.EyelidAction blink_ = VoxelFace.EyelidAction.Normal;

		public void close(){
			this._neck.gameObject.SetActive (false);
		}

		private Emotions emotion_ = Emotions.Normal;

		public Emotions emotion{
			set{
				emotion_ = value;
				this.refersh_ = true;
			}
		}

		public bool talk {

			set{
				isTalk_ = value;
				this.refersh_ = true;
				talkTime_ = 1.0f;
			}
		}
		public void open(){

			this._neck.gameObject.SetActive (true);
			refersh_ = true;
		}
		public Task openTask(){
			if (this.isOpen()) {
				return new Task();			
			}
			TweenTask task = new TweenTask (delegate {
				Tween tween = TweenScale.Begin(_neck.gameObject, 0.3f, Vector3.one);
				return tween;
			});
			
			
			
			TaskManager.PushFront (task, delegate {
				refersh_ = true;
				_neck.gameObject.transform.localScale = Vector3.zero;
				open();
			});
			return task;
			
		}

		
		public Task closeTask(){
			
			TweenTask task = new TweenTask (delegate {
				Tween tween = TweenScale.Begin(_neck.gameObject, 0.3f, Vector3.zero);
				return tween;
			});
			
			TaskManager.PushBack (task, delegate {
				close();
						});
			
			TaskManager.PushFront (task, delegate {
				_neck.gameObject.transform.localScale = Vector3.one;

			});
			return task;
			
		}

		private void refershEmotion(Emotion emotion){

			Color[] colors = _face.beginEmotions();
			_face.doEyeAction(colors, emotion.eye);
			if(isTalk_){
				_face.doMouseAction(colors, talk_);
			}else{
				_face.doMouseAction(colors, emotion.mouse);
			}
			
			_face.doEyebrowAction(colors, emotion.eyebrow);
			_face.doEyelidAction(colors, blink_);
			_face.endEmotions(colors);
		}
		private void refershEmotions(){
				
			
			for(int i =0; i< _emotions.Length; ++i){
				if(this.emotion_ == _emotions[i].emotion){
					refershEmotion(_emotions[i]);
					return;
				}
			}
			refershEmotion (this.normal_);
		}
		private void refersh ()
		{
			if (refersh_) {
				refersh_ = false;
				refershEmotions();
				if(emotion_ == Emotions.Normal){

				}

			}
		}

		private void updateEyelid ()
		{
			blinkTime_ += Time.deltaTime;
			switch(blink_){
			case VoxelFace.EyelidAction.Normal:
				if(blinkTime_ > blinkAllTime_){
					blink_ = VoxelFace.EyelidAction.One;
					blinkTime_ = 0.0f;
					refersh_ = true;
				}
				break;
			case VoxelFace.EyelidAction.One:
				if(blinkTime_ > 0.06f){
					blink_ = VoxelFace.EyelidAction.Two;
					blinkTime_ = 0.0f;
					refersh_ = true;
				}
				break;
			case VoxelFace.EyelidAction.Two:
				if(blinkTime_ > 0.06f){
					blink_ = VoxelFace.EyelidAction.Three;
					blinkTime_ = 0.0f;
					refersh_ = true;
				}
				break;
			case VoxelFace.EyelidAction.Three:
				if(blinkTime_ > 0.061f){
					blink_ = VoxelFace.EyelidAction.Normal;
					blinkTime_ = 0.0f;
					blinkAllTime_ = UnityEngine.Random.Range(2.0f, 6.0f);
					refersh_ = true;
				}
				break;
			}
		}

		private void updateTalk ()
		{
			if (isTalk_) {
//				Debug.Log ("!!@#");
				talkTime_ += Time.deltaTime;
				if(talkTime_ > 0.1f && _talks.Length > 0){
					talk_ = _talks[UnityEngine.Random.Range(0, _talks.Length)];
					talkTime_ = 0.0f;
					refersh_ = true;
				}
						
			}
		}

		public void Update(){
			updateEyelid ();
			updateTalk ();
			refersh ();
		}
	}*/
}
