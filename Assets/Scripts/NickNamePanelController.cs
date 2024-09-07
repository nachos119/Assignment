using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NickNamePanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text nickNameText = null;

    private Transform mainCamara = null;

    private void Start()
    {
        mainCamara = Camera.main.transform;
    }

    public void UpdateNickName(string _name)
    {
        nickNameText.text = _name;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + (mainCamara.rotation * Vector3.forward),
            mainCamara.rotation * Vector3.up);
    }
}
