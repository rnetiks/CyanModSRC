using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace testedClass
{
   public class Tested : UnityEngine.MonoBehaviour
	{
      
         IN_GAME_MAIN_CAMERA mainCamera;
         public static Tested instance;
         string T1 = string.Empty;
         string info = string.Empty;
         float x = 0f;
         float y = 0f;
         float z = 0f;
         float w = 0f;
         public void AddCamera(IN_GAME_MAIN_CAMERA c)
         {
             mainCamera = c;
         }
         public static string ssd = string.Empty;
         void OnGUI()
         {
             if (false)
             {
                 GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, 20, 200, 400));



                 GUILayout.EndArea();
             }
         }
        void Update()
        {
          
        }
       void Start()
       {
           instance = this;
       }
	}
}
