using UnityEngine;
using System.Collections;

public class UMJDemo_BulletDestroyer : MonoBehaviour {
	
	private float LIfeTime = 0f;	
			
	// Update is called once per frame
	void Update () 
	{
		LIfeTime += Time.deltaTime;
		if ( LIfeTime > 1.2f ) Destroy( gameObject );
	}
}
