using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Moves the joystick and passes the vector data so player can move

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bgImage;          // Joystick background image
    private Image joystick;         // Joystick image
    private Vector3 inputVec;       // Vector passed from joystick

    //// FINDS THE IMAGES ////
    private void Start()
    {
        bgImage = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;        // Position clicked

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform, ped.position, ped.pressEventCamera, out pos))     // if there is a touch/click within the defined space (the bg image rect)
        {
            //// GETS THE POINT CLICKED AND TRANSLATES TO BE EBTWEEN -1 AND 1 ////
            pos.x = (pos.x / bgImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImage.rectTransform.sizeDelta.y);

            // Debug.Log(pos);

            /*inputVec = new Vector3((pos.x * 2) + 1, (pos.y * 2) - 1);
            inputVec = (inputVec.magnitude > 1.0f) ? inputVec.normalized : inputVec;*/

            inputVec = new Vector3(pos.x, pos.y);       // Assigns pos of place on image to the vector for movement
            inputVec = (inputVec.magnitude > 1.0f) ? inputVec.normalized : inputVec;    // Normalises if magnitude > 1 else it doesnt change
            
            joystick.rectTransform.anchoredPosition = new Vector3(inputVec.x * (bgImage.rectTransform.sizeDelta.x / 2), inputVec.y * (bgImage.rectTransform.sizeDelta.y / 2));      // Moves joystick image - ref for player only

            //Debug.Log(joystick.rectTransform.anchoredPosition);
        }
    }
    
    //// PASSES THE POINTER DATA TO THE ONDRAG FUNCTION ////
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    //// RETURNS JOYSTICK IMAGE TO CENTRE ON RELEASE ////
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVec = Vector3.zero;
        joystick.rectTransform.anchoredPosition = Vector3.zero;
    }


    //// RETURNS THE X COMPONENT OF VECTOR ////
    public float Horizontal()
    {
        if (inputVec.x != 0)
            return inputVec.x;
        else
            return Input.GetAxis("Horizontal");
    }

    //// RETURNS THE Y COMPONENT OF THE VECOTR ////
    public float Vertical()
    {
        if (inputVec.y != 0)
            return inputVec.y;
        else
            return Input.GetAxis("Vertical");
    }
}
