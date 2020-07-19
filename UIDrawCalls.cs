using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


	public class UIDrawCalls: UnityEngine.MonoBehaviour
	{
        public static UIDrawCalls instance;
        void Awake()
        {
            instance = this;
        }
        void OnDestroy()
        {
            if (instance != null)
            {
                instance = null;
            }
        }
	}

