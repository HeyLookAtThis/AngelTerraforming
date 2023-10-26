using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(AudioSource))]
public class MenuButton : MonoBehaviour
{
    [SerializeField] private AudioClip _audio;
    [SerializeField] private string _text;

    private TextMeshProUGUI _textMeshPro;
    private Button _button;
    private AudioSource _audioSource;
    

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        _button = GetComponent<Button>();
        _audioSource.clip = _audio;
        _audioSource.playOnAwake = false;
        _textMeshPro.SetText(_text);
    }

    private void OnEnable()
    {
        AddAction(_audioSource.Play);
    }

    private void OnDisable()
    {
        RemoveAction(_audioSource.Play);
    }

    public void AddAction(UnityAction action)
    {
        _button?.onClick.AddListener(action);
    }

    public void RemoveAction(UnityAction action)
    {
        _button?.onClick.RemoveListener(action);
    }
}
