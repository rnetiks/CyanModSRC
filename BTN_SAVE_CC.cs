using System;
using UnityEngine;

public class BTN_SAVE_CC : MonoBehaviour
{
    //Сохранение сетов.

    public GameObject manager;

    private void OnClick()
    {
        this.manager.GetComponent<CustomCharacterManager>().SaveData();
    }
    void Start()
    {
        base.gameObject.transform.localPosition = new Vector3(0, 9999, 0);
    }
}

