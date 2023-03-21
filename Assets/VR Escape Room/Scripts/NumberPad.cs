using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberPad : MonoBehaviour
{
    public string Sequence;
    public GameObject CardSpawnerPrefab, spawnPosition;
    public TextMeshProUGUI InputDisplayText;
    private string m_CurrentEnteredCode = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed(int valuePressed)
    {
        m_CurrentEnteredCode += valuePressed;

        if (m_CurrentEnteredCode.Length == Sequence.Length)
        {
            if (m_CurrentEnteredCode == Sequence)
            {
                Instantiate(CardSpawnerPrefab, spawnPosition.transform);
                InputDisplayText.SetText("Valid code !");
            }
            else
            {
                InputDisplayText.SetText("Invalid code !");
                m_CurrentEnteredCode = "";
            }
        }
    }
}
