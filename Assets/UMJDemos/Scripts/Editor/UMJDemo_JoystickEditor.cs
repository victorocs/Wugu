/********************************************************
 * 														*
 * ScriptName:   UMJDemo_JoystickEditor.cs				*
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
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(UMJDemo_Joystick))]

public class UMJDemo_JoystickEditor : Editor {
			
	#region MainVars
	private UMJDemo_Joystick MyTarget = null;
	#endregion
	
	#region OnEnable
	public void OnEnable()
	{
		MyTarget = ( UMJDemo_Joystick )target;
	}
	#endregion
	
	#region OnInspectorGUI
	public override void OnInspectorGUI()
	{
		GUILayout.Box( "", "HorizontalSlider", GUILayout.Height( 15 ) );
		
		ShowParameters();
		
		if ( GUI.changed ) EditorUtility.SetDirty( MyTarget );
		
		GUILayout.Box( "", "HorizontalSlider", GUILayout.Height( 15 ) );
	}
	#endregion
	
	#region ShowParameters
	private void ShowParameters()
	{
		MyTarget.Statick = EditorGUILayout.Toggle( "Statick", MyTarget.Statick );
		MyTarget.TouchPad = EditorGUILayout.Toggle( "Touch Pad", MyTarget.TouchPad );
		
		MyTarget.JoystickSize = EditorGUILayout.Slider( "Joystick Size", MyTarget.JoystickSize, 2f, 10f );
		MyTarget.JoystickOffsetX = EditorGUILayout.Slider( "Joystick Offset_X", MyTarget.JoystickOffsetX, -10f, 50f );
		MyTarget.JoystickOffsetY = EditorGUILayout.Slider( "Joystick Offset_Y", MyTarget.JoystickOffsetY, -10f, 50f );
		
		MyTarget.BorderSize = EditorGUILayout.Slider( "Border Size", MyTarget.BorderSize, 10f, 100f );
		
		if ( !MyTarget.Statick )
		{
			MyTarget.TouchZoneWidth = EditorGUILayout.Slider( "TouchZone Width", MyTarget.TouchZoneWidth, 5f, 90f );
			MyTarget.TouchZoneHeight = EditorGUILayout.Slider( "TouchZoneH Height", MyTarget.TouchZoneHeight, 5f, 90f );
			MyTarget.TouchZoneOffsetX = EditorGUILayout.Slider( "TouchZone Offset_X", MyTarget.TouchZoneOffsetX, -50f, 550f );
			MyTarget.TouchZoneOffsetY = EditorGUILayout.Slider( "TouchZone Offset_Y", MyTarget.TouchZoneOffsetY, -50f, 550f );
		}	
		
		MyTarget.JoystickTextureGOName = EditorGUILayout.TextField( "Joystick NameGO", MyTarget.JoystickTextureGOName );
		MyTarget.JoystickBackgroundGOName = EditorGUILayout.TextField( "Background NameGO", MyTarget.JoystickBackgroundGOName );
		MyTarget.JoystickTouchZoneGOName = EditorGUILayout.TextField( "TouchZone NameGO", MyTarget.JoystickTouchZoneGOName );
							
		
		MyTarget.JoystickTexture = (Texture2D)EditorGUILayout.ObjectField( "Joystick Texture", MyTarget.JoystickTexture, typeof(Texture2D), false );
		MyTarget.BackgroundTexture = (Texture2D)EditorGUILayout.ObjectField( "Background Texture", MyTarget.BackgroundTexture, typeof(Texture2D), false );
		MyTarget.TouchZoneTexture = (Texture2D)EditorGUILayout.ObjectField( "TouchZone Texture", MyTarget.TouchZoneTexture, typeof(Texture2D), false );
		
	}
	#endregion
}
