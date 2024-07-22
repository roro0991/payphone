using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Coin : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Canvas canvas;
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("PhoneCanvas").GetComponent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {

        PointerEventData pointerData = (PointerEventData)eventData; 

        Vector2 position; 
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            canvas.worldCamera,
            out position);

        transform.position = canvas.transform.TransformPoint(position);

        image.raycastTarget = false;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
}
