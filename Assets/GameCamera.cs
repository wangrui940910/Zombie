using UnityEngine;
using System.Collections;
using GDGeek;

public class GameCamera : MonoBehaviour {
	public GameObject _begin = null;
	public GameObject _lost = null;
	public void lostrun(float v){
		this.gameObject.transform.position = _begin.transform.position * (1f - v) + _lost.transform.position * v;
		this.gameObject.transform.eulerAngles = _begin.transform.eulerAngles * (1f - v) + _lost.transform.eulerAngles * v;
	}
	public Task lost(){
		TweenTask tt = new TweenTask (delegate {
			Debug.Log("!!!!!");
			return TweenValue.Begin(this.gameObject,1.0f,0,1,this.gameObject,"lostrun");
		});
		return tt;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
