using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class WindowMod : MonoBehaviour {
	Rect screenPosition;
	
	[DllImport("user32.dll")]
	static extern IntPtr SetWindowLongPtr (IntPtr hwnd, int _nIndex, IntPtr dwNewLong);
	[DllImport("user32.dll")]
	static extern bool SetWindowPos (IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
	[DllImport("user32.dll")]
	static extern IntPtr GetForegroundWindow ();
	
	const uint SWP_SHOWWINDOW = 0x0040;
	const uint SWP_NOSIZE = 0x0001;
	//const int GWL_STYLE = -16;
	//IntPtr WS_BORDER = (IntPtr)0x00800000L;
	
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);	
	}
	
	IEnumerator Start() {
		if (!Application.isEditor) {
			screenPosition.x = -Screen.width/2;
			screenPosition.y = 0;
			screenPosition.width = Screen.width;
			screenPosition.height = Screen.height;
			Screen.SetResolution((int)screenPosition.width,(int)screenPosition.height,false);
			yield return new WaitForSeconds(0.1f);
			SetWindowPos (GetForegroundWindow(), 0, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW | SWP_NOSIZE);
			
		}
		
	}
	
	
}
