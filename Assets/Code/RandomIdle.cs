using UnityEngine;
using System.Collections;

public class RandomIdle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Animator anim = this.GetComponent<Animator> ();
		anim.Play ("idle_" + Random.Range (0, 7));
		anim.speed = Random.Range (0.1f, 2f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
