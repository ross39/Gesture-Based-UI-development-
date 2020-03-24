using UnityEngine;
using System.Collections;
using Thalmic.Myo;
using LockingPolicy = Thalmic.Myo.LockingPolicy;
using UnlockType = Thalmic.Myo.UnlockType;
using Pose = Thalmic.Myo.Pose;
using VibrationType = Thalmic.Myo.VibrationType;


public class PlayerController : MonoBehaviour {

	public ThalmicMyo thalmicMyo;
	public GameObject myo = null;
	private Pose _lastPose = Pose.Unknown;
	public GameObject laser;
	public float projectileSpeed = 10;
	public float projectileRepeatRate = 0.2f;
	
	public float speed = 15.0f;
	public float padding = 1;
	public float health = 200;
	
	public AudioClip fireSound;

	private float xmax = -5;
	private float xmin = 5;
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0) {
				Die();
			}
		}
	}
	
	void Die(){
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win Screen");
		Destroy(gameObject);
	}
	
	void Start(){
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		xmin = camera.ViewportToWorldPoint(new UnityEngine.Vector3(0,0,distance)).x + padding;
		xmax = camera.ViewportToWorldPoint(new UnityEngine.Vector3(1,1,distance)).x - padding;
	}

	void Fire(){
		GameObject beam = Instantiate(laser, transform.position, UnityEngine.Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new UnityEngine.Vector3(0, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	void Update () {
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

// Need to set up the poses to work with the current keyboard inputs.
// Have a settings menu where the user can chose between the twO?

		if(thalmicMyo.pose == Pose.Fist){
			thalmicMyo.Vibrate(VibrationType.Short);
			InvokeRepeating("Fire", 0.0001f, projectileRepeatRate);
		}
		//if(Input.GetKeyDown(KeyCode.Space)){
		//InvokeRepeating("Fire", 0.0001f, projectileRepeatRate);
		//}
		//if(Input.GetKeyUp(KeyCode.Space)){
		//CancelInvoke("Fire");
		//}
//Waving left
		if(thalmicMyo.pose == Pose.WaveIn){
			thalmicMyo.Vibrate(VibrationType.Short);
			transform.position = new UnityEngine.Vector3(
				Mathf.Clamp(transform.position.x - speed * Time.deltaTime, xmin, xmax),
				transform.position.y, 
				transform.position.z 
			);
//Waving right
		}else if (thalmicMyo.pose == Pose.WaveOut){
			thalmicMyo.Vibrate(VibrationType.Short);
			transform.position = new UnityEngine.Vector3(
				Mathf.Clamp(transform.position.x + speed * Time.deltaTime, xmin, xmax),
				transform.position.y, 
				transform.position.z 
			);
		}
	}
}
