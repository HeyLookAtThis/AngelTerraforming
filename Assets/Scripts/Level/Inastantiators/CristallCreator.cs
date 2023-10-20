using UnityEngine;
using UnityEngine.Events;

public class CristallCreator : MonoBehaviour
{
    [SerializeField] private Cristall _cristall;
    [SerializeField] private Transform _cantainer;

    public Cristall Create(Vector3 position)
    {
        return Instantiate(_cristall, position, Quaternion.identity, _cantainer);
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
