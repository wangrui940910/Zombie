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
	public AudioSource _feng = null;
	public void light(float v){
		_light.color = _from * (1f - v) + _to * v;
	}

	Task listTask(){
		TweenTask tt = new TweenTask (delegate() {
			return TweenValue.Begin(this.gameObject, 1.0f, 0f,1f,this.gameObject,"light");	
		});
		return tt;
	}
	public Task lost(){
		TaskSet ta = new TaskSet();
		Task attack =_body.action ("die2", false);
		ta.push (attack);
		TaskWait asound = new TaskWait (1.0f);


		TaskManager.PushBack (asound, delegate {

			SoundEffect.instance.PlayLoseSound();
		});
		ta.push (asound);
		return ta;
	}
	public Task lostTask(){
		TaskList tl = new TaskList ();
		TaskSet ts = new TaskSet ();
		Task die = _body.action ("die2", false);
		TaskManager.PushFront (tl, delegate() {
			_feng.Play();	
		});
		ts.push (lost());
		ts.push (listTask());
		tl.push (ts);
		tl.push (_camera.lost());
		Task shit = _shit.shiting ();
		TaskManager.PushFront (shit, delegate() {
			SoundEffect.instance.PlayButtSound();
		});
		tl.push (shit);
		TaskManager.PushBack (tl, delegate {
			_light.color = _from;
			_shit.reset();
			_camera.reset();
			_body.reset();
		});
		//tl.push (_camera.lost());

		//tl.push()
		return tl;
	
	}
}
