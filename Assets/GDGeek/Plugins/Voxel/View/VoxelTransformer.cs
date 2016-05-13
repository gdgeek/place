using UnityEngine;
using System.Collections;
using GDGeek;

public class VoxelTransformer : MonoBehaviour {
	
	public GameObject _capLeft = null;
	public GameObject _capRight = null;
	public GameObject _wingLeft = null;
	public GameObject _fireLeft = null;

	
	public GameObject _wingRight = null;
	public GameObject _fireRight = null;
	
	public GameObject _rocketLeft = null;
	public GameObject _rocketRight = null;
	public GameObject _head = null;
	public GameObject _headFire = null;
	public GameObject _end = null;
	
	private Task outHead(){
		return new TweenTask (delegate() {
			return TweenLocalPosition.Begin (_head, 1.0f, new Vector3 (0, 0, 3));
		});
	}
	private Task outEnd(){
		
		return new TweenTask (delegate() {
			return TweenLocalPosition.Begin (_end, 1.0f, new Vector3 (0, 0, -3));
		});
	}

	private Task inHead(){
		return new TweenTask (delegate() {
			return TweenLocalPosition.Begin (_head, 1.0f, new Vector3 (0, 0, 0));
		});
	}
	private Task inEnd(){
		
		return new TweenTask (delegate() {
			return TweenLocalPosition.Begin (_end, 1.0f, new Vector3 (0, 0, 0));
		});
	}
	
	private Task outHeadAndEnd(){
		TaskSet ts = new TaskSet ();
		ts.push (outHead ());
		ts.push (outEnd ());
		return ts;
		
	}
	
	private Task inHeadAndEnd(){
		TaskSet ts = new TaskSet ();
		ts.push (inHead ());
		ts.push (inEnd ());
		return ts;
		
	}
	
	private Task outWing(){
		
		TaskSet ts = new TaskSet ();
		TweenTask left = new TweenTask (delegate() {
			return TweenLocalPosition.Begin(_wingLeft, 1.0f, new Vector3(-5, 0, 0));
		});
		ts.push (left);
		
		
		TweenTask right = new TweenTask (delegate() {
			return TweenLocalPosition.Begin(_wingRight, 1.0f, new Vector3(5, 0, 0));
		});
		
		ts.push (right);
		return ts;
	}

	
	private Task inWing(){
		
		TaskSet ts = new TaskSet ();
		TweenTask left = new TweenTask (delegate() {
			return TweenLocalPosition.Begin(_wingLeft, 1.0f, new Vector3(0, 0, 0));
		});
		ts.push (left);
		
		
		TweenTask right = new TweenTask (delegate() {
			return TweenLocalPosition.Begin(_wingRight, 1.0f, new Vector3(0, 0, 0));
		});
		
		ts.push (right);
		return ts;
	}
	public Task outFire(){
		TaskSet ts = new TaskSet ();
		TweenTask left = new TweenTask (delegate() {
			return TweenLocalPosition.Begin(_fireLeft, 1.0f, new Vector3(0, 0, 10));
		});
		ts.push (left);
		
		TweenTask right = new TweenTask (delegate() {
			return TweenLocalPosition.Begin(_fireRight, 1.0f, new Vector3(0, 0, 10));
		});
		
		ts.push (right);
		return ts;
	}


	public Task inFire(){
		TaskSet ts = new TaskSet ();
		TweenTask left = new TweenTask (delegate() {
			return TweenLocalPosition.Begin(_fireLeft, 1.0f, new Vector3(0, 0, 0));
		});
		ts.push (left);
		
		TweenTask right = new TweenTask (delegate() {
			return TweenLocalPosition.Begin(_fireRight, 1.0f, new Vector3(0, 0, 0));
		});
		
		ts.push (right);
		return ts;
	}

	public Task outCap(){
		TaskSet ts = new TaskSet ();
		TweenTask left = new TweenTask (delegate() {
			return TweenRotation.Begin(_capLeft, 1.0f, Quaternion.Euler(0, 0 , 179.999f));
		});
		ts.push (left);
		
		TweenTask right = new TweenTask (delegate() {
			return TweenRotation.Begin(_capRight, 1.0f, Quaternion.Euler(0, 0 , -179.999f));
		});
		
		ts.push (right);
		return ts;
	}

	public Task inCap(){
		TaskSet ts = new TaskSet ();
		TweenTask left = new TweenTask (delegate() {
			return TweenRotation.Begin(_capLeft, 1.0f, Quaternion.Euler(0, 0 , 0));
		});
		ts.push (left);
		
		TweenTask right = new TweenTask (delegate() {
			return TweenRotation.Begin(_capRight, 1.0f, Quaternion.Euler(0, 0 , 0));
		});
		
		ts.push (right);
		return ts;
	}

	public Task outRocket(){
		TaskSet ts = new TaskSet ();
		TweenTask left = new TweenTask (delegate() {
			
			return TweenRotation.Begin(_rocketLeft, 1.0f,  Quaternion.Euler(-30, 0 , 0));
		});
		ts.push (left);
		
		TweenTask right = new TweenTask (delegate() {
			return TweenRotation.Begin(_rocketRight, 1.0f,  Quaternion.Euler(-30, 0 , 0));
		});

		
		ts.push (right);

		TweenTask end =  new TweenTask (delegate() {
			return TweenLocalPosition.Begin (_end, 1.0f, new Vector3 (0, 0, 3));
		});


		ts.push (end);
		return ts;
	}

	Task inRocket ()
	{
		TaskSet ts = new TaskSet ();
		TweenTask left = new TweenTask (delegate() {
			
			return TweenRotation.Begin(_rocketLeft, 1.0f,  Quaternion.Euler(0, 0 , 0));
		});
		ts.push (left);
		
		TweenTask right = new TweenTask (delegate() {
			return TweenRotation.Begin(_rocketRight, 1.0f,  Quaternion.Euler(0, 0 , 0));
		});
		
		
		ts.push (right);
		
		TweenTask end =  new TweenTask (delegate() {
			return TweenLocalPosition.Begin (_end, 1.0f, new Vector3 (0, 0, -3));
		});
		
		
		ts.push (end);
		return ts;
	}

	public Task showTask () {
		TaskList tl = new TaskList ();
		tl.push (new TaskWait (3.0f));
		tl.push (outWing());
		tl.push (outFire());
		tl.push (outHeadAndEnd());
		tl.push (outCap());
		tl.push (outRocket());
		
		tl.push (new TaskWait (3.0f));
		
		tl.push (inRocket());
		tl.push (inCap());
		tl.push (inHeadAndEnd());
		tl.push (inFire());
		tl.push (inWing());
		return tl;
	}
	

}
