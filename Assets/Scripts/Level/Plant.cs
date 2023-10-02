using UnityEngine;

public abstract class Plant : MonoBehaviour
{
    public bool IsGreen { get; private set; } = false;

    public abstract void MakeGreen();

    public void SetGreen()
    {
        IsGreen = true;
    }
}
