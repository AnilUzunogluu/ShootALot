using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    private string buttonName;
    private Button _button;
    private void Awake()
    {
        buttonName = gameObject.name;
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        GetClickEvent();
    }

    private void GetClickEvent()
    {
        switch (buttonName)
        {
            case "Start":
                _button.onClick.AddListener(LevelManager.Instance.LoadGame);
                break;
            case "Exit":
                _button.onClick.AddListener(LevelManager.Instance.QuitGame);
                break;
            case "Play Again":
                _button.onClick.AddListener(LevelManager.Instance.LoadGame);
                break;
            case "Main Menu":
                _button.onClick.AddListener(LevelManager.Instance.LoadMainMenu);
                break;
            default:
                    break;
        }
    }
}
