using UnityEngine;
using System.Collections;
using GDGeek;

public class Shit : MonoBehaviour {
	private float time_ = 0.0f;
	private bool isRun_ = false;

	private  TaskCircle tc_ = null;//new TaskCircle ();
	public void reset(){
		this.gameObject.SetActive(false);

	}
	public Task shiting(){
		tc_ = new TaskCircle ();


		Task shit0 = new TaskWait (0.2f);
		TaskManager.PushFront (shit0, delegate {
			this.gameObject.SetActive(true);	
		});


		Task shit1 = new TaskWait (0.2f);
		TaskManager.PushFront (shit1, delegate {
			
			this.gameObject.SetActive(false);	
		});


		tc_.push (shit0);
		tc_.push (shit1);
		TaskManager.PushFront (tc_, delegate {
			isRun_ = true;
			time_ = 0.0f;	
		});
		TaskManager.PushBack (tc_, delegate {
			isRun_ = false;
		});
		return tc_;

	}

	void Update(){
		if (isRun_) {
			time_ += Time.deltaTime;
			if (time_ > 1.0f) {
				if (Input.GetKeyDown (KeyCode.Space)) {

					tc_.forceQuit ();
				}
			}
		}

	}


}
