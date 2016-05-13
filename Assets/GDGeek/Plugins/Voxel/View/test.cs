using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	private float x_ = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		x_ += Time.deltaTime*5;
		this.transform.localRotation = Quaternion.Euler(new Vector3(0, x_,0));
	}
}
