using UnityEngine;
using System.Collections;

public class UMJDemo_ModeChanger : MonoBehaviour {
	
	public bool MainMenuLevel = false;
	
	private GUIText AProj = null;
	private GUIText ADev = null;
	
	private string CurrentTitle = "Switch To Dynamic Mode";
	private bool StatickMode = true;
	
	public UMJDemo_Joystick LeftJoy;
	public UMJDemo_Joystick RightJoy;
	
	void Awake()
	{
		AProj = transform.FindChild( "AProj" ).GetComponent<GUIText>();
		ADev = transform.FindChild( "ADev" ).GetComponent<GUIText>();
		
		float CASize = 2.2f / 50f * Screen.width;		
		float SizePixelX = Screen.width / 100f;
		float SizePixelY = Screen.height / 100f;
		float SizeTransformX = transform.position.x * 100f;
		float SizeTransformY = transform.position.y * 100f;		
		Vector2 CA_Position = new Vector2( SizePixelX * SizeTransformX - CASize / 10f, SizePixelY * SizeTransformY - CASize / 10f );		
		
		ADev.pixelOffset = new Vector2( CA_Position.x, CA_Position.y );
		AProj.pixelOffset = new Vector2( CA_Position.x, CA_Position.y );		
		
		int FontSize = (int)( CASize / 1.4f );
		
		AProj.fontSize = FontSize;
		ADev.fontSize = FontSize;
		
		transform.position = Vector3.zero;
	}
	
	void OnGUI()
	{
		if ( Application.loadedLevelName == "CameraTop" )
		if (GUI.Button(new Rect(Screen.width - Screen.width / 3.5f, 10f, Screen.width / 4f, Screen.width / 12f), CurrentTitle))
		{
			StatickMode = !StatickMode;
			LeftJoy.Statick = !LeftJoy.Statick;
			RightJoy.Statick = !RightJoy.Statick;
			
			if ( StatickMode )
			{				
				CurrentTitle = "Switch To Dynamic Mode";
				
				LeftJoy.Joystick.enabled = true;
				LeftJoy.JoystickBackground.enabled = true;
				
				RightJoy.Joystick.enabled = true;
				RightJoy.JoystickBackground.enabled = true;
			}
			else CurrentTitle = "Switch To Statick Mode";
		}
		
		//if (GUI.Button(new Rect(Screen.width - Screen.width / 3.5f, Screen.width / 9f, Screen.width / 4f, Screen.width / 12f), "Change Level"))
		if ( MainMenuLevel )
		{
			if (GUI.Button(new Rect(Screen.width / 2.8f, Screen.height / 3.5f, Screen.width / 2.5f, Screen.width / 12f), "MultiJoy_Demo"))
			{
				Application.LoadLevel( "MultiJoy" );		
			}
			
			if (GUI.Button(new Rect(Screen.width / 2.8f, Screen.height / 2.1f, Screen.width / 2.5f, Screen.width / 12f), "CameraTop_Demo"))
			{
				Application.LoadLevel( "CameraTop" );		
			}
			
			if (GUI.Button(new Rect(Screen.width / 2.8f, Screen.height / 1.5f, Screen.width / 2.5f, Screen.width / 12f), "CameraFPS_Demo"))
			{
				Application.LoadLevel( "CameraFPS" );		
			}
		}
		else
		{
			if (GUI.Button(new Rect(Screen.width / 2.8f, Screen.width / 2f, Screen.width / 4f, Screen.width / 12f), "Goto MainMenu"))
			{
				Application.LoadLevel( "MainMenu" );			
			}
		}
	}	
}