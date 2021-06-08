using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    Transform settingsPanel;
    Event keyEvent;
    TextMeshProUGUI buttonText;
    KeyCode newKey;

    bool waitingForKey;

    void Awake()
    {
        settingsPanel = transform.Find("Buttons");
        waitingForKey = false;

        for (int i = 0; i < 5; i++)
        {
            if (settingsPanel.GetChild(i).name == "RightKey")
                settingsPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().SetText(GameManager.GM.rightMove.ToString());
            else if (settingsPanel.GetChild(i).name == "LeftKey")
                settingsPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().SetText(GameManager.GM.leftMove.ToString());
            else if (settingsPanel.GetChild(i).name == "JumpKey")
                settingsPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().SetText(GameManager.GM.jump.ToString());
            else if (settingsPanel.GetChild(i).name == "FireKey")
                settingsPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().SetText(GameManager.GM.fire.ToString());
            else if (settingsPanel.GetChild(i).name == "PauseKey")
                settingsPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().SetText(GameManager.GM.pause.ToString());
        }

        volumeSlider.value = PlayerPrefs.GetFloat("volumeMain");
    }


    private void OnGUI()
    {
        keyEvent = Event.current;

        if (keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }
    public void StartAssigment(string keyName)
    {
        if (!waitingForKey)
        {
            StartCoroutine(AssihmentKeyCourutine(keyName));
        }
    }
    public void SendText(TextMeshProUGUI text)
    {
        buttonText = text;
    }
    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
            yield return null;
    }
    public IEnumerator AssihmentKeyCourutine(string keyName)
    {
        buttonText.fontSize = 80f;
        waitingForKey = true;

        yield return WaitForKey();

        switch (keyName)
        {
            case "right":
                GameManager.GM.rightMove = newKey;
                buttonText.text = GameManager.GM.rightMove.ToString();
                buttonText.fontSize = 100f;
                PlayerPrefs.SetString("rightKey", GameManager.GM.rightMove.ToString());
                break;
            case "left":
                GameManager.GM.leftMove = newKey;
                buttonText.text = GameManager.GM.leftMove.ToString();
                buttonText.fontSize = 100f;
                PlayerPrefs.SetString("leftKey", GameManager.GM.leftMove.ToString());
                break;
            case "jump":
                GameManager.GM.jump = newKey;
                buttonText.text = GameManager.GM.jump.ToString();
                buttonText.fontSize = 100f;
                PlayerPrefs.SetString("jumpKey", GameManager.GM.jump.ToString());
                break;
            case "fire":
                GameManager.GM.fire = newKey;
                buttonText.text = GameManager.GM.fire.ToString();
                buttonText.fontSize = 100f;
                PlayerPrefs.SetString("fireKey", GameManager.GM.fire.ToString());
                break;
            case "pause":
                GameManager.GM.pause = newKey;
                buttonText.text = GameManager.GM.pause.ToString();
                buttonText.fontSize = 100f;
                PlayerPrefs.SetString("pauseKey", GameManager.GM.pause.ToString());
                break;
        }

        yield return null;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("volumeMain", volume);
    }
}
