using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Exit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button exitButton = GetComponent<Button>();

        // Add a listener to the button's onClick event
        exitButton.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void ExitGame()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif

        // For testing in the Unity Editor
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
