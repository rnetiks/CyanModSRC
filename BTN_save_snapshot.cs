using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.IO;

public class BTN_save_snapshot : MonoBehaviour
{
    public GameObject info;
    public GameObject targetTexture;
    public GameObject[] thingsNeedToHide;
    Vector3 eeee;
    public static bool flag1;
    public static string path = Application.dataPath + "/Snapshots/";

    private void OnClick()
    {
        TweenPosition tp = base.gameObject.AddComponent<TweenPosition>();
        tp.from = new Vector3(0, -1000, 0);
        tp.to = new Vector3(0, -500, 0);
        tp.duration = 0.1f;
        tp.Play(true);


        foreach (GameObject obj2 in this.thingsNeedToHide)
        {
            Transform transform = obj2.transform;
            transform.position += (Vector3)(Vector3.up * 10000f);
        }
        StartCoroutine(ScreenshotEncode());
    }
    void Start()
    {
        eeee = base.gameObject.transform.position;
        flag1 = false;
    }
    void Update()
    {
        if (flag1)
        {
            base.gameObject.transform.position = eeee;
        }
    }
    [DebuggerHidden]
    private IEnumerator ScreenshotEncode()
    { return new ScreenshotEncodec__Iterator0 { f__this = this }; }

    [CompilerGenerated]
    private class ScreenshotEncodec__Iterator0 : IEnumerator<object>
    {
        internal BTN_save_snapshot f__this;
        internal GameObject go__4;
        internal string img_name__5;
        internal float r__0;
        internal object Scurrent;
        internal int SPC;
        internal GameObject[] Ss_5__2;
        internal int Ss_6__3;
        internal Texture2D texture__1;

        [DebuggerHidden]
        public void Dispose()
        {
            this.SPC = -1;
        }

        public bool MoveNext()
        {
            uint sPC = (uint)this.SPC;
            this.SPC = -1;
            switch (sPC)
            {
                case 0:

                    this.Scurrent = new WaitForEndOfFrame();
                    this.SPC = 1;

                    break;

                case 1:
                    this.r__0 = ((float)Screen.height) / 600f;
                    this.texture__1 = new Texture2D((int)(this.r__0 * this.f__this.targetTexture.transform.localScale.x), (int)(this.r__0 * this.f__this.targetTexture.transform.localScale.y), TextureFormat.RGB24, false);
                    this.texture__1.ReadPixels(new Rect((Screen.width * 0.5f) - (this.texture__1.width * 0.5f), ((Screen.height * 0.5f) - (this.texture__1.height * 0.5f)) - (this.r__0 * 0f), (float)this.texture__1.width, (float)this.texture__1.height), 0, 0);
                    this.texture__1.Apply();

                    this.Scurrent = 0;
                    this.SPC = 2;
                    break;


                case 2:
                    {
                        this.Ss_5__2 = this.f__this.thingsNeedToHide;
                        this.Ss_6__3 = 0;
                        while (this.Ss_6__3 < this.Ss_5__2.Length)
                        {
                            this.go__4 = this.Ss_5__2[this.Ss_6__3];
                            Transform transform = this.go__4.transform;
                            transform.position -= (Vector3)(Vector3.up * 10000f);
                            this.Ss_6__3++;
                        }
                        string stre33 = "aottg_ss-" + DateTime.Today.Month.ToString() + "_" + DateTime.Today.Day.ToString() + "_" + DateTime.Today.Year.ToString() + "-" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".png";

                        this.img_name__5 = stre33;
                        object[] args = new object[] { this.img_name__5, this.texture__1.width, this.texture__1.height, Convert.ToBase64String(this.texture__1.EncodeToPNG()) };
                        Application.ExternalCall("SaveImg", args);
                        byte[] bytes = texture__1.EncodeToPNG();
                        string path2 = path + stre33;
                        Directory.CreateDirectory(Path.GetDirectoryName(path2));
                        File.WriteAllBytes(path2, bytes);
                        BTN_save_snapshot.flag1 = true;
                        UnityEngine.Object.DestroyObject(this.texture__1);
                        this.SPC = -1;
                        goto Label_0321;
                    }
                default:
                    goto Label_0321;
            }
            return true;
        Label_0321:
            return false;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        { get { return this.Scurrent; } }

        object IEnumerator.Current
        { get { return this.Scurrent; } }
    }
}

