using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExCursorView : MonoBehaviour
{
    public GameObject studyManager;
    private StudyManager sm;
    private SpriteRenderer MainSpriteRenderer;
    public Sprite defaultCursor, discoveredCursor;
    public Sprite circleCursor, discoveredCircleCursor;
    public Sprite squareCursor, discoveredSquareCursor;
    private Renderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        sm = studyManager.GetComponent<StudyManager>();
        _renderer = gameObject.GetComponent<Renderer>();
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        ChangeCursorVisual(sm.selectedVisual);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && !sm.isStartStudy) {
            if(sm.isPractice) {
                sm.isDiscover = !sm.isDiscover;
                sm.isPracticeReady = !sm.isPracticeReady;
                ChangeCursorVisual(sm.selectedVisual);
            } else {
                sm.isDiscover = false;
                ChangeCursorVisual(sm.selectedVisual);
            }
        }
        ChangeCursorVisual(sm.selectedVisual);
    }

    private void ChangeCursorVisual(int _num)
    {
        if (sm.isDiscover)
        {
            switch(_num)
            {
                case 0:
                    MainSpriteRenderer.sprite = discoveredSquareCursor;
                    break;
                case 1:
                    MainSpriteRenderer.sprite = discoveredCircleCursor;
                    break;
                case 2:
                    MainSpriteRenderer.sprite = discoveredCursor;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch(_num)
            {
                case 0:
                    MainSpriteRenderer.sprite = squareCursor;
                    break;
                case 1:
                    MainSpriteRenderer.sprite = circleCursor;
                    break;
                case 2:
                    MainSpriteRenderer.sprite = defaultCursor;
                    break;
                default:
                    break;
            }
        }
    }

    public void EnableView()
    {
        _renderer.enabled = true;
    }

    public void UnableView()
    {
        _renderer.enabled = false;
    }
}
