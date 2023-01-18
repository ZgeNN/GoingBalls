using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript: MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
