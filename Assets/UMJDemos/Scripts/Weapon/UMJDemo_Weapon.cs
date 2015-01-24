using UnityEngine;
using System.Collections;

public class UMJDemo_Weapon : MonoBehaviour {	
		
	public GameObject MyBullet = null;		
	public Transform Muzzle = null;	
	private float ReadyTime = 0f;
	
	void Update() { }	
	
	public void Shooting()
	{
		ReadyTime += Time.deltaTime; 
		if ( ReadyTime > 0.15f )
		{						
			GameObject BulletCloneGO = ( GameObject ) Instantiate( MyBullet, Muzzle.position, Muzzle.rotation ) as GameObject;			
			BulletCloneGO.rigidbody.velocity = Muzzle.transform.TransformDirection( Vector3.forward * 100f );
			ReadyTime = 0f;			
		}
	}
}