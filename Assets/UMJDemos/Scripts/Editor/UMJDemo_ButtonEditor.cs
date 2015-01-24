/********************************************************
 * 														*
 * ScriptName:   UMJDemo_ButtonEditor.cs				*
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

[CustomEditor(typeof(UMJDemo_Button))]

public class UMJDemo_ButtonEditor : Editor {
	
	#region MainVars
	private UMJDemo_Button MyTarget = null;
	#endregion
	
	#region OnEnable
	public void OnEnable()
	{
		MyTarget = ( UMJDemo_Button )target;
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
		MyTarget.DV_SETUP_MODE = EditorGUILayout.Toggle( "DV_SETUP_MODE", MyTarget.DV_SETUP_MODE );
		MyTarget.UseMouse = EditorGUILayout.Toggle( "Use Mouse", MyTarget.UseMouse );
		
		MyTarget.ButtonUpSize = EditorGUILayout.Slider( "Button UpSize", MyTarget.ButtonUpSize, 2f, 10f );
		MyTarget.ButtonDownSize = EditorGUILayout.Slider( "Button DownSize", MyTarget.ButtonDownSize, 2f, 10f );
		MyTarget.ButtonOffsetX = EditorGUILayout.Slider( "Button OffsetX", MyTarget.ButtonOffsetX, -10f, 50f );
		MyTarget.ButtonOffsetY = EditorGUILayout.Slider( "Button OffsetY", MyTarget.ButtonOffsetY, -10f, 50f );
		
		MyTarget.ButtonTexture = (Texture2D)EditorGUILayout.ObjectField( "Button Texture", MyTarget.ButtonTexture, typeof(Texture2D), false );
	}
	#endregion
	
}