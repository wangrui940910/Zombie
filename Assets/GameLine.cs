using UnityEngine;
using System.Collections;
using GDGeek;

public class GameLine : MonoBehaviour {
	public GameObject _x;
	public GameObject _y;
	// Use this for initialization
	void Start () {
		GameObject man = null;
		for (int i = 0; i < 50; ++i) {
			if (Random.Range (0, 100) % 2 == 0) {
				man = GameObject.Instantiate(_x);
				man.transform.parent = this.transform;
				man.transform.localPosition = _x.transform.localPosition;
				man.transform.localScale = _x.transform.localScale;
			} else {
				man = GameObject.Instantiate (_y);
				man.transform.parent = this.transform;
				man.transform.localPosition = _y.transform.localPosition;
				man.transform.localScale = _y.transform.localScale;
			}

			var p = man.transform.localPosition;
			p.z =  - (i + 1f) * 0.5f;
			man.transform.localPosition = p;
			man.SetActive (true);
		}
	
	}
	public Task inTask(){
		TweenTask tt = new TweenTask (delegate() {
			return TweenLocalPosition.Begin(this.gameObject, 0.3f, this.transform.localPosition + new Vector3(0,0, 1f));
		});

		return tt;
	}
}
