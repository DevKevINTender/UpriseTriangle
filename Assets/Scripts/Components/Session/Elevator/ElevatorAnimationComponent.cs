using UnityEngine;
using DG.Tweening;

public class ElevatorAnimationComponent : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] RectTransform topLine;
    [SerializeField] RectTransform bottomLine;
    [SerializeField] Transform cameraMain;

    public void DoShake()
    {
        TweenCallback callback = () => { cameraMain.transform.localPosition = new Vector3(0, 0, -10); };
        cameraMain.transform.DOShakePosition(0.5f, 0.15f, 15, 0, false, true).OnComplete(callback);
    }

    public void PersonEnterElevator()
    {
        topLine.DOAnchorPosY(0, 0.1f);
        bottomLine.DOAnchorPosY(0, 0.1f);
        DoShake();
    }

    public void PersoExitElevator()
    {
        topLine.DOAnchorPosY(80, 0.5f);
        bottomLine.DOAnchorPosY(-80, 0.5f);
    }
}
