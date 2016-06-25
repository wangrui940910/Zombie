using UnityEngine;
using System.Collections;
using GDGeek;

public class FoeBody : MonoBehaviour {

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
		swie.addAction ("magic", "magic");
		swie.addAction ("attack", delegate(FSMEvent evt) {
			attack_++;
			return  "attack";
		});
		swie.addAction ("skill", "skill");
		swie.addAction ("def", "def");
		return swie;

	}
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
			_animator.speed = 2+(attack_-1)*10;
		});

		return swie;

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
	private State getMagic(){
		StateWithEventMap swie = TaskState.Create (delegate() {
			TaskList tl = new TaskList();
			TaskWait tw = new TaskWait(0.01f);
			TaskManager.PushFront(tw, delegate() {

				this._animator.Play("magic");
			});
			Task task = new Task();

			task.isOver = delegate() {
				var info = _animator.GetCurrentAnimatorStateInfo(0);

				if(!info.IsName("magic")){
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

	private State getPower(){
		StateWithEventMap swie = new StateWithEventMap ();
		swie.onStart += delegate {
			//	_body.power();
		};
		return swie;
	}

	private State getDef(){
		StateWithEventMap swie = TaskState.Create (delegate() {
			TaskList tl = new TaskList();
			TaskWait tw = new TaskWait(0.01f);
			TaskManager.PushFront(tw, delegate() {
				_animator.Play("def");
			});
			Task task = new Task();

			task.isOver = delegate() {
				var info = _animator.GetCurrentAnimatorStateInfo(0);
				if(info.normalizedTime > info.length){
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
	private Task resetTask(){
		TweenTask tt = new TweenTask(delegate() {
			return TweenLocalPosition.Begin(this.gameObject, 0.01f, Vector3.zero);	
		});

		return tt;
	}

	private Vector3 position_ = Vector3.zero;
	private State getHit(){
		StateWithEventMap swie = TaskState.Create (delegate() {
			TaskList tl = new TaskList();
			TaskWait tw = new TaskWait(0.01f);
			TaskManager.PushFront(tw, delegate() {
				_animator.Play("def");
			});
			Task task = new Task();

			task.isOver = delegate() {
				var info = _animator.GetCurrentAnimatorStateInfo(0);
				if(info.normalizedTime > info.length){
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
	void Start () {


		fsm_.addState ("idle", getIdle ());
		fsm_.addState ("attack", getAttack ());
		fsm_.addState ("skill", getSkill ());
		//	fsm_.addState ("hit", getHit ());

		fsm_.addState ("magic", getMagic());
		fsm_.addState ("power", getPower());
		fsm_.addState ("def", getDef());

		fsm_.init ("idle");

	}


}
