using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

	public class PanelUISpectator : UnityEngine.MonoBehaviour
	{
        public GameObject go;
        public float sensitivity = 1F;
        private Camera goCamera;
        private Vector3 MousePos;
        private float MyAngle = 0F;
        public static PanelUISpectator instance;
        static bool enbl;
        public static bool Enabled
        {
            get
            {
                return enbl;
            }
            set
            {
                //if (PanelUISpectator.instance == null)
                //{
                //    (new GameObject("Spectator_UI")).AddComponent<PanelUISpectator>();
                //}
                //PanelUISpectator.instance.gameObject.SetActive(enbl);
                enbl = value;
            }
        }

        void Awake()
        {
            instance = this;
        }
        void OnDestroy()
        {
            instance = null;
        }
        void OnGUI()
        {
            float hh = 200 / 3;
            GUILayout.BeginArea(new Rect(Screen.width - 400, Screen.height - 200, 400, 200));
            GUILayoutOption[] option = new GUILayoutOption[] { GUILayout.Height(hh),GUILayout.Width(50f) };
            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical();
                {
                    GUILayout.Label("", option);
                    GUILayout.Button("A", option);
                    GUILayout.Label("", option);
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical();
                {
                    GUILayout.Label("W", option);
                    GUILayout.Button("S", option);
                    GUILayout.Label("", option);
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical();
                {
                    GUILayout.Label("", option);
                    GUILayout.Button("D", option);
                    GUILayout.Label("", option);
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
        void Start()
        {
            // Инициализируем кординаты мышки и ищем главную камеру
            goCamera = Camera.main;
            go = goCamera.transform.parent.gameObject;
        }

        void Update()
        {
            MousePos = Input.mousePosition;
        }
        void LateUpdate()
        {
            Transform transform7 = goCamera.transform;
            float num2 = 100f;
            if (FengGameManagerMKII.inputRC.isInputLevel(InputCodeRC.levelSlow))
            {
                num2 = 20f;
            }
            else if (FengGameManagerMKII.inputRC.isInputLevel(InputCodeRC.levelFast))
            {
                num2 = 400f;
            }
            if (FengGameManagerMKII.inputRC.isInputLevel(InputCodeRC.levelForward))
            {
                transform7.position += (Vector3)((transform7.forward * num2) * Time.deltaTime);
            }
            else if (FengGameManagerMKII.inputRC.isInputLevel(InputCodeRC.levelBack))
            {
                transform7.position -= (Vector3)((transform7.forward * num2) * Time.deltaTime);
            }
            if (FengGameManagerMKII.inputRC.isInputLevel(InputCodeRC.levelLeft))
            {
                transform7.position -= (Vector3)((transform7.right * num2) * Time.deltaTime);
            }
            else if (FengGameManagerMKII.inputRC.isInputLevel(InputCodeRC.levelRight))
            {
                transform7.position += (Vector3)((transform7.right * num2) * Time.deltaTime);
            }
            if (FengGameManagerMKII.inputRC.isInputLevel(InputCodeRC.levelUp))
            {
                transform7.position += (Vector3)((transform7.up * num2) * Time.deltaTime);
            }
            else if (FengGameManagerMKII.inputRC.isInputLevel(InputCodeRC.levelDown))
            {
                transform7.position -= (Vector3)((transform7.up * num2) * Time.deltaTime);
            }
        }
        void FixedUpdate()
        {
            if (Input.GetMouseButton(1))
            {
                MyAngle = 0;
                MyAngle = sensitivity * ((MousePos.x - (Screen.width / 2)) / Screen.width);
                goCamera.transform.RotateAround(go.transform.position, goCamera.transform.up, MyAngle);
                MyAngle = sensitivity * ((MousePos.y - (Screen.height / 2)) / Screen.height);
                goCamera.transform.RotateAround(go.transform.position, goCamera.transform.right, -MyAngle);
            }
        }
     
	}

