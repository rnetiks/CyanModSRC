//Премер создания скиня для Кнопки в GUI by tap1k
//Запук класса посредством добавления компонента
//Пример запуска(можно добавить в FengGameManagerMKII.Start()):
//if (Example_skin_GUI.instance == null)
//{
//     GameObject gm = new GameObject("skin_loaded");
//     gm.AddComponent<Example_skin_GUI>();
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

public class Example_skin_GUI : UnityEngine.MonoBehaviour
{
    public static Example_skin_GUI instance;
    bool isAppled = false;
    void Awake()
    {
        
        instance = this;
        DontDestroyOnLoad(base.gameObject);
        isAppled = true;
    }
    Texture2D loadTexture(string path)
    {
        FileInfo info = new FileInfo(path);
        if (info.Exists)
        {
            byte[] bytes = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            texture.LoadImage(bytes);
            texture.Apply();
            return texture;
        }
        return null;
    }
    void OnGUI()
    {
        if (isAppled)
        {
            isAppled = false;
            Texture2D button_normal = loadTexture(Application.dataPath + "/button_normal.png");
            if (button_normal != null)
            {
                GUI.skin.button.normal.background = button_normal;//тестура кнопки при обычном положении
            }
            Texture2D button_active = loadTexture(Application.dataPath + "/button_active.png");
            if (button_active != null)
            {
                GUI.skin.button.active.background = button_active;//текстура кнопки при клике по кнопке
            }
            Texture2D button_hover = loadTexture(Application.dataPath + "/button_hover.png");
            if (button_hover != null)
            {
                GUI.skin.button.hover.background = button_hover;//текстура кнопки при навелдении на нее
            }
            Texture2D button_onNormal = loadTexture(Application.dataPath + "/button_onNormal.png");
            if (button_onNormal != null)
            {
                GUI.skin.button.onNormal.background = button_onNormal;//текстура кнопки когда он включен
            }
            Texture2D button_onHover = loadTexture(Application.dataPath + "/button_onHover.png");
            if (button_onHover != null)
            {
                GUI.skin.button.onHover.background = button_onHover;//текстура кнопки при наведении на нее когда кнопка активна
            }
            Texture2D button_onActive = loadTexture(Application.dataPath + "/button_onActive.png");
            if (button_onActive != null)
            {
                GUI.skin.button.onActive.background = button_onActive;//текстура кнопки при клике когда кнопка активна
            }
            GUI.skin.button.normal.textColor = new Color(0f, 1f, 0f, 1f); //цвет текста
            GUI.skin.button.fontSize = 16;//размер текста

            GUI.skin.button.border.bottom = 7; //смещение текстуры
            GUI.skin.button.border.top = 7; //смещение текстуры
            GUI.skin.button.border.left = 7; //смещение текстуры
            GUI.skin.button.border.right = 7; //смещение текстуры (при загрузке текстуры выше 40х40 эти значения стоит увеличить)
         
            GUI.skin.button.overflow.bottom = 2; //увеличение размера(визуально) текстуры
            GUI.skin.button.overflow.top = 2; //увеличение размера(визуально) текстуры
            GUI.skin.button.overflow.left = 2;//увеличение размера(визуально) текстуры
            GUI.skin.button.overflow.right = 2; 
        }
    }
}

//небольшие пояснения:
//GUI.skin.button - эта сам стайл кнопки (GUIStyle). 
//У стиля кнопки есть параметры: 
//https://docs.unity3d.com/ru/current/ScriptReference/GUIStyle.html
//У кнопок есть статы. при определенно положениии кнопка будет задействовать свою стату GUI.skin.button.normal
//Статы бывают: 
//normal (при бездействии)
//active (стата при клике по скине)
//hover (стата при наведении на скин)
//focused (стата при фокусе на нее) у кнопки такой статы нет
//onNormal(стата при включенном положении)
//onHover(стата при включенном положении и при навелдении)
//onActive(стата при включенном положении и при клике)
//onfocused(стата при включенном положении и при фокусе на нем)
//У каждой статы есть параметры текстуры GUI.skin.button.normal.background и параметр цвета текста GUI.skin.button.normal.textColor