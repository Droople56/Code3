using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Vector3 origin;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        gameObject.transform.position = origin;
    }

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        Debug.Log(origin);
        Debug.Log(transform.localPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
