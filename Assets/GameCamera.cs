using UnityEngine;
using System.Collections;
using GDGeek;

public class GameCamera : MonoBehaviour {
	public GameObject _begin = null;
	public GameObject _lost = null;
	public GameObject _win = null;
	public GameObject _we = null;
	public GameObject _shit;
	public GameObject _room;

	public void lostrun(float v){
		
		this.gameObject.transform.position = _begin.transform.position * (1f - v) + _lost.transform.position * v;
	}

	public void winrun(float v){

		this.gameObject.transform.position = _begin.transform.position * (1f - v) + _win.transform.position * v;
	}

	public Task win(){

		TweenTask tt = new TweenTask (delegate {
			return TweenValue.Begin(this.gameObject,1.0f, 0, 1,this.gameObject,"winrun");
		});
		TaskManager.PushFront (tt, delegate() {

			_room.SetActive (false);
		});
		TaskManager.AddUpdate (tt, delegate(float d) {
			this.transform.LookAt (_we.transform.position+ new  Vector3(0, 20, 0));
		});
		return tt;
	}
	public Task lost(){
		
		TweenTask tt = new TweenTask (delegate {
			return TweenValue.Begin(this.gameObject,1.0f, 0, 1,this.gameObject,"lostrun");
		});
		TaskManager.AddUpdate (tt, delegate(float d) {
			this.transform.LookAt (_shit.transform.position);
		});
		return tt;
	}
	public void reset(){
		_room.SetActive (true);
	//	this.transform.LookAt (_we.transform.position);
		this.transform.localEulerAngles = Vector3.zero;
		lostrun (0);
	}
}
