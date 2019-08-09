using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateUserCursorNumberView : MonoBehaviour
{
    public GameObject userCursor;
    public GameObject studyManager;
    private StudyManager sm;
    // Start is called before the first frame update
    void Start()
    {
        sm = studyManager.GetComponent<StudyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sm.isStartSession)
        {
            float x = userCursor.transform.position.x;
            float y = userCursor.transform.position.y;
            Vector3 pos = Camera.main.WorldToScreenPoint(userCursor.transform.position);
            pos.x = pos.x - Screen.width/2;
            pos.y = pos.y - Screen.height/2 + 1;
            this.GetComponent<RectTransform>().localPosition = pos;
            this.GetComponent<Text>().text = sm.selfCursorNum.ToString();
        }
    }
}
