using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelView : MonoBehaviour
{
    public Text Header;
    public Text Description;

    public void InitView(string Header, string Description)
    {
        this.Header.text = $"{Header}";
        this.Description.text = $"{Description}";
    }

    public void ClosePanel()
    {
        Destroy(this.gameObject);
    }
}
