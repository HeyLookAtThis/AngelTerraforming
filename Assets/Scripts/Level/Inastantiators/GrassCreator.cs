using System.Collections.Generic;
using UnityEngine;

public class GrassCreator : Instantiator
{
    [SerializeField] private float _distance;
    [SerializeField] private Grass _grass;

    private List<Grass> _grassList = new List<Grass>();

    private bool _isCreated;

    public override void Create()
    {
        if(_isCreated) 
            return;

        for (float i = Grid.Start.x; i < Grid.End.x; i += _distance)
        {
            for (float j = Grid.Start.z; j < Grid.End.z; j += _distance)
            {
                Vector3 position = new Vector3(i, Grid.Start.y, j);

                if (IsEmptyGround(position))
                {
                    Grass grass = Instantiate(_grass, position, Quaternion.identity, Container);
                    grass.TurnOff();
                    _grassList.Add(grass);
                }
            }
        }

        _isCreated = true;
    }

    public override void SetDefaultState()
    {
        foreach (var grass in _grassList)
            grass.TurnOff();
    }


    private bool IsEmptyGround(Vector3 position)
    {
        float rayOriginHeight = 0.2f;

        Vector3 rayPoint = new Vector3(position.x, position.y + rayOriginHeight, position.z);
        Physics.Raycast(rayPoint, Vector3.down, out RaycastHit hit);

        if (hit.collider != null)
            if (hit.collider.TryGetComponent<Ground>(out Ground ground))
                return true;

        return false;
    }
}
