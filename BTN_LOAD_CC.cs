using System;
using UnityEngine;

public class BTN_LOAD_CC : MonoBehaviour
{
    //Загрузка Сетов.

    public GameObject manager;

    private void OnClick()
    {
        this.manager.GetComponent<CustomCharacterManager>().LoadData();
    }
    void Start()
    {
        base.gameObject.transform.localPosition = new Vector3(0, 9999, 0);
    }
}

