using System;
using UnityEngine;

public class CharacterCreationComponent : MonoBehaviour
{
    public GameObject manager;
    public CreatePart part;
    CustomCharacterManager custcharacter;

    public void nextOption()
    {
        (custcharacter != null ? custcharacter : custcharacter = this.manager.GetComponent<CustomCharacterManager>()).nextOption(this.part);
    }

    public void prevOption()
    {
        (custcharacter != null ? custcharacter : custcharacter = this.manager.GetComponent<CustomCharacterManager>()).prevOption(this.part);
    }
    void Start()
    {
        base.gameObject.transform.localPosition = new Vector3(0, 9999, 0);
    }

}

