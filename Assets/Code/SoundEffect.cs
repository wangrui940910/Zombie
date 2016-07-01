using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour {

    public AudioClip VictorySound;
    public AudioClip LoseSound;
    public AudioClip ButtSound;
    public AudioClip AttackSound;
    public AudioClip HitSound;
	public AudioClip Small;
	public AudioClip Music;
	public AudioClip Boxing;
    public static SoundEffect instance;

    void Awake() {
        if (instance != null)
            Debug.Log("instance");
        instance = this;
    }
    void MakeSound(AudioClip a) {
        
		AudioSource.PlayClipAtPoint(a, transform.position);
    }

	public void BoxingVictorySound() {
		MakeSound(Boxing);
	}
	public void SmallVictorySound() {
		MakeSound(Small);
	}
	public void MusicVictorySound() {
		MakeSound(Music);
	}

    public void PlayVictorySound() {
        MakeSound(VictorySound);
    }
    public void PlayLoseSound()
    {
        MakeSound(LoseSound);
    }
    public void PlayButtSound()
    {
        MakeSound(ButtSound);
    }
	public void PlayAttackSound(float v)
	{
		AudioSource.PlayClipAtPoint(AttackSound, transform.position,v );
     
    }
    public void PlayHitSound()
    {
        MakeSound(HitSound);
    }
 
}
