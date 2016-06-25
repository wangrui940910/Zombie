using UnityEngine;
using System.Collections;
using GDGeek;

public class GameLost : MonoBehaviour {
	public Light _light;
	public Color _from;
	public Color _to;
	public Body _body = null;
	public GameCamera _camera;
	public Shit _shit;
	public void light(float v){
		_light.color = _from * (1f - v) + _to * v;
	}
	Task listTask(){
		TweenTask tt = new TweenTask (delegate() {
			return TweenValue.Begin(this.gameObject, 1.0f, 0f,1f,this.gameObject,"light");	
		});
		return tt;
	}
	public Task lostTask(){
		TaskList tl = new TaskList ();
		TaskSet ts = new TaskSet ();
		ts.push (_body.action ("die2", false));
		ts.push (listTask());
		tl.push (ts);
		tl.push (_camera.lost());
		tl.push (_shit.shiting());

		//tl.push()
		return tl;
	
	}
}
