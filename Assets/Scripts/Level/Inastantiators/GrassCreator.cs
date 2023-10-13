using UnityEngine;

[RequireComponent(typeof(Ground))]
public class GrassCreator : Instantiator
{
    [SerializeField] private Grass _grass;

    protected override void Create()
    {
        float rayOriginHeingt = 0.2f;

        for (float i = Ground.StartingCoordinate.x; i < Ground.EndingCoordinate.x; i += Distance)
        {
            for (float j = Ground.StartingCoordinate.z; j < Ground.EndingCoordinate.z; j += Distance)
            {
                Vector3 position = new Vector3(i, Ground.StartingCoordinate.y, j);

                if (IsEmptyGround(position, rayOriginHeingt))
                    Instantiate(_grass, position, Quaternion.identity, Container).TurnOff();
            }
        }
    }
}
