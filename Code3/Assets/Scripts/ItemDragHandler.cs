using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Vector3 origin;
    RectTransform cell;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RectTransform cell = transform as RectTransform;
        if(RectTransformUtility.RectangleContainsScreenPoint(cell, Input.mousePosition))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.localPosition = origin;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.localPosition;
        cell = transform.GetComponentInParent<RectTransform>();
        Debug.Log(cell);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
