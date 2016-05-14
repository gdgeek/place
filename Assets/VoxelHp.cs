using UnityEngine;
using System.Collections;

public class VoxelHp : MonoBehaviour {
	public GameObject[] _hps;
	// Use this for initialization
	public void setValue(int hp){

		for (int i = 0; i<_hps.Length; ++i) {
			_hps [i].SetActive (false);
		}
		for (int i = 0; i < hp && i<_hps.Length; ++i) {
			_hps [i].SetActive (true);
		}
	}
}
