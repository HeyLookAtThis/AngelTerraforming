using UnityEngine;

[RequireComponent(typeof(PlayerColliderController))]
public class PlayerParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private PlayerColliderController _playerColliderController;

    private void Awake()
    {
        _playerColliderController = GetComponent<PlayerColliderController>();
    }

    private void OnEnable()
    {
        _playerColliderController.ChangedGrounded += ChangeActivity;
    }

    private void OnDisable()
    {
        _playerColliderController.ChangedGrounded -= ChangeActivity;
    }

    private void Start()
    {
        ChangeActivity(false);
    }

    private void ChangeActivity(bool isActive)
    {
        if (isActive)
            _particleSystem.Play();
        else
            _particleSystem.Stop();
    }
}
