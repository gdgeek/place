using UnityEngine;
using System.Collections;
using GDGeek;

public class VoxelMan : MonoBehaviour {
	public Animator _animator = null;
	public VoxelFoe _foe = null;
	public VoxelMesh _mesh = null;
	private FSM fsm_ = new FSM();
	// Use this for initialization

	public State getIdle(){
		StateWithEventMap state = new StateWithEventMap ();
		state.onStart += delegate {
			_animator.Play("Idle");
			Debug.Log("in idle");
		};
		state.addAction ("attack", "attack");
		return state;
	}
	public void attackOver(){

		fsm_.post ("atk_over");
		//_foe._fsm.post ("atk_over");
		Debug.Log ("atk over");
	}
	public State getAttack(){
		bool isAttackOver = false;
		bool doubleAttackOver = false;
		StateWithEventMap state = TaskState.Create (delegate() {
			
			Task task = new Task();
			TaskManager.PushFront(task, delegate() {
				isAttackOver =false;
				doubleAttackOver =false;
				_foe._fsm.post("hurt");
				_animator.Play("Attack");
			});
			task.isOver = delegate {
				return isAttackOver;
			};
			TaskManager.PushBack(task, delegate() {
				_animator.speed = 1;
			});
			//_animator.i
			return task;

		}, this.fsm_, delegate(FSMEvent evt) {
			if(doubleAttackOver){
				return "attack";

			}
			return "idle";	
		} );
		state.addAction ("attack", delegate(FSMEvent evt) {
			_animator.speed = 10000;
			doubleAttackOver = true;
			Debug.Log("double attack");
		});

		state.addAction ("atk_over", delegate(FSMEvent evt) {

			isAttackOver = true;	
		});

		return state;
	}
	void Start () {
		fsm_.addState ("idle", getIdle());
		fsm_.addState ("attack", getAttack ());

		fsm_.init ("idle");

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			fsm_.post ("attack");
		///Debug.Log ("space");
		}
	}
}
