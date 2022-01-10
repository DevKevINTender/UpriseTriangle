using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineComponent : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;

    public void SetWinPanel(GameObject _winPanel)
    {
        winPanel = _winPanel;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
            StartCoroutine(MoveToCollision());
        }
    }

    private IEnumerator MoveToCollision()
    {
        yield return new WaitForSeconds(1);
        winPanel.SetActive(true);
    }
}
