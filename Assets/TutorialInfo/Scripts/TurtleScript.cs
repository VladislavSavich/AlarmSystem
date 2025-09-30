using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour
{
    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
    }
}
