using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetClicController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CameraSettings _cameraSettings;
    [SerializeField] private Boarder _boarder;
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        switch(pointerEventData.button)
        {
            case PointerEventData.InputButton.Left:
                _cameraSettings.Activate();
                break;
            case PointerEventData.InputButton.Right:
                _boarder.Board(this.gameObject);
                break;

        }
    }
}
