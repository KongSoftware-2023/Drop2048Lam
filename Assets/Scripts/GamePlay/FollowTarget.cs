using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
   [SerializeField]protected GameObject target;
   protected void OnEnable()
    {
        transform.position = target.transform.position;
    }
}
