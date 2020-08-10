using System;
using UnityEngine;

public class SpectatorMovement : MonoBehaviour
{
    public bool disable;
    public float sensitivity = 1F;
    private Camera goCamera;
    static bool enabledPanel = true;
    Vector2 scrollPos;
    float[] speed_pos = new float[] { 100f, 100f, 100f, 100f };
    float[] speed_rot = new float[] { 1.2f, 1.2f, 1.2f, 1.2f };

    void Start()
    {
        goCamera = Camera.main;
    }
    void OnGUI()
    {
        if (!this.disable && enabledPanel)
        {
            GUI.backgroundColor = CyanMod.INC.gui_color;
            GUILayout.BeginArea(new Rect(Screen.width - 480f, Screen.height - 200f, 480f, 200f), GUI.skin.box);
            scrollPos = GUILayout.BeginScrollView(scrollPos);
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            {
                GUILayout.Label("Вращение камеры:");
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("U:", GUILayout.Width(30f));
                    speed_rot[0] = Convert.ToSingle(GUILayout.TextField(speed_rot[0].ToString("F"), GUILayout.Width(70f)));
                    speed_rot[0] = GUILayout.HorizontalSlider(speed_rot[0], 0f, 20f);
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("J:", GUILayout.Width(30f));
                    speed_rot[1] = Convert.ToSingle(GUILayout.TextField(speed_rot[1].ToString("F"), GUILayout.Width(70f)));
                    speed_rot[1] = GUILayout.HorizontalSlider(speed_rot[1], 0f, 20f);
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("H:", GUILayout.Width(30f));
                    speed_rot[2] = Convert.ToSingle(GUILayout.TextField(speed_rot[2].ToString("F"), GUILayout.Width(70f)));
                    speed_rot[2] = GUILayout.HorizontalSlider(speed_rot[2], 0f, 20f);
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("K:", GUILayout.Width(30f));
                    speed_rot[3] = Convert.ToSingle(GUILayout.TextField(speed_rot[3].ToString("F"), GUILayout.Width(70f)));
                    speed_rot[3] = GUILayout.HorizontalSlider(speed_rot[3], 0f, 20f);
                }
                GUILayout.EndHorizontal();


            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            {
                GUILayout.Label("Перемещение камеры:");
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("W:", GUILayout.Width(30f));
                    speed_pos[0] = Convert.ToSingle(GUILayout.TextField(speed_pos[0].ToString("F"), GUILayout.Width(70f)));
                    speed_pos[0] = GUILayout.HorizontalSlider(speed_pos[0], 0f, 200f);
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("S:", GUILayout.Width(30f));
                    speed_pos[1] = Convert.ToSingle(GUILayout.TextField(speed_pos[1].ToString("F"), GUILayout.Width(70f)));
                    speed_pos[1] = GUILayout.HorizontalSlider(speed_pos[1], 0f, 200f);
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("A:", GUILayout.Width(30f));
                    speed_pos[2] = Convert.ToSingle(GUILayout.TextField(speed_pos[2].ToString("F"), GUILayout.Width(70f)));
                    speed_pos[2] = GUILayout.HorizontalSlider(speed_pos[2], 0f, 200f);
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("D:", GUILayout.Width(30f));
                    speed_pos[3] = Convert.ToSingle(GUILayout.TextField(speed_pos[3].ToString("F"), GUILayout.Width(70f)));
                    speed_pos[3] = GUILayout.HorizontalSlider(speed_pos[3], 0f, 200f);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Поле зрения[" + Camera.main.fieldOfView.ToString("F") + "]:",GUILayout.Width(150f));
            Camera.main.fieldOfView = GUILayout.HorizontalSlider(Camera.main.fieldOfView, 0f, 150f);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Кнопка[8]Управление мышью.");
                GUILayout.Label("Кнопка[9]Скрыть интерфейс.");
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
        {
            angle += 360f;
        }
        if (angle > 360f)
        {
            angle -= 360f;
        }
        return Mathf.Clamp(angle, min, max);
    }
    void FixedUpdate()
    {
        if (!this.disable)
        {
            float MyAnglex = 0;
            float MyAngley = 0;
            if (Input.GetKey(KeyCode.H))
            {
                MyAnglex = speed_rot[3] * ((250f - (Screen.width / 2)) / Screen.width);
               
                    goCamera.transform.RotateAround(goCamera.gameObject.transform.position, goCamera.transform.up, MyAnglex);
                
            }
            if (Input.GetKey(KeyCode.K))
            {
                MyAnglex = speed_rot[2] * ((250f - (Screen.width / 2)) / Screen.width);
               
                    goCamera.transform.RotateAround(goCamera.gameObject.transform.position, goCamera.transform.up, -MyAnglex);
                
            }
            if (Input.GetKey(KeyCode.U))
            {
                MyAngley = speed_rot[0] * ((250f - (Screen.height / 2)) / Screen.height);
              
                    goCamera.transform.RotateAround(goCamera.gameObject.transform.position, goCamera.transform.right,MyAngley);
                
            }
            if (Input.GetKey(KeyCode.J))
            {
                MyAngley = speed_rot[1] * ((250f - (Screen.height / 2)) / Screen.height);
               
                    goCamera.transform.RotateAround(goCamera.gameObject.transform.position, goCamera.transform.right,-MyAngley);
                
            }

          
            
        }
    }

    private void Update()
    {
        if (!this.disable)
        {
            float num;
            float num2;
            float[] ss = new float[] { speed_pos[0], speed_pos[1], speed_pos[2], speed_pos[3] };
         
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                IN_GAME_MAIN_CAMERA.instance.mouselook.disable = !IN_GAME_MAIN_CAMERA.instance.mouselook.disable;
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                enabledPanel = !enabledPanel;
            }
            if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_jump))
            {
                for (int i = 0; i < ss.Length; i++ )
                {
                    ss[i] *= 3f;
                }
            }
            if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_up))
            {
                num = 1f;
            }
            else if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_down))
            {
                num = -1f;
            }
            else
            {
                num = 0f;
            }
            if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_left))
            {
                num2 = -1f;
            }
            else if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_right))
            {
                num2 = 1f;
            }
            else
            {
                num2 = 0f;
            }
            Transform transform = base.transform;
            if (num > 0f)
            {
                transform.position += (Vector3)((base.transform.forward * ss[0]) * Time.deltaTime);
            }
            else if (num < 0f)
            {
                transform.position -= (Vector3)((base.transform.forward * ss[1]) * Time.deltaTime);
            }
            if (num2 > 0f)
            {
                transform.position += (Vector3)((base.transform.right * ss[3]) * Time.deltaTime);
            }
            else if (num2 < 0f)
            {
                transform.position -= (Vector3)((base.transform.right * ss[2]) * Time.deltaTime);
            }
            if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_leftRope))
            {
                transform.position -= (Vector3) ((base.transform.up * 100) * Time.deltaTime);
            }
            else if (FengGameManagerMKII.inputRC.isInputCustomKey(InputCode.custom_rightRope))
            {
                transform.position += (Vector3) ((base.transform.up * 100) * Time.deltaTime);
            }
        }
    }
}

