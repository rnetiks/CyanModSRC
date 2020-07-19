using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CyanMod;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine.UI;

	public class GameObjectInterect: UnityEngine.MonoBehaviour
	{
        GameObject current_gm;
        string Filter;
        string saved_name;
        List<GameObject> list_gm;
        bool isEnable = false;
        Texture textured;
        Rect rect;
        Vector2 pos;
        Vector2 pos2;
        List<Type> types;
        List<Component> childrenOBJ;
        List<UISprite> uispriteList;
        bool editpos = false;
        float iscorrect = 0f;
        UISprite sprite_test;
        string[] localscalse = new string[] { "0", "0", "0" };
        void Awake()
        {
            rect = new Rect(0, 0, 500, 500);
            list_gm = new List<GameObject>();
            Filter = "";
            saved_name = "image";
            types = new List<Type>();
            childrenOBJ = new List<Component>();
            uispriteList = new List<UISprite>();
        }
        void OnEnable()
        {
            isEnable = base.enabled;
        }
        void set_GameObject(GameObject obj)
        {
            if (textured != null)
            {
                textured = null;
            }
            pos2.y = 0;
            types = new List<Type>();
            childrenOBJ = new List<Component>();
            uispriteList = new List<UISprite>();
            current_gm = obj;
        }
        void menu(int i)
        {
            GUILayout.Label("Game Objects", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter });
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(200f));
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.alignment = TextAnchor.MiddleLeft;
            GUIStyle style2 = GUI.skin.button;
            GUILayout.BeginHorizontal();
            Filter = GUILayout.TextField(Filter, GUILayout.Width(170f));
            if (GUILayout.Button("upd"))
            {
                update_list();
            }
            GUILayout.EndHorizontal();
            pos = GUILayout.BeginScrollView(pos);
            foreach (GameObject o in list_gm)
            {
                if (o != null)
                {
                    if (o == current_gm)
                    {
                        style.normal.background = style2.onNormal.background;
                        style.normal.textColor = style2.onNormal.textColor;
                    }
                    else
                    {
                        style.normal.background = style2.normal.background;
                        style.normal.textColor = style2.normal.textColor;
                    }
                    string nname = o.name;
                    if (o.GetActive())
                    {
                        nname = "<color=#00FF00>O</color>" + nname;
                    }
                    else
                    {
                        nname = "<color=#FF0000>O</color>" + nname;
                    }
                    if (GUILayout.Button(nname, style))
                    {
                        set_GameObject(o);
                    }
                }
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.BeginVertical();

            pos2 = GUILayout.BeginScrollView(pos2);
            if (current_gm != null)
            {
                string str = "Name:" + current_gm.name;
                if (current_gm.transform != null)
                {
                    str = str + "\nParrent name:" + (current_gm.transform.parent != null ? current_gm.transform.parent.name : "none");
                    str = str + "\nGlobal pos:" + (current_gm.transform.position.ToString());
                    str = str + "\nLocal pos:" + (current_gm.transform.localPosition.ToString());
                    str = str + "\nRotation:" + (current_gm.transform.rotation.ToString());
                     str = str + "\nLocalScale:" + (current_gm.transform.localScale.ToString());
                }
                str = str + "\nActive:" + (current_gm.GetActive());

                GUILayout.TextField(str != string.Empty ? str : "none", GUI.skin.label);
                if (current_gm.transform.parent != null)
                {
                    if (GUILayout.Button("go to parrent"))
                    {
                        set_GameObject(current_gm.transform.parent.gameObject);
                    }
                }
                if (GUILayout.Button("test"))
                {
                   
                    foreach (Canvas ren in current_gm.GetComponentsInChildren<Canvas>())
                    {
                        if (ren.renderer.material != null)
                        {
                            ren.renderer.material.mainTexture = FengGameManagerMKII.background_image;
                        }
                    }
                    
                    foreach (Renderer ren in current_gm.GetComponentsInParent<Renderer>())
                    {
                        if (ren.material != null)
                        {
                            ren.material.mainTexture = FengGameManagerMKII.background_image;
                        }
                    }

                }
                if (GUILayout.Button("Dell"))
                {
                    Destroy(current_gm);
                }
                if (GUILayout.Button("Children Object"))
                {
                    childrenOBJ = new List<Component>();
                    foreach (Component obj in current_gm.GetComponentsInChildren<Component>())
                    {
                        if (obj.name != "" && !childrenOBJ.Contains(obj))
                        {
                            childrenOBJ.Add(obj);
                        }
                    }
                }
                if (childrenOBJ.Count>0)
                {
                    if (GUILayout.Button("Clear"))
                    {
                        childrenOBJ.Clear();
                    }
                    foreach (Component obj in childrenOBJ)
                    {
                        if (GUILayout.Button(obj.name))
                        {
                            set_GameObject(obj.gameObject);
                        }
                    }
                }
                if (GUILayout.Button("Show  List Sprites"))
                {
                    uispriteList = new List<UISprite>();
                    UISprite[] sdd = FindObjectsOfType<UISprite>();
                    foreach (UISprite sprit in sdd)
                    {
                        uispriteList.Add(sprit);
                    }
                }
                if (uispriteList.Count > 0)
                {
                    if (GUILayout.Button("Clear"))
                    {
                        uispriteList.Clear();
                    }
                    foreach (UISprite sss in uispriteList)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label(sss.spriteName);
                        GUILayout.Box(sss.mainTexture, new GUIStyle() { fixedHeight = 40,fixedWidth = 40});
                        GUILayout.EndHorizontal();
                    }
                }
                if (GUILayout.Button("Show List Components Object"))
                {
                    types = new List<Type>();
                
                    Type[] typelist = Assembly.GetExecutingAssembly().GetTypes();
                    foreach (Type type in typelist)
                    {
                        if (current_gm.GetComponent(type) != null)
                        {
                            types.Add(type);
                        }
                    }
                    Type[] typelist2 = Assembly.GetAssembly(typeof (GUILayout)).GetTypes();
                    foreach (Type type in typelist2)
                    {
                        if (current_gm.GetComponent(type) != null)
                        {
                            types.Add(type);
                        }
                    }

                }
                if (types.Count > 0)
                {
                    if (GUILayout.Button("Clear"))
                    {
                        types.Clear();
                    }
                    string dd = "";
                    foreach (Type type in types)
                    {
                        dd = dd + type.Name + "\n";
                    }
                    GUILayout.TextField(dd, GUI.skin.label);
                }
                if (GUILayout.Button("Edit position"))
                {
                    editpos = !editpos;
                }
                if (editpos)
                {
                    GUILayout.Label("+" + iscorrect.ToString("F"));
                    iscorrect = GUILayout.HorizontalSlider(iscorrect,0,100f);
                    iscorrect = GUILayout.HorizontalSlider(iscorrect, 0, 50f);
                    iscorrect = GUILayout.HorizontalSlider(iscorrect, 0, 5f);
                    GUILayout.Label("Global position:");
                 
                    GUILayout.Label("Local position:");
                    cext.pos_to_GUI_Slider(current_gm, 200f, -10 * iscorrect, 10 * iscorrect);
                    GUILayout.Label("Local scale:");
                    GUILayout.BeginHorizontal();
                    localscalse[0] = GUILayout.TextField(localscalse[0]);
                    localscalse[1] = GUILayout.TextField(localscalse[1]);
                    localscalse[2] = GUILayout.TextField(localscalse[2]);
                    if (GUILayout.Button(">>"))
                    {
                        current_gm.transform.localScale = new Vector3(Convert.ToSingle(localscalse[0]), Convert.ToSingle(localscalse[1]), Convert.ToSingle(localscalse[2]));  
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Label("Rotation:");
               cext.rot_to_GUI_Slider(current_gm, 200f);
                }
                if (current_gm.renderer != null && current_gm.renderer.material != null)
                {
                    if (GUILayout.Button("Main Texture"))
                    {
                        textured = current_gm.renderer.material.mainTexture;
                        saved_name = current_gm.name;
                    }
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Front"))
                    {
                        textured = current_gm.renderer.material.GetTexture("_FrontTex");
                        saved_name = current_gm.name;
                    }
                    if (GUILayout.Button("Back"))
                    {
                        textured = current_gm.renderer.material.GetTexture("_BackTex");
                        saved_name = current_gm.name;
                    }
                    if (GUILayout.Button("Left"))
                    {
                        textured = current_gm.renderer.material.GetTexture("_LeftTex");
                        saved_name = current_gm.name;
                    }
                    if (GUILayout.Button("Right"))
                    {
                        textured = current_gm.renderer.material.GetTexture("_RightTex");
                        saved_name = current_gm.name;
                    }
                    if (GUILayout.Button("Up"))
                    {
                        textured = current_gm.renderer.material.GetTexture("_UpTex");
                        saved_name = current_gm.name;
                    }
                    if (GUILayout.Button("Down"))
                    {
                        textured = current_gm.renderer.material.GetTexture("_DownTex");
                        saved_name = current_gm.name;
                    }
                    GUILayout.EndHorizontal();
                }
                if (textured != null)
                {
                    GUIStyle style3 = new GUIStyle();
                    style3.fixedHeight = 200;
                    style3.fixedWidth = 200;
                    GUILayout.Box(textured, style3);
                    saved_name = GUILayout.TextField(saved_name);
                    if (GUILayout.Button("Load"))
                    {
                        FileInfo info = new FileInfo(Application.dataPath + "/" + saved_name + ".png");
                        if (info.Exists)
                        {
                            byte[] sssd = info.ReadAllBytes();
                            Texture2D rrr = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                            rrr.LoadImage(sssd);
                            rrr.Apply();
                            current_gm.renderer.material.mainTexture = (Texture)rrr;
                            saved_name = "Loaded!";
                        }
                    }
                    if (GUILayout.Button("Save"))
                    {
                        File.WriteAllBytes(Application.dataPath + "/" + saved_name + ".png", ((Texture2D)textured).EncodeToPNG());
                        saved_name = "Saved!";
                    }
                    if (GUILayout.Button("Close"))
                    {
                        textured = null;
                    }
                }

            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUI.DragWindow();
        }
        void OnGUI()
        {
            if (isEnable)
            {
                rect = GUI.Window(250, rect, menu, "",GUI.skin.box);
            }
        }
        void update_list()
        {
            list_gm = new List<GameObject>();
            foreach(GameObject obj in FindObjectsOfType<GameObject>())
            {
                if(obj != null && obj.name != "" && obj.name.Contains(Filter))
                {
                    list_gm.Add(obj);
                }
            }
            list_gm = this.list_gm = new List<GameObject>(this.list_gm.OrderBy<GameObject, GameObject>(x => x, new FunctionComparer<GameObject>((x, y) => string.Compare(x.name, y.name))));
        }
        void Start()
        {
            isEnable = false; 
        }
        void LateUpdate()
        {
            if (FengGameManagerMKII.inputRC.isInputCyanKeyDown(InputCode.objects_list))
            {
                isEnable = !isEnable;
                if (isEnable)
                {
                    update_list();
                    current_gm = list_gm[0];
                }
            }
        }
	}

