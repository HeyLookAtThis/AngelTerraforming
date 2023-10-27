using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(AudioSource))]
public abstract class MenuButton : MonoBehaviour
{
    [SerializeField] private AudioClip _audio;

    private AudioSource _audioSource;
    private Button _button;
    

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _button = GetComponent<Button>();
        _audioSource.clip = _audio;
        _audioSource.playOnAwake = false;
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        Unsubscrube();
    }

    public virtual void Initialize()
    {
        AddAction(_audioSource.Play);
    }

    public virtual void Unsubscrube()
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

    public void Click()
    {
        _button.onClick?.Invoke();
    }

    public void Clicked()
    {
        Debug.Log("Clicked!");
    }
}
