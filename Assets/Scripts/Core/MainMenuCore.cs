using System.Collections;
using System.Collections.Generic;
using Controlers;
using DG.Tweening;
using DOTweenAnimation.Global;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Views.MainMenu;

public class MainMenuCore : MonoBehaviour
{
    [SerializeField] private MenuPanelView MenuPanelView;
    [SerializeField] private PageIndicatorPanelView PageIndicatorPanelView;
    [SerializeField] private Text CoinsCountText;
    [SerializeField] private TransitionAnimation TransitionAnimation;
    [SerializeField] private SegmentStoreView SegmentStoreView;
    [Header("Buttons")]
    [SerializeField] private MMStartButtonView mmStartButtonViewObj;
    [SerializeField] private MMPersonButtonView mmPersonButtonViewObj;
    [SerializeField] private MMSkillButtonView mmSkillButtonViewObj;
    [SerializeField] private MMSegmentButtonView mmSegmentButtonViewObj;
    [SerializeField] private MMLevelsButtonView mmLevelsButtonViewObj;
    [SerializeField] private MMSettingsButtonView mmSettingsButtonView;
    [Header("")]
    AsyncOperation async;

    [SerializeField] private List<RectTransform> pageList = new List<RectTransform>();
    [SerializeField] private int pageCount;
    private int currentPage;
    
    public delegate void buttonDelegate();
    void Start()
    {
        TransitionAnimation.gameObject.SetActive(true);
        TransitionAnimation.OpenScene();
        CoinsCountText.text = $"{CoinsControler.GetCoinsCount()}";
        MenuPanelView.InitView(this);
        PageIndicatorPanelView.InitView(pageCount);
        PageIndicatorPanelView.UpdateView(currentPage);
        
        mmStartButtonViewObj.InitView(LoadSession,LevelChooseControler.GetCurrentLevel());
        mmPersonButtonViewObj.InitView(LoadPersonStorage,PersonStorageContoler.GetPersonById(PersonStorageContoler.GetCurrentPerson()));
        mmSkillButtonViewObj.InitView(LoadSkillStorage,SkillStorageContoler.GetSkillById(SkillStorageContoler.GetCurrentSkill()));
        mmSegmentButtonViewObj.InitView(OpenSegmentPanel,SegmentControler.GetSegmentCount());
        mmLevelsButtonViewObj.InitView(LoadLevelChoose);
        mmSettingsButtonView.InitView(OpenSettingsPanel);

    }
    
    public void LoadScene(int id)
    {
        
    }

    public void LoadSession()
    {
        TransitionAnimation.CloseScene(0, "LevelTest");
    }
    
    public void LoadLevelChoose()
    {
        TransitionAnimation.CloseScene(0, "LevelChoose");
    } 
    
    public void LoadPersonStorage()
    {
        TransitionAnimation.CloseScene(0, "PersonStorage");
    } 
    
    public void LoadSkillStorage()
    {
        TransitionAnimation.CloseScene(0, "SkillStorage");
    }

    public void OpenSegmentPanel()
    {
        SegmentStoreView.gameObject.SetActive(true);
    }
    public void OpenSettingsPanel()
    {
        SegmentStoreView.gameObject.SetActive(true);
    }
    public void ShowPreviousPage()
    {
        if (currentPage > 0)
        {
            
            pageList[currentPage].GetComponent<RectTransform>().DOAnchorPos(new Vector2(1500, 0), 0.5f)
                .SetEase(Ease.Linear);
            
            currentPage--;
            PageIndicatorPanelView.UpdateView(currentPage);
            
            pageList[currentPage].GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.5f)
                .SetEase(Ease.Linear);
        }
    }

    public void ShowNextPage()
    {
        if (currentPage < pageCount - 1)
        {
          
            pageList[currentPage].GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1500, 0), 0.5f)
                .SetEase(Ease.Linear);
            
            currentPage++;
            PageIndicatorPanelView.UpdateView(currentPage);
            
            pageList[currentPage].GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.5f)
                .SetEase(Ease.Linear);
        }
    }
}
