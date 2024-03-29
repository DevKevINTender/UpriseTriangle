﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovePanelView : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public delegate void PauseDelegate();
    private PauseDelegate startPause;
    private PauseDelegate endPause;

    [SerializeField] private PersonMoveController personMoveController;
    private int touchCount;
    private Vector3 touchPoint;

    public void Init(PauseDelegate startpause, PauseDelegate endPause)
    {
        this.startPause = startpause;
        this.endPause = endPause;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        endPause?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        touchPoint = eventData.pointerCurrentRaycast.screenPosition;
        touchCount = Input.touchCount;
        personMoveController.PlayerMove(touchPoint, touchCount);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Input.touchCount == 1)
        {
            StartCoroutine(pauseDelay());
        }
        personMoveController.SetTouchCount(0);
    }

    public IEnumerator pauseDelay()
    {
        float timer = 0.3f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        if (Input.touchCount == 0)
        {
            startPause?.Invoke();
        }
        
    }
}