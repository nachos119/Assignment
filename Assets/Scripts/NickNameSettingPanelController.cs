using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NickNameSettingPanelController : MonoBehaviour
{
    [SerializeField] private Button checkButton = null;
    [SerializeField] private TMP_InputField inputNickName = null;

    private void Awake()
    {
        checkButton.onClick.AddListener(OnClickButton);
        inputNickName.onValueChanged.AddListener(UpdateNickNameInputField);

        checkButton.interactable = false;

        Time.timeScale = 0;

        Cursor.visible = true;
    }

    private void OnClickButton()
    {
        PlayerManager.Instance.SetNickName = inputNickName.text;

        Time.timeScale = 1;
        this.gameObject.SetActive(false);

        PlayerManager.Instance.SetUpdateNickName?.Invoke();

        Cursor.visible = false;
    }

    private void UpdateNickNameInputField(string _text)
    {
        if (string.IsNullOrEmpty(_text))
        {
            checkButton.interactable = false;
        }
        else
        {
            checkButton.interactable = true;
        }
    }
}
