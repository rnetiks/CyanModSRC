using System;
using UnityEngine;

public class CharacterStatComponent : MonoBehaviour
{
    public GameObject manager;
    public CreateStat type;
    CustomCharacterManager custcharacter;

    public void nextOption()
    {
        (custcharacter != null ? custcharacter : custcharacter = this.manager.GetComponent<CustomCharacterManager>()).nextStatOption(this.type);
    }

    public void prevOption()
    {
        (custcharacter != null ? custcharacter : custcharacter = this.manager.GetComponent<CustomCharacterManager>()).prevStatOption(this.type);
    }
    void Start()
    {
        base.gameObject.transform.localPosition = new Vector3(0, 9999, 0);
    }

}

