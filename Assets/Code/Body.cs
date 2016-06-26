using UnityEngine;
using System.Collections;
using GDGeek;

public class Body : MonoBehaviour {

	public GameObject _fromPoint = null;
	public GameObject _attackPoint = null;
	public Animator _animator = null;

	public FSM fsm_ = new FSM();  


	public void post(string action){
		fsm_.post (action);
	}
	private State getIdle(){
		StateWithEventMap swie = new StateWithEventMap ();
		swie.onStart += delegate {

			this._animator.speed = 1;
			this._animator.Play("idle");
		};

		swie.addAction ("attack", delegate(FSMEvent evt) {
			attack_++;
			return  "move";
		});
		swie.addAction ("skill", "skill");

		return swie;

	}
	/*private State getCameraAttack(){
		StateWithEventMap swie = new StateWithEventMap ();
		swie.onStart += delegate {
			TaskManager.Run(cameraTask());
		};
		return swie;
	}*/


	private State getAttack(){
		StateWithEventMap swie = TaskState.Create (delegate() {
			TaskList tl = new TaskList();
			TaskWait tw = new TaskWait(0.01f);
			TaskManager.PushFront(tw, delegate() {
				this._animator.Play("attack");
			});
			Task task = new Task();
			TaskManager.PushFront(task, delegate() {
				
			});
			task.isOver = delegate() {
				var info = _animator.GetCurrentAnimatorStateInfo(0);
		
				if(!info.IsName("attack")){
					return true;
				}else{
					return false;
				}
			};
			task.shutdown = delegate() {
				attack_-- ;	
			};
			tl.push(tw);
			tl.push(task);
			tl.push(resetTask());
			return tl;
		}, fsm_, delegate(FSMEvent evt) {
			if(this.attack_ != 0){
				return "attack";
			}else{
				return "idle";
			}
		});
		swie.addAction ("attack", delegate(FSMEvent evt) {
			attack_ ++;
			//_animator.speed = 2+(attack_-1)*10;
		});

		return swie;

	}
	public Task action(string name, bool idle = true){
		TaskList tl = new TaskList();
		TaskWait tw = new TaskWait(0.01f);
		TaskManager.PushFront(tw, delegate() {
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
				this._animator.Play("idle");
			}
			Debug.Log ("over " + name);
		});
		return tl;  
	
	}
	private State getSkill(){
		StateWithEventMap swie = TaskState.Create (delegate() {
			TaskList tl = new TaskList();
			TaskWait tw = new TaskWait(0.01f);
			TaskManager.PushFront(tw, delegate() {

				this._animator.Play("skill");
			});
			Task task = new Task();

			task.isOver = delegate() {
				var info = _animator.GetCurrentAnimatorStateInfo(0);

				if(!info.IsName("skill")){
					return true;
				}else{
					return false;
				}
			};

			tl.push(tw);
			tl.push(task);
			tl.push(resetTask());
			return tl;
		}, fsm_, delegate(FSMEvent evt) {

			return "idle";

		});
		return swie;

	}

	private int attack_ = 0;
	public Task resetTask(){
		TweenTask tt = new TweenTask(delegate() {
			return TweenLocalPosition.Begin(this.gameObject, 0.01f, Vector3.zero);	
		});

		return tt;
	}
	public void reset(){
		this.gameObject.transform.position = _fromPoint.transform.position;
		this._animator.Play ("idle");

	}
	public  Task moveTask(float v){
		TweenTask tt = new TweenTask(delegate() {
			return TweenWorldPosition.Begin(this.gameObject, 0.1f, _fromPoint.transform.position * (1f-v) + _attackPoint.transform.position*v);	
		});

		return tt;
	}
	private State getMove(){
		StateWithEventMap state = TaskState.Create (delegate {
			return moveTask(1f);
		}, this.fsm_, "attack");

		return state;
	} 

	private Vector3 position_ = Vector3.zero;

	void Start () {


		//fsm_.addState ("idle", getIdle ());
		//fsm_.addState ("move", getMove());

		//fsm_.addState ("camera_attack", getCameraAttack ());
	//	fsm_.addState ("attack", getAttack ());
	//	fsm_.addState ("skill", getSkill ());
	//	fsm_.addState ("hit", getHit ());
			
	
	//	fsm_.init ("idle");

	}


}
