using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventObjectFromBeingDestroyed : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
