using UnityEngine;
using UnityEngine.Events;

public class IceCristallCreator : MonoBehaviour
{
    [SerializeField] private uint _count;
    [SerializeField] private IceCristall _cristall;
    [SerializeField] private Transform _cantainer;

    public uint Count => _count;

    public void Create(Vector3 position)
    {
        Instantiate(_cristall, position, Quaternion.identity, _cantainer);
        _count--;
    }

    public Vector3 GetRandomPosition(Vector3 treePosition)
    {
        float minDistance = 0.5f;
        float maxDistance = 1f;

        Vector3 randomPosition = new Vector3();

        randomPosition.y = treePosition.y;
        randomPosition.x += treePosition.x + Random.Range(minDistance, maxDistance) * GetRandomMultiplier();
        randomPosition.z += treePosition.z + Random.Range(minDistance, maxDistance) * GetRandomMultiplier();

        return randomPosition;
    }

    private int GetRandomMultiplier()
    {
        int positiveMultiplier = 1;
        int negativeMultiplier = -positiveMultiplier;
        
        float randomValue = Random.Range(positiveMultiplier, negativeMultiplier);

        if (randomValue < 0)
            return negativeMultiplier;

        return positiveMultiplier;
    }
}
