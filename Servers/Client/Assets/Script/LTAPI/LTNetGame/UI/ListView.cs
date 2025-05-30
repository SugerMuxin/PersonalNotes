using UnityEngine;
using System.Collections;

using System;

public class ListView  {

	public ListView()
	{
	}

	public int SelectIndex = -1;
	public ArrayList Items= new ArrayList();
	Vector2 scrollPos;
	public int Width = 300;
	public int Height = 150;
	public int CellWidht = 300;
	public int CellHeight = 30;
	public Action <object, int>SelectIndexChanged;
	public void OnGui()
	{
		scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width (Width), GUILayout.Height (Height));
		for(int i =0 ;i < Items.Count ;i ++)
		{
			string st = Items[i].ToString();
			if (i ==SelectIndex )
			{
				if(GUILayout.Button("(选中)" + st , GUILayout.Width (CellWidht), GUILayout.Height (CellHeight)))
				{
					Debug.Log("index + " + i.ToString());
				}
			}else
			{
				if(GUILayout.Button(st, GUILayout.Width (CellWidht), GUILayout.Height (CellHeight)))
				{
					Debug.Log("index + " + i.ToString());
					SelectIndex = i;
					if(SelectIndexChanged != null)
					{
						SelectIndexChanged(this,SelectIndex);
					}
				}
			}
			
		}
		GUILayout.EndScrollView();


	}
}
