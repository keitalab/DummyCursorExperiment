using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserSelectNumberController : MonoBehaviour
{
    public GameObject studyManager;
    private StudyManager sm;
    public InputField userSelectNumberField;
    public Text userSelectNumberText;
    // Start is called before the first frame update
    void Start()
    {
        sm = studyManager.GetComponent<StudyManager>();
        userSelectNumberField = userSelectNumberField.GetComponent<InputField>();
        userSelectNumberText = userSelectNumberText.GetComponent<Text>();

        userSelectNumberField.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputUserSelectNumber()
    {
        userSelectNumberText.text = userSelectNumberField.text;
        int _temp = (userSelectNumberText.text == "" || userSelectNumberText.text == null) ? 1 : int.Parse(userSelectNumberText.text);
        sm.userSelectCursorNum = _temp;
    }

    public void FocusOnField()
    {
        userSelectNumberField.ActivateInputField();
    }
}
