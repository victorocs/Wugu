/********************************************************
 * 														*
 * ScriptName:   UMJDemo_Button.cs						*
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

[RequireComponent (typeof(GUITexture))]

[ExecuteInEditMode]
public class UMJDemo_Button : MonoBehaviour {
	
	#region Button_Parameters Vars
	public bool DV_SETUP_MODE = false;
	
	private GUITexture Button = null;
	
	public float ButtonOffsetX = 10f;
	public float ButtonOffsetY = 10f;
	
	public float ButtonUpSize = 5f;
	public float ButtonDownSize = 4.5f;	

	public int TouchID = 0;	
	
	public bool Pressed = false;
	public bool Clicked = false;
	private bool ClickReady = true;
		
	private Touch BTN_Touch;
	
	private float BTN_PositionX = 0f;
	private float BTN_PositionY = 0f;
	
	private float BtnUpSize = 0f;
	private float BtnDwnSize = 0f;
	
	public bool UseMouse = false;
	
	public Texture2D ButtonTexture = null;
	#endregion
	
	#region Awake
	void Awake () 
	{
		ButtonSetup();
		CalculationSizeAndPosition( Screen.width );				
	}
	#endregion
	
	#region ButtonSetup
	private void ButtonSetup()
	{
		Button = guiTexture;
		Button.texture = ButtonTexture;
	}
	#endregion
	
	#region CalculationSizeAndPosition
	private void CalculationSizeAndPosition( float ScreenWidth )
	{
		BtnUpSize = ButtonUpSize / 50f * ScreenWidth;
		BtnDwnSize = ButtonDownSize / 50f * ScreenWidth;
				
		BTN_PositionX = ( ( ScreenWidth / 10f ) - ( BtnUpSize / 5f ) ) + ( ButtonOffsetX * ScreenWidth / 50f );
		BTN_PositionY = ( ( ScreenWidth / 10f ) - ( BtnUpSize / 5f ) ) + ( ButtonOffsetY * ScreenWidth / 50f );
		
		Button.pixelInset = new Rect( BTN_PositionX, BTN_PositionY, BtnUpSize, BtnUpSize );
	}
	#endregion
	
	#region Update
	void Update () 
	{
		if ( DV_SETUP_MODE )
		{
			ButtonSetup();
			CalculationSizeAndPosition( Screen.width );
		}
		GetTouchID( Input.touchCount );
	}
	#endregion
	
	#region GetTouchID
	private void GetTouchID( int touchCount )
	{
		if ( UseMouse ) 
		{
			if ( Input.GetMouseButton( 0 ) )
			{
				if ( GetTap( Input.mousePosition.x, Input.mousePosition.y ) )
				{
					ButtonDown();
				}
			}
			else ButtonUp();
		}
		else
		{
			for ( int cnt = 0; cnt < touchCount; cnt++ )
			{
				if ( !Pressed ) TouchID = cnt;			
				if ( TouchID < touchCount ) BTN_Touch = Input.GetTouch ( TouchID );
				if ( TouchID >= touchCount ) BTN_Touch = Input.GetTouch ( touchCount - 1 );
			}
			
			if ( GetTap( BTN_Touch.position.x, BTN_Touch.position.y ) )
			{
				ButtonDown();
			}
			
			if ( BTN_Touch.phase == TouchPhase.Ended || BTN_Touch.phase == TouchPhase.Canceled ) ButtonUp();
		}
	}
	#endregion
	
	#region GetTap
	private bool GetTap( float posX, float posY )
	{
		if ( BTN_Touch.phase == TouchPhase.Began )
		{			
			if ( posX > BTN_PositionX 
			&& posY > BTN_PositionY 
			&& posX < BTN_PositionX + BtnUpSize 
			&& posY < BTN_PositionY + BtnUpSize )
			{			
				if ( UseMouse )
				{
					if ( Input.GetMouseButtonDown( 0 ) ) return true;
					else return false;				
				}
				else
				{
					return true;
				}				
			}	
			else return false;
		}
		else return false;		
	}
	#endregion
	
	#region ButtonDown
	private void ButtonDown()
	{
		Pressed = true;
		
		if ( ClickReady )
		{
			Clicked = true;			
			ClickReady = false;
		}
		else
		{
			Clicked = false;
		}
		
		Button.pixelInset = new Rect( BTN_PositionX, BTN_PositionY, BtnDwnSize, BtnDwnSize );
	}
	#endregion
	
	#region ButtonUp
	private void ButtonUp()
	{
		Pressed = false;
		ClickReady = true;
		TouchID = 0;
		Button.pixelInset = new Rect( BTN_PositionX, BTN_PositionY, BtnUpSize, BtnUpSize );
	}
	#endregion
}
