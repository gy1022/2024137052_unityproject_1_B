using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderFollowObject : MonoBehaviour
{
    public Transform targetObject;
    public Slider slider;

    // Update is called once per frame
    void Update()
    {
        if(targetObject != null && slider != null)
        {
            Vector3 screerPos = Camera.main.WorldToScreenPoint(targetObject.position);

            RectTransform canvasRect = slider.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            Vector2 canvasPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screerPos, null, out canvasPos);

            slider.transform.localPosition = canvasPos;
        }
    }
}
