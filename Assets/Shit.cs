using UnityEngine;
using System.Collections;
using GDGeek;

public class Shit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public Task shiting(){
		TaskCircle tc = new TaskCircle ();


		Task shit0 = new TaskWait (0.2f);
		TaskManager.PushFront (shit0, delegate {
			this.gameObject.SetActive(true);	
		});


		Task shit1 = new TaskWait (0.2f);
		TaskManager.PushFront (shit1, delegate {
			this.gameObject.SetActive(false);	
		});


		tc.push (shit0);
		tc.push (shit1);
		return tc;

	}
}
