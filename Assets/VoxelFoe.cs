using UnityEngine;
using System.Collections;
using GDGeek;

public class VoxelFoe : MonoBehaviour {
	public FSM _fsm = new FSM ();
	public VoxelHp _hpBar = null;
	public VoxelMesh _mesh;
	public GameObject _old;
	public GameObject _girl;
	public int _hp = 5;
	public int _number = 0;
	public Animator _animator = null;
	public State getIdle(){
		StateWithEventMap state = new StateWithEventMap ();
		state.onStart += delegate {
			_animator.Play("Idle");
			Debug.Log("in foe idle");
		};
		state.addAction ("hurt", delegate(FSMEvent evt) {
			_hp--; 

			_hpBar.setValue (_hp);
			if(_hp > 0){
				
				return "hurt";
			}else{
				return "die";
			}
		});
		return state;
	}


	public State getHurt(){
		StateWithEventMap state = TaskState.Create (delegate() {
			Task task = new TaskWait(0.3f);
			TaskManager.PushFront(task, delegate() {
				_animator.Play("Hurt");
				Debug.Log("in attack");

			});

			return task;

		}, this._fsm, "idle");


		return state;
	}
	public State getDie(){
		
		StateWithEventMap state = TaskState.Create (delegate() {
			Task task = new TaskWait(0.4f);
			TaskManager.PushFront(task, delegate() {
				VoxelBoom.GetInstance().emission(_mesh, 25);
				_old.SetActive(false);
				_girl.SetActive(false);
			});

			return task;

		}, this._fsm, "start");


		return state;
	}

	public State getStart(){

		StateWithEventMap state = TaskState.Create (delegate() {
			Task task = new TaskWait(0.1f);
			TaskManager.PushFront(task, delegate {
				_hp = 5;
				_hpBar.setValue (_hp);
				_number ++;
				if(_number %2 == 1){
					_girl.SetActive(false);
					_old.SetActive(true);
				}else{

					_girl.SetActive(true);
					_old.SetActive(false);

				}
			});
		//	_hpBar.setValue (_hp);
			return task;

		}, this._fsm, "idle");


		return state;
	}
	void Start () {

		_fsm.addState ("start", getStart());
		_fsm.addState ("idle", getIdle());
		_fsm.addState ("hurt", getHurt());
		_fsm.addState ("die", getDie());
		_fsm.init ("start");
	}
	

}
