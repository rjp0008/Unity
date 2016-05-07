using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    private Button[] buttons;

	// Use this for initialization
	void Start ()
    {
        GetButton("PlayButton").onClick.AddListener(PlayButtonClick);
        GetButton("Exit").onClick.AddListener(ExitButtonClick);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //sourced from https://xinyustudio.wordpress.com/2014/12/12/unity3d-4-6-ui-get-the-button-and-handle-the-click-event-in-code/
    public Button GetButton(string caption)
    {
        var go = GameObject.Find(caption);
        var button = go.GetComponent<Button>();
        return button;
    }

    void PlayButtonClick()
    {
        SceneManager.LoadScene("Scene01");
    }

    void ExitButtonClick()
    {
        Application.Quit();
    }
}
