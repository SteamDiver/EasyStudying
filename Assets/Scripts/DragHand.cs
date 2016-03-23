using UnityEngine;
using UnityEngine.EventSystems;

public class DragHand : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;
    Vector3 StartPosition;
    Transform startParent;


    public Canvas myCanvas;

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        StartPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        GameObject doubleitem = itemBeingDragged;
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == startParent)
        {
            transform.position = StartPosition;
        }
        else
        {
             //GameObject NewItem = (GameObject) Instantiate(doubleitem,StartPosition, Quaternion.identity);
            //NewItem.transform.SetParent(startParent);
           
        }
       
    }
}
