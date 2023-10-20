using UnityEngine;

public class GrassCreator : Instantiator
{
    [SerializeField] private Grass _grass;

    private void Start()
    {
        Create();
    }

    public override void Create()
    {
        float rayOriginHeingt = 0.2f;

        for (float i = Grid.Start.x; i < Grid.End.x; i += Distance)
        {
            for (float j = Grid.Start.z; j < Grid.End.z; j += Distance)
            {
                Vector3 position = new Vector3(i, Grid.Start.y, j);

                if (IsEmptyGround(position, rayOriginHeingt))
                    Instantiate(_grass, position, Quaternion.identity, Container).TurnOff();
            }
        }
    }
}
