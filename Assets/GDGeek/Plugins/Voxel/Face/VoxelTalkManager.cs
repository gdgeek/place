using UnityEngine;
using System.Collections;
using System;

namespace GDGeek{
	/*
	public class VoxelTalkManager : MonoBehaviour {
		public void close ()
		{
			_pop.close ();
			this._head.close ();
		}

		public Task closePopTask ()
		{
			Task task = new Task ();
			TaskManager.PushBack (task, delegate {
				_pop.close ();
						});
			return task;

		}

		public bool isOpen ()
		{
			return this._head.isOpen ();
		}

	
		public void goNext ()
		{
			Debug.Log ("go next!");
		}

		[Serializable]
		public struct Sentence{
			public VoxelHead.Emotions emotion;
			public string talk;
		};
		public void Awake(){
			_head.close ();
			_pop.close ();
		}
		public VoxelHead _head = null;

		//public VoxelFace _face = null;
		public VoxelFontManager _pop = null;
		//private TaskCircle blink_ = null;
		private bool isPop_ = false;
		private Task _popTask(string text, VoxelHead.Emotions emotion){
		
			if (String.IsNullOrEmpty (text)) {
				return new Task();	
			}


			Task task = null;
			if (isPop_) {
					Task next = _pop.next (text);
					task = next;
			} else {
					isPop_ = true;	
					task = _pop.openTask (text);
			}


			TaskManager.PushFront (task, delegate {
				_head.talk = true;
			});

			TaskManager.PushBack (task, delegate {
				_head.talk = false;
				_head.emotion = emotion;
			});
			return task;
		}
		public Task popTask (Sentence sentence)
		{

			TaskList tl = new TaskList ();
			tl.push (new TaskPack (delegate {
				return _popTask (sentence.talk, sentence.emotion);
						}));
			tl.push (new TaskWait (0.5f));
			return tl;	
		}

		
		public Task comeInTask ()
		{
			if (isOpen ()) {
				return new Task();			
			}
			Task task = _head.openTask ();
			TaskManager.PushBack (task, delegate {
				isPop_ = false;
						});
			return task;
		}
		public Task closeTask(){
			Task task = new Task ();
			TaskManager.PushFront (task, delegate {
				_head.close();
				_pop.close();
						});
			return task;
		}
		public Task goOutTask ()
		{
			TaskSet ts = new TaskSet ();

			ts.push(_head.closeTask ());
			ts.push (_pop.closeTask());
			return ts;
		}
	}
*/
}
