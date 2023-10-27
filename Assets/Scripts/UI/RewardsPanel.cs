using System.Collections;
using TMPro;
using UnityEngine;

public class RewardsPanel : MonoBehaviour
{
    [SerializeField] private Player _player;

    private TextMeshProUGUI _textMeshProUGUI;
    private Coroutine _valueChanger;
    private int _rewards;

    private void Awake()
    {
        _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    public void BeginChangeValue()
    {
        if (_valueChanger != null)
            StopCoroutine(_valueChanger);

        _valueChanger = StartCoroutine(ValueChanger());
    }

    private IEnumerator ValueChanger()
    {
        float seconds = 0.1f;
        var waitTime = new WaitForSeconds(seconds);
        int newRewards = _rewards + _player.CoinsNumber;

        while (_rewards != newRewards)
        {
            _textMeshProUGUI.text = _rewards.ToString();
            _rewards++;
            _player.RemoveCoin();
            yield return waitTime;
        }

        if (_rewards == newRewards)
            yield break;
    }
}
