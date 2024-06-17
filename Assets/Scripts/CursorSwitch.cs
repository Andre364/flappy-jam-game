using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSwitch : MonoBehaviour
{
    public RectTransform canvas;
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, Camera.main, out anchoredPos);
        //gameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPos;

        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Input.mousePosition; //new Vector2(pos.x * 16, pos.y * 9);
    }
}
