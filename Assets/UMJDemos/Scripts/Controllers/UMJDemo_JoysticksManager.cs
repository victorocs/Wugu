/********************************************************
 * 														*
 * ScriptName:   UMJDemo_JoysticksManager.cs			*
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

[ExecuteInEditMode]
public class UMJDemo_JoysticksManager : MonoBehaviour {	
	
	#region Joystic_Parameters Vars	
	public bool DV_SETUP_MODE = false;
	public bool UseMouse = false;	
	private UMJDemo_Joystick[] JoyList = null;
	#endregion
	
	#region Awake
	void Awake () 
	{
		DV_SETUP_MODE = false;
		
		JoyManagerSetup();
	}
	#endregion
	
	#region JoyManagerSetup
	private void JoyManagerSetup()
	{
		GameObject[] TemJoyList = GameObject.FindGameObjectsWithTag( "GameController" );
		
		JoyList = new UMJDemo_Joystick[ TemJoyList.Length ];
				
		for ( int cnt = 0; cnt < TemJoyList.Length; cnt++ )	JoyList[ cnt ] = TemJoyList[ cnt ].GetComponent<UMJDemo_Joystick>();
		for ( int cnt = 0; cnt < JoyList.Length; cnt++ ) JoyList[ cnt ].JoystickAwake() ;
	}
	#endregion
	
	#region Update
	void Update () 
	{
		if ( DV_SETUP_MODE )
		{
			JoyManagerSetup();
			
			for ( int cnt = 0; cnt < JoyList.Length; cnt++ )
			{								
				JoyList[ cnt ].CalculationSizeAndPosition( Screen.width, DV_SETUP_MODE ) ;				
			}
		}
		
		if ( UseMouse )
		{
			for ( int dnt = 0; dnt < JoyList.Length; dnt++ )
			{								
				JoyMouseManagment( JoyList[ dnt ] );				
			}
		}
		else
		{
			for ( int cnt = 0; cnt < Input.touchCount; cnt++ )
			{
				for ( int dnt = 0; dnt < JoyList.Length; dnt++ )
				{					
					JoyTouchManagment( Input.GetTouch( cnt ), JoyList[ dnt ] );
				}
			}
		}
	}
	#endregion
	
	#region JoyTouchManagment
	private void JoyTouchManagment( Touch touch, UMJDemo_Joystick Joystick )
	{
		switch ( touch.phase ) 
		{
			case TouchPhase.Began:
			
				if ( Joystick.CheckPosition ( touch.position.x, touch.position.y ) && !Joystick.TouchDown )
				{					
					Joystick.TouchID = touch.fingerId;	
					Joystick.TouchDown = true;
					Joystick.GetDefaultPosition( touch.position.x, touch.position.y );
				}
			
			break;
			
			case TouchPhase.Stationary:
			case TouchPhase.Moved:
			
				if ( Joystick.TouchID == touch.fingerId && Joystick.TouchDown )
				{
					Joystick.GetCurrentPosition( touch.position.x, touch.position.y );
				}				
			
			break;
			
			case TouchPhase.Ended:
			case TouchPhase.Canceled:
			
				if ( Joystick.TouchID == touch.fingerId )
				{
					Joystick.ResetJoystickPosition();
				}
			
			break;
		}
	}
	#endregion
	
	#region JoyMouseManagment
	private void JoyMouseManagment( UMJDemo_Joystick Joystick )
	{
		if ( Joystick.CheckPosition ( Input.mousePosition.x, Input.mousePosition.y ) && Input.GetMouseButtonDown( 0 ) )
		{
			Joystick.TouchDown = true;			
			Joystick.GetDefaultPosition ( Input.mousePosition.x, Input.mousePosition.y );			
		}
		
		if ( Joystick.TouchDown && Input.GetMouseButton( 0 ) ) Joystick.GetCurrentPosition ( Input.mousePosition.x, Input.mousePosition.y );
		
		if ( Input.GetMouseButtonUp( 0 ) ) Joystick.ResetJoystickPosition();
	}
	#endregion
}
