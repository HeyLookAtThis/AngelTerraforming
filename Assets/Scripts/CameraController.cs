using UnityEngine;

[RequireComponent(typeof(Camera), typeof(AudioListener))]
public class CameraController : MonoBehaviour
{
    private Camera _camera;
    private AudioListener _listener;

    private void Awake()
    {
        _listener = GetComponent<AudioListener>();
        _camera = GetComponent<Camera>();
    }

    public void Activate()
    {
        _camera.enabled = true;
        _listener.enabled = true;
    }

    public void Deactivate()
    {
        _listener.enabled = false;
        _camera.enabled = false;
    }
}
