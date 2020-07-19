using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CyanMod;

public class Characters
{
    public class I_LOVE_YOU : UnityEngine.MonoBehaviour
    {
        float timer = 0;
        ParticleSystem partical;
        GameObject baseG;
        void Awake()
        {
            baseG = base.gameObject;
        }
        void Start()
        {
            partical = baseG.GetComponent<ParticleSystem>();
            partical.enableEmission = true;
        }
        void Update()
        {
            timer += FengGameManagerMKII.deltaTime;
            if (timer > 3)
            {
                if (partical.enableEmission)
                {
                    partical.enableEmission = false;
                }
                if (timer > 6)
                {
                    Destroy(baseG);
                }
            }
        }
    }

    public class FACK_YOU : UnityEngine.MonoBehaviour
    {
        float timer = 0;
        ParticleSystem partical;
        GameObject baseG;
        void Awake()
        {
            baseG = base.gameObject;
        }
        void Start()
        {
            partical = baseG.GetComponent<ParticleSystem>();
            partical.enableEmission = true;
        }
        void Update()
        {
            timer += FengGameManagerMKII.deltaTime;
            if (timer > 1.5)
            {
                Destroy(baseG);
            }
        }
    }
    public class CLASS_1 : UnityEngine.MonoBehaviour
    {
        float timer = 0;
        ParticleSystem partical;
        GameObject baseG;
        void Awake()
        {
            baseG = base.gameObject;
        }
        void Start()
        {
            partical = baseG.GetComponent<ParticleSystem>();
            partical.enableEmission = true;
        }
        void Update()
        {
            timer += FengGameManagerMKII.deltaTime;
            if (timer > 3)
            {
                Destroy(baseG);
            }
        }
    }
    public class CLASS_2 : UnityEngine.MonoBehaviour
    {
        float timer = 0;
        ParticleSystem partical;
        GameObject baseG;
        void Awake()
        {
            baseG = base.gameObject;
        }
        void Start()
        {
            partical = baseG.GetComponent<ParticleSystem>();
            partical.enableEmission = true;
        }
        void Update()
        {
            timer += FengGameManagerMKII.deltaTime;
            if (timer > 3)
            {
                Destroy(baseG);
            }
        }
    }
    public class DANGER : UnityEngine.MonoBehaviour
    {
        float timer = 0;
        ParticleSystem partical;
        GameObject baseG;
        void Awake()
        {
            baseG = base.gameObject;
        }
        void Start()
        {
            partical = baseG.GetComponent<ParticleSystem>();
            partical.enableEmission = true;
        }
        void Update()
        {
            timer += FengGameManagerMKII.deltaTime;
            if (timer > 2)
            {
                Destroy(baseG);
            }
        }
    }
    public class WTF : UnityEngine.MonoBehaviour
    {
        float timer = 0;
        ParticleSystem partical;
        GameObject baseG;
        void Awake()
        {
            baseG = base.gameObject;
        }
        void Start()
        {
            partical = baseG.GetComponent<ParticleSystem>();
            partical.enableEmission = true;
        }
        void Update()
        {
            timer += FengGameManagerMKII.deltaTime;
            if (timer > 4)
            {
                Destroy(baseG);
            }
        }
    }
}
