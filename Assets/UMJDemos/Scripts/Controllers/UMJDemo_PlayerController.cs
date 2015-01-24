/********************************************************
 * 														*
 * ScriptName:   UMJDemo_PlayerController.cs			*
 * 														*
 * Copyright(c): Victor Klepikov						*
 * Support: 	 ialucard4@gmail.com					*
 * 														*
 * MyAssets:     http://goo.gl/8ncIsT					*
 * MyTwitter:	 http://twitter.com/VictorKlepikov		*
 * MyFacebook:	 http://www.facebook.com/vikle4 		*
 * 														*
 ********************************************************/


using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class UMJDemo_PlayerController : MonoBehaviour {
	
	#region Move_Controller_Parameters Vars
	public UMJDemo_Joystick MoveJoystick;
	public UMJDemo_Joystick RotateJoystick;
	public UMJDemo_Button ShootButton;
	
	public float CameraSmooth = 0.075f;
	public float MoveSpeed = 15f;
	public float RotateSpeed = 15f;	
	public float MinTForceToMove = 10f;
	public float MinTForceToRotate = 10f;
	public float MinTForceToShoot = 25f;
	
	public UMJDemo_Weapon MainWeapon;
	private CharacterController PlayerController = null;
	private Transform PlayerTransform = null;
	private Vector3 Movement;
	#endregion	
	
	#region Camera_Parameters Vars
	public bool FPSCamera = false;
	public bool MultiJoyCam = false;
	public float CameraBiasFactor = 3.35f;	
	private Transform CameraTransform = null;	
	#endregion	
	
	#region Awake
	void Awake () 
	{	
		PlayerTransform = transform;
		PlayerController = GetComponent<CharacterController>();				
		CameraTransform = Camera.main.transform;
	}
	#endregion	
	
	#region Update
	void Update () 
	{	
		if ( MoveJoystick.JSK_TouchForce > MinTForceToMove ) PlayerMovement( MoveJoystick.JSK_DirectionNormalized.y, MoveJoystick.JSK_DirectionNormalized.x, MoveJoystick.JSK_TouchForce );
		else PlayerController.Move( Physics.gravity * Time.deltaTime * 3f );				
		
		if ( RotateJoystick.JSK_TouchForce > MinTForceToRotate ) PlayerRotation( RotateJoystick.JSK_Direction.y, RotateJoystick.JSK_Direction.x, RotateJoystick.JSK_TouchForce );
		if ( !FPSCamera && RotateJoystick.JSK_TouchForce > MinTForceToShoot || FPSCamera && ShootButton.Pressed ) MainWeapon.Shooting();
				
		if ( !MultiJoyCam )CameraMovement ();
		if ( FPSCamera ) CamRot_FPS();
	}
	#endregion
	
	#region PlayerMovement
	private void PlayerMovement( float MoveFactor_X, float MoveFactor_Y, float MoveStrength )
	{		
		if ( FPSCamera )
		{						
			Movement += ( PlayerTransform.forward * MoveFactor_X ) * MoveSpeed * MoveStrength / 100f;
			Movement += ( PlayerTransform.right * MoveFactor_Y ) * MoveSpeed * MoveStrength / 100f;
		}
		else
		{
			Movement = new Vector3 ( -MoveFactor_X, 0, MoveFactor_Y ) * MoveSpeed * MoveStrength / 100f;				
		}	
		
		Movement *= Time.deltaTime;
		Movement += Physics.gravity * Time.deltaTime * 3f;
		PlayerController.Move( Movement );
		if ( RotateJoystick.JSK_TouchForce < MinTForceToMove ) PlayerRotation( MoveJoystick.JSK_Direction.y, MoveJoystick.JSK_Direction.x, MoveStrength );		
	}
	#endregion
	
	#region PlayerRotation
	private void PlayerRotation( float RotateFactor_Y, float RotateFactor_X, float RotateStrength )
	{
		Vector3 TargetDirection = new Vector3 ( -RotateFactor_Y, 0f, RotateFactor_X );
		Quaternion TargetRotation = Quaternion.LookRotation ( TargetDirection, Vector3.up );
		Quaternion NewRotation = Quaternion.Lerp ( PlayerTransform.rotation, TargetRotation, ( RotateSpeed / 100f * RotateStrength ) * Time.deltaTime );		
		if ( !FPSCamera ) PlayerTransform.rotation = NewRotation;		
	}
	#endregion	
	
	float PositionX = 0f;
	float PositionY = 0f;
	#region CameraMovement
	private void CameraMovement()
	{	
		if (!FPSCamera ) { PositionX = 7.8f; PositionY = 11.5f; }
		else PositionY  = 0.8f;
		
		Vector3 CameraVelocity = Vector3.zero;
		Vector3 CameraTargenPosition = new Vector3 ( PlayerTransform.position.x + PositionX, PlayerTransform.position.y + PositionY, PlayerTransform.position.z );
		Vector3 NewCameraTargenDirection = new Vector3 ( ( -RotateJoystick.JSK_DirectionNormalized.y * RotateJoystick.JSK_TouchForce / 100f ) * CameraBiasFactor, 
											 0f, (  RotateJoystick.JSK_DirectionNormalized.x * RotateJoystick.JSK_TouchForce / 100f ) * CameraBiasFactor );		
						
		if ( !FPSCamera ) CameraTargenPosition += NewCameraTargenDirection;		
		CameraTransform.position = Vector3.SmoothDamp( CameraTransform.position, CameraTargenPosition, ref CameraVelocity, CameraSmooth );
	}
	#endregion
	
	float RotationX = 0f;
	float RotationY = 0f;
	private void CamRot_FPS()
	{
//		if ( RotateJoystick.JSK_Touch.phase != TouchPhase.Stationary )
//		{		
			RotationX += Mathf.Clamp ( RotateJoystick.JSK_DirectionNormalized.x * RotateJoystick.JSK_TouchForce / 100f, -1, 1 );
			RotationY += Mathf.Clamp ( RotateJoystick.JSK_DirectionNormalized.y * RotateJoystick.JSK_TouchForce / 100f, -1, 1 );		
		
			Quaternion FPS_Rotation = Quaternion.Euler( -RotationY * 5f, RotationX * 5f, 0f );		
			CameraTransform.rotation = FPS_Rotation;
			PlayerTransform.rotation = FPS_Rotation;
//		}
	}
}