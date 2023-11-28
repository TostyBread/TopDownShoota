using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{

    private RectTransform _crosshair;

    // Start is called before the first frame update
    void Start()
    {
        _crosshair = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_crosshair == null)
            return;

        _crosshair.position = Input.mousePosition; // crosshair will now follow the mouse

        if(Cursor.visible) // if mouse cursor visible, it will hide
            Cursor.visible = false;
    }
}
