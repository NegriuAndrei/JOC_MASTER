using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class DialogManager : MonoBehaviour
{

    public static DialogManager instance;
    public GameObject dialogPanel;
    public TMP_Text dialogText;
    public string[] sentences;
    public int currentSentence;
    public bool justStarted;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogPanel.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(1))
            {
                if (!justStarted)
                {
                    currentSentence++;

                    if (currentSentence >= sentences.Length)
                    {
                        dialogPanel.SetActive(false);
                    }
                    else
                    {
                        dialogText.text = sentences[currentSentence];
                    }
                }
                else 
                {
                    justStarted = false;
                }
            }
        }
    }
    public void ShowDialog(string[] newLines)
    {
        sentences = newLines;
        currentSentence = 0;
        dialogText.text = sentences[currentSentence];
        dialogPanel.SetActive(true);
        justStarted = true;
    }
}
