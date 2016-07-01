using UnityEngine;
using System.Collections;
using GDGeek;

public class FoeBody : MonoBehaviour {

	public Animator _animator = null;


	public Task action(string name, bool idle = true){
		TaskList tl = new TaskList();
		TaskWait tw = new TaskWait(0.01f);
		TaskManager.PushFront(tw, delegate() {
			_animator.speed = 1.2f;
			this._animator.Play(name);
		});
		Task task = new Task();

		task.isOver = delegate() {
			var info = _animator.GetCurrentAnimatorStateInfo(0);
			//			Debug.Log(info.normalizedTime);
			if(info.normalizedTime> 1){
				return true;
			}else{
				return false;
			}
		};

		tl.push(tw);
		tl.push(task);
		TaskManager.PushBack (tl, delegate {
			if(idle){
				this._animator.Play("shit");
			}
			Debug.Log ("over " + name);
		});
		return tl;  

	}



	void Start () {

		this._animator.Play("shit");


	}


}
