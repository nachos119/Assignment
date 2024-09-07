using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private readonly string moveAxisName = "Vertical";
    [SerializeField] private readonly string rotateAxisName = "Horizontal";
    [SerializeField] private readonly string attackButtonName = "Attack";

    private PlayerManager playerManager = null;

    public float move { get; private set; }
    public float rotate { get; private set; }
    public bool attack { get; private set; }

    private void Awake()
    {
        playerManager = PlayerManager.Instance;
    }

    private void Update()
    {
        move = Input.GetAxis(moveAxisName);
        rotate = Input.GetAxis(rotateAxisName);

        if (!playerManager.SetOtherAction)
            attack = Input.GetButton(attackButtonName);
    }
}
