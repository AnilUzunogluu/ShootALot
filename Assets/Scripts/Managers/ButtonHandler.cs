using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    private string _buttonName;
    private Button _button;
    private void Awake()
    {
        _buttonName = gameObject.name;
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        GetClickEvent();
    }

    private void GetClickEvent()
    {
        switch (_buttonName)
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
