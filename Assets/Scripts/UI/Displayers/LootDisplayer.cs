using TMPro;
using UnityEngine;

public class LootDisplayer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private int _value;

    public Player Player => _player;

    private void Start()
    {
        _textMeshPro.text = _value.ToString();
    }

    protected void ChangeValue(int value)
    {
        _value = value;
        _textMeshPro.text = _value.ToString();
    }
}
