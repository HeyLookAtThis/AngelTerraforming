using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;

    [SerializeField] private RectTransform _playerRectTransform;

    private void OnEnable()
    {
        _player.ChangePosition += SetPlayerPosition;
    }

    private void OnDisable()
    {
        _player.ChangePosition -= SetPlayerPosition;
    }

    private void SetPlayerPosition(Vector3 position)
    {
        _playerRectTransform.anchoredPosition = new Vector2(TransformToLocalCoordinate(position.x), TransformToLocalCoordinate(position.z));
    }

    private float TransformToLocalCoordinate(float coordinate)
    {
        float scale = 5;
        float difference = 25;
        return (coordinate - difference) * scale;
    }
}
