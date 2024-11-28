using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Tooltip : MonoBehaviour
{
    [SerializeField] private float xLimit = 960;
    [SerializeField] private float yLimit = 540;

    [SerializeField] private float xOffset = 150;
    [SerializeField] private float yOffset = 150;

    public virtual void AdjustPosition()
    {
        //以下代码实现让技能解释框出现在鼠标附近
        Vector2 mouseposition = Input.mousePosition;

        float newXoffset = 0;
        float newYoffset = 0;

        if (mouseposition.x > xLimit)
            newXoffset = -xOffset;
        else
            newXoffset = xOffset;

        if (mouseposition.y > yLimit)
            newYoffset = -yOffset;
        else
            newYoffset = yOffset;

        transform.position = new Vector2(mouseposition.x + newXoffset, mouseposition.y + newYoffset);

    }

    public void AdjustFontSize(TextMeshProUGUI _text)
    {
        if (_text.text.Length > 12)
            _text.fontSize = _text.fontSize * .8f;
    }
}
