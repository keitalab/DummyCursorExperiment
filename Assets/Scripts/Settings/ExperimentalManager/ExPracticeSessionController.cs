using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExPracticeSessionController : MonoBehaviour
{
    public GameObject experimentalManager;
    private ExperimentalManager em;
    public InputField practiceSessionField, tiralField;
    public Text practiceSessionText, trialText;
    // Start is called before the first frame update
    void Start()
    {
        em = experimentalManager.GetComponent<ExperimentalManager>();
        practiceSessionField = practiceSessionField.GetComponent<InputField>();
        practiceSessionText = practiceSessionText.GetComponent<Text>();
        tiralField = tiralField.GetComponent<InputField>();
        trialText = trialText.GetComponent<Text>();

        practiceSessionField.text = (em.practiceSession).ToString();
        tiralField.text = (em.trial).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputPracticeSession()
    {
        practiceSessionText.text = practiceSessionField.text;
        int _temp = (practiceSessionText.text == "" || practiceSessionText.text == null) ? 1 : int.Parse(practiceSessionText.text);
        em.practiceSession = _temp;
    }

    public void InputTrial()
    {
        trialText.text = tiralField.text;
        int _temp = (trialText.text == "" || trialText.text == null) ? 1 : int.Parse(trialText.text);
        em.trial = _temp;
    }
}
