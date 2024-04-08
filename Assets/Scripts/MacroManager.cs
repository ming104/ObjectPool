#define ARRAY

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacroManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if ARRAY
        Debug.Log("MACRO : ARRAY");
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }
}
