using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class CoroutineHost : MonoBehaviour
{
    //singleton
    public static CoroutineHost Instance
    {
        get
        {
            if(n_Instance == null)
            {
                n_Instance = (CoroutineHost)FindObjectOfType(typeof(CoroutineHost));

                if(n_Instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "CoroutineHost";
                    n_Instance = go.AddComponent<CoroutineHost>();
                }
                DontDestroyOnLoad(n_Instance.gameObject);
            }
            return n_Instance;
        }
    }

    private static CoroutineHost n_Instance = null;
}
