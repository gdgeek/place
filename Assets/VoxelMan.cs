using UnityEngine;
using System.Collections;
using GDGeek;

public class VoxelMan : MonoBehaviour {
	public Animator _animator = null;
	// Use this for initialization
	void Start () {
		TaskWait tw = new TaskWait (3.0f);
		TaskManager.PushBack (tw, delegate {
			_animator.Play ("Attack");
		});
		TaskManager.Run (tw);

		tw = new TaskWait (5.0f);
		TaskManager.PushBack (tw, delegate {
			_animator.Play ("Attack");
		});
		TaskManager.Run (tw);


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
