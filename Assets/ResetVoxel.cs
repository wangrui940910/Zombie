using UnityEngine;
using System.Collections;
using GDGeek;


[ExecuteInEditMode]
public class ResetVoxel : MonoBehaviour {
	public bool _reset = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (_reset) {
			
			_reset = false;
			VoxelMesh[] meshs = this.gameObject.GetComponentsInChildren<VoxelMesh> ();
			for (int i = 0; i < meshs.Length; ++i) {
				GameObject.DestroyImmediate (meshs[i].filter.gameObject);
				GameObject.DestroyImmediate (meshs[i]);
			}

			VoxelMaker[] makers = this.gameObject.GetComponentsInChildren<VoxelMaker> ();

			for (int i = 0; i < makers.Length; ++i) {
				makers [i]._building = true;
			}

		}
	
	}
}
