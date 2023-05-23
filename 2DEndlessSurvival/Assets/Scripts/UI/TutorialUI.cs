using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public bool isDisplay = true;
    private void Start()
    {
        GameInput.Instance.OpenTutorialMenu += GameInput_OpenTutorialMenu;
        Show();
    }

    private void GameInput_OpenTutorialMenu(object sender, System.EventArgs e)
    {
        ToggleTab();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ToggleTab()
    {
        isDisplay = !isDisplay;
        if (isDisplay)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
