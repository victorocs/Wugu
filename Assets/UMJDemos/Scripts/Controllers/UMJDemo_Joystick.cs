/********************************************************
 * 														*
 * ScriptName:   UMJDemo_Joystick.cs					*
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

public class UMJDemo_Joystick : MonoBehaviour {
		
	#region Joystic_Parameters Vars	
	public bool Statick = false;
	public bool TouchPad = false;
	
	public float JoystickOffsetX = 10f;
	public float JoystickOffsetY = 10f;
	public float JoystickSize = 6f;
	
	public float BorderSize = 58.5f;	
	
	public float TouchZoneWidth = 20f;
	public float TouchZoneHeight = 21f;
	private float TouchZoneWidthPart = 0f;
	private float TouchZoneHeightPart = 0f;
	
	public float TouchZoneOffsetX = 100f;
	public float TouchZoneOffsetY = 100f;
	
	private float TouchZonePositionX = 0f;
	private float TouchZonePositionY = 0f;
	
	public GUITexture Joystick = null;
	public GUITexture JoystickBackground = null;
	public GUITexture JoystickTouchZone = null;
	
	public Vector2 JSK_DefaultPosition = Vector2.zero;
	public Vector2 JSK_CurrentPosition = Vector2.zero;	
	public Vector2 JSK_Direction = Vector2.zero;	
	public Vector2 JSK_DirectionNormalized = Vector2.zero;
	public Vector2 JSK_BorderPosition = Vector2.zero;
	public float JSK_TouchForce = 0f;
	public float JSK_CurrentDistance = 0f;		

	public int TouchID = -1;	
	
	public bool BorderOutput = false;
	
	public string JoystickTextureGOName = "JoystickTexture";
	public string JoystickBackgroundGOName = "JoystickBackground";
	public string JoystickTouchZoneGOName = "JoystickTouchZone";
	
	public Texture2D JoystickTexture = null;
	public Texture2D BackgroundTexture = null;
	public Texture2D TouchZoneTexture = null;
	
	private float JoySize = 0f;
	private float BgrdSize = 0f;
	private float CalculatedBorderSize = 0f;
	
	public bool TouchDown = false;
	#endregion

	#region JoystickAwake
	public void JoystickAwake() 
	{		
		JoystickSetup();
		
		JoystickTouchZone.enabled = false;
		
		CalculationSizeAndPosition( Screen.width, false );
		
		if ( !Statick ) 
		{ 
			Joystick.enabled = false; 
			JoystickBackground.enabled = false;
		}
	}
	#endregion
	
	#region JoystickSetup
	private void JoystickSetup()
	{
		Joystick = transform.FindChild( JoystickTextureGOName ).guiTexture;
		JoystickBackground = transform.FindChild( JoystickBackgroundGOName ).guiTexture;
		JoystickTouchZone = transform.FindChild( JoystickTouchZoneGOName ).guiTexture;		
		
		Joystick.texture = JoystickTexture;
		JoystickBackground.texture = BackgroundTexture;
		JoystickTouchZone.texture = TouchZoneTexture;
	}
	#endregion
	
	#region CalculationSizeAndPosition
	public void CalculationSizeAndPosition( float ScreenWidth, bool SETUP_MODE )
	{		
		if ( SETUP_MODE )
		{
			JoystickSetup();
			
			Joystick.enabled = true; 
			JoystickBackground.enabled = true;
			if ( !Statick ) JoystickTouchZone.enabled = true;
			else JoystickTouchZone.enabled = false;
		}
						
		JoySize	= JoystickSize / 50f * ScreenWidth;
		BgrdSize = JoySize;
		TouchZoneWidthPart = JoySize * TouchZoneWidth / 10f;
		TouchZoneHeightPart = JoySize * TouchZoneHeight / 10f;			
		CalculatedBorderSize = ( JoySize * BorderSize ) / 50f;		
		
		JSK_DefaultPosition = new Vector2( ( ( ScreenWidth / 10f ) - ( JoySize / 5f ) ) + ( JoystickOffsetX * ScreenWidth / 50f ),			
										   ( ( ScreenWidth / 10f ) - ( JoySize / 5f ) ) + ( JoystickOffsetY * ScreenWidth / 50f ) );
		
		JSK_CurrentPosition = JSK_DefaultPosition;		
		
		TouchZonePositionX = JSK_CurrentPosition.x - ( JoySize * ( TouchZoneOffsetX / 100f ) );
		TouchZonePositionY = JSK_CurrentPosition.y - ( JoySize * ( TouchZoneOffsetY / 100f ) );
		
		JoystickTouchZone.pixelInset = new Rect( TouchZonePositionX, TouchZonePositionY, TouchZoneWidthPart, TouchZoneHeightPart );
		Joystick.pixelInset = new Rect( JSK_CurrentPosition.x, JSK_CurrentPosition.y, JoySize, JoySize );
		JoystickBackground.pixelInset = new Rect( JSK_CurrentPosition.x, JSK_CurrentPosition.y, BgrdSize, BgrdSize );
	}
	#endregion
	
	#region CheckPosition
	public bool CheckPosition( float posX, float posY )
	{
		if ( Statick )
		{
			if ( posX > JSK_DefaultPosition.x 
			&& posY > JSK_DefaultPosition.y 
			&& posX < JSK_DefaultPosition.x + BgrdSize 
			&& posY < JSK_DefaultPosition.y + BgrdSize )
			{
				return true;
			}
			else return false;
		}
		else
		{
			if ( posX > TouchZonePositionX 
			&& posY > TouchZonePositionY 
			&& posX < TouchZonePositionX + TouchZoneWidthPart 
			&& posY < TouchZonePositionY + TouchZoneHeightPart )
			{
				return true;
			}
			else return false;
		}
	}
	#endregion

	#region GetDefaultPosition
	public void GetDefaultPosition( float posX, float posY )
	{
		if ( !TouchPad ) { Joystick.enabled = true; JoystickBackground.enabled = true; }
		
		if ( !Statick ) JSK_DefaultPosition = new Vector2( posX - JoySize / 2f, posY - JoySize / 2f );
		
		Joystick.pixelInset = new Rect( JSK_DefaultPosition.x, JSK_DefaultPosition.y, JoySize, JoySize );
		JoystickBackground.pixelInset = new Rect( JSK_DefaultPosition.x, JSK_DefaultPosition.y, BgrdSize, BgrdSize );
	}
	#endregion
	
	#region GetCurrentPosition
	public void GetCurrentPosition( float posX, float posY )
	{
		JSK_CurrentPosition = new Vector2( posX - JoySize / 2f, posY - JoySize / 2f );
		GetJoystickBorder();
		GetJoystickStrength();
	}
	#endregion
	
	#region GetJoystickBorder
	private void GetJoystickBorder()
	{
		JSK_Direction = JSK_CurrentPosition - JSK_DefaultPosition;		
		JSK_DirectionNormalized = Vector3.Normalize( JSK_Direction );
		JSK_BorderPosition = JSK_DefaultPosition;
		JSK_BorderPosition += JSK_DirectionNormalized * CalculatedBorderSize;		
		JSK_CurrentDistance = Vector2.Distance( JSK_DefaultPosition, JSK_CurrentPosition );
		
		if ( JSK_CurrentDistance > CalculatedBorderSize ) BorderOutput = true;
		else BorderOutput = false;		
		
		if ( BorderOutput ) JSK_CurrentPosition = JSK_BorderPosition;
		Joystick.pixelInset = new Rect( JSK_CurrentPosition.x, JSK_CurrentPosition.y, JoySize, JoySize );		
	}
	#endregion
	
	#region GetJoystickStrength
	private void GetJoystickStrength()
	{
		if ( !BorderOutput ) JSK_TouchForce = ( JSK_CurrentDistance / CalculatedBorderSize ) * 100f;
		else JSK_TouchForce = 100f;		
	}
	#endregion
	
	#region ResetJoystickPosition
	public void ResetJoystickPosition()
	{		
		JSK_CurrentPosition = JSK_DefaultPosition;
		
		Joystick.pixelInset = new Rect( JSK_CurrentPosition.x, JSK_CurrentPosition.y, JoySize, JoySize );
		JoystickBackground.pixelInset = new Rect( JSK_CurrentPosition.x, JSK_CurrentPosition.y, BgrdSize, BgrdSize );
		
		if ( !Statick ) 
		{ 
			Joystick.enabled = false; 
			JoystickBackground.enabled = false; 
		}
		BorderOutput = false;
		TouchDown = false;		
		TouchID = -1;
		JSK_CurrentDistance = 0f;
		JSK_TouchForce = 0f;		
	}
	#endregion
}