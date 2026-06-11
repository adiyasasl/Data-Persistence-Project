using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public Text BestScoreText;
    public TMP_InputField NameInputField;

    private void Start()
    {
        if (DataManager.Instance != null)
        {
            BestScoreText.text = $"Best Score : {DataManager.Instance.LoadSavedData().PlayerName} : {DataManager.Instance.LoadSavedData().HighScore}";

            if (DataManager.Instance.LoadSavedData().PlayerName != "")
            {
                NameInputField.text = DataManager.Instance.LoadSavedData().PlayerName;
            }
        }
    }

    public void StartNew()
    {
        if (DataManager.Instance != null)
        {
            DataManager.Instance.SetName(NameInputField.text);
        }

        SceneLoader.Instance.LoadScene(1);
    }
}
