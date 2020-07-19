using System;
using UnityEngine;

public class BodyPushBox : MonoBehaviour
{
    public GameObject parent;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "bodyCollider")
        {
            BodyPushBox component = other.gameObject.GetComponent<BodyPushBox>();
            if ((component != null) && (component.parent != null))
            {
                float num;
                Vector3 vector = component.parent.transform.position - this.parent.transform.position;
                float radius = base.gameObject.GetComponent<CapsuleCollider>().radius;
                float num3 = base.gameObject.GetComponent<CapsuleCollider>().radius;
                vector.y = 0f;
                if (vector.magnitude > 0f)
                {
                    num = (radius + num3) - vector.magnitude;
                    vector.Normalize();
                }
                else
                {
                    num = radius + num3;
                    vector.x = 1f;
                }
            }
        }
    }
}

