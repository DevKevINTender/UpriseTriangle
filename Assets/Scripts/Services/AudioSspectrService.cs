using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSspectrService : MonoBehaviour
{
    [SerializeField] private GameObject ValuePb;
    [SerializeField] List<GameObject> ListValue = new List<GameObject>();
    private bool canSpawn;
    private int tick;
    private void Start()
    {
        canSpawn = true;
        for (int i = 0; i <= 63; i++)
        {
            //ListValue.Add(Instantiate(ValuePb, new Vector3(-i, -8, 0), Quaternion.identity));
        }
    }

    void Update()
    {
        float[] spectrum = new float[64];
       
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
        ListValue.Clear();
        if (canSpawn)
        {
            for (int i = 0; i <= 63; i++)
            {
                    ListValue.Add(Instantiate(ValuePb, new Vector3(-i, tick, 0), Quaternion.identity));
                    ListValue[i].GetComponent<TextMesh>().text = $"{Math.Round(spectrum[i], 2)}";
                    if (spectrum[i] > 0.05)
                    {
                        ListValue[i].GetComponent<TextMesh>().color = Color.cyan;
                    }
                    if (spectrum[i] > 0.1)
                    {
                        ListValue[i].GetComponent<TextMesh>().color = Color.green;
                    }
                    if (spectrum[i] > 0.2)
                    {
                        ListValue[i].GetComponent<TextMesh>().color = Color.yellow;
                    }
                    if (spectrum[i] > 0.5)
                    {
                        ListValue[i].GetComponent<TextMesh>().color = Color.red;
                    }
                    if (i == 63) ListValue[i].GetComponent<TextMesh>().text = $"t:{tick}";
            }

            canSpawn = false;
            tick++;
            StartCoroutine(tickTime());
        }
        
           
            //ListValue[i].GetComponent<TextMesh>().text = $"{Math.Round(spectrum[i],2)}";
            //Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            //Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            //Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            //Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
    }

    IEnumerator tickTime()
    {
        yield return null;
        //yield return new WaitForSeconds(0.05f);
        canSpawn = true;
    }
}
