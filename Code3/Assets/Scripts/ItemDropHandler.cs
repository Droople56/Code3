using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel = transform as RectTransform;
        if(RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log(eventData);
        }
        else
        {
            Debug.Log("Drop Thing");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
