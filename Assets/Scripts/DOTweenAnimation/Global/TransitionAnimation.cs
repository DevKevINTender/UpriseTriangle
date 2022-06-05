using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace DOTweenAnimation.Global
{
    public class TransitionAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform leftPanel;
        [SerializeField] private RectTransform centerImage;

        [SerializeField] private Image bgColor;
        
        public Sequence tPanelAnim;
        
        [SerializeField] Color32 red = new Color32(255, 27, 94, 255);
        [SerializeField] Color32 green = new Color32(46,255,193,255);
        [SerializeField] Color32 white = new Color32(255, 255, 255, 255);
        [SerializeField] Color32 greyDark = new Color32(26, 27, 23, 255);

        private void Start()
        {
            DOTween.defaultTimeScaleIndependent = true;
        }

        public void OpenScene()
        {
            bgColor.color = white;
            tPanelAnim.Kill();
            tPanelAnim = DOTween.Sequence();
            tPanelAnim.AppendInterval(1.5f);
            
            tPanelAnim.Append(centerImage.DOAnchorPos(new Vector2(0, 1500), 1f));
            
            tPanelAnim.Append(leftPanel.DOScaleY(0, 0.75f)).SetEase(Ease.OutCubic);
        }

        public void CloseScene(float duration, int id)
        {
            bgColor.color = white;
            centerImage.localScale = new Vector3(1,1,1);
            tPanelAnim.Kill();
            tPanelAnim = DOTween.Sequence();
            tPanelAnim.AppendInterval(duration);
            
            tPanelAnim.Append(leftPanel.DOScaleY(1, 0.75f)).SetEase(Ease.OutCubic);
            tPanelAnim.Append(centerImage.DOAnchorPos(Vector2.zero, 0.5f)).SetEase(Ease.OutCubic);
            tPanelAnim.Append(centerImage.DOPunchScale(new Vector2(1.2f, 1.2f), 0.5f, 2)).OnComplete(() =>
            {
                SceneManager.LoadScene(id); });
        } 
        public void CloseScene(float duration, string id)
        {
            bgColor.color = white;
            centerImage.localScale = new Vector3(1,1,1);
            tPanelAnim.Kill();
            tPanelAnim = DOTween.Sequence();
            tPanelAnim.AppendInterval(duration);
            
            tPanelAnim.Append(leftPanel.DOScaleY(1, 0.75f)).SetEase(Ease.OutCubic);
            tPanelAnim.Append(centerImage.DOAnchorPos(Vector2.zero, 0.5f)).SetEase(Ease.OutCubic);
            tPanelAnim.Append(centerImage.DOPunchScale(new Vector2(1.2f, 1.2f), 0.5f, 2)).OnComplete(() =>
            {
                SceneManager.LoadScene(id); });
        }

        public void OpenSessionScene()
        {
            bgColor.color = white;
            tPanelAnim.Kill();
            tPanelAnim = DOTween.Sequence();
            tPanelAnim.AppendInterval(1.5f);
            
            tPanelAnim.Append(centerImage.DOPunchScale(new Vector2(0.5f, 0.5f), 0.5f, 2)).SetEase(Ease.InElastic);
            tPanelAnim.Append(centerImage.DOScale(new Vector2(0f, 0f), 0.25f)).SetEase(Ease.OutCubic);
            tPanelAnim.Append(centerImage.DOAnchorPos(new Vector2(0, 1500), 0.5f)).SetEase(Ease.InOutCubic);
            tPanelAnim.Append(leftPanel.DOScaleY(0, 0.75f)).SetEase(Ease.OutCubic);
        }
    }
}