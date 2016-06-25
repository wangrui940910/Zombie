using UnityEngine;
using System.Collections;

public class GameLamp : MonoBehaviour {
	public GameObject[] _lights;
	public Color _color;
	public void light(int n){
		for (int i = 0; i < n; ++i) {
			Renderer r = _lights [i].GetComponent<Renderer> ();
			r.material.color = _color;	
		}
		for (int i = n; i < _lights.Length; ++i) {

			Renderer r = _lights [i].GetComponent<Renderer> ();
			r.material.color = Color.white;	
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
