using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChaser : MonoBehaviour
{
    public Vector3 PositionOffset = Vector3.zero;
    public float LerpSpeed = 5f;

    protected Vector2 targetPos = Vector2.zero;
    protected Vector2 _initialOffset = Vector2.zero;

    private PlayerWeaponHandler _playerWeaponHandler;

    private void Start()
    {
        _playerWeaponHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeaponHandler>();
    }

    private void FixedUpdate()
    {
        if (_playerWeaponHandler == null)
            return;

        targetPos = Vector2.Lerp(targetPos, _playerWeaponHandler.AimPosition(), Time.deltaTime * LerpSpeed); //Lerp is pretty much a calculation of, for example, a to b distance, where Lerp will gives you the exact middle of a and b

        transform.position = new Vector3(targetPos.x + PositionOffset.x, targetPos.y + PositionOffset.y, PositionOffset.z);
    }
}
