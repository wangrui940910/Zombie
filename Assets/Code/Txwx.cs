using UnityEngine;
using System.Collections;
using GDGeek;

public class Txwx : MonoBehaviour {
	public GameObject _tian;
	public GameObject _xia;
	public GameObject _wu;
	public GameObject _xiang;
	public Task show(){
		TaskList tl = new TaskList ();
		TaskWait tian = new TaskWait (0.5f);
		TaskManager.PushFront (tian, delegate() {
			_tian.SetActive(true);
			SoundEffect.instance.PlayAttackSound(1);
		});

		TaskWait xia = new TaskWait (0.5f);
		TaskManager.PushFront (xia, delegate() {
			SoundEffect.instance.PlayAttackSound(1);
			_xia.SetActive(true);
		});


		TaskWait wu = new TaskWait (0.5f);
		TaskManager.PushFront (wu, delegate() {
			SoundEffect.instance.PlayAttackSound(1);
			_wu.SetActive(true);
		});


		TaskWait xiang = new TaskWait (0.5f);
		TaskManager.PushFront (xiang, delegate() {
			SoundEffect.instance.PlayAttackSound(1);
			_xiang.SetActive(true);
		});

		tl.push (tian);
		tl.push (xia);
		tl.push (wu);
		tl.push (xiang);
		return tl;

	}
	public void hide(){
		
		_tian.SetActive (false);
		_xia.SetActive (false);
		_wu.SetActive (false);
		_xiang.SetActive (false);

	}
}
