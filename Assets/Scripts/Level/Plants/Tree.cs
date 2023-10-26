using System.Collections.Generic;
using UnityEngine;

public class Tree : Plant
{
    [SerializeField] private float _radius;
    [SerializeField] private List<GameObject> _emptyTunks;
    [SerializeField] private List<GameObject> _greenTrunks;

    private bool _isTrunkEmpty => !IsGreen;

    private void Start()
    {
        ChangeLeaves();
    }

    public override void MakeGreen()
    {
        if (IsGreen == false)
        {
            SetGreen();
            ChangeLeaves();
            RunPlantAround();
        }    
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void ChangeLeaves()
    {
        foreach (var tunk in _emptyTunks)
            tunk.SetActive(_isTrunkEmpty);

        foreach (var tunk in _greenTrunks)
            tunk.SetActive(IsGreen);
    }

    private void RunPlantAround()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<Grass>(out Grass grass))
            {
                if (grass.IsGreen == false)
                    grass.MakeGreen();
            }
            else if (collider.TryGetComponent<Flower>(out Flower flower))
            {
                if (flower.IsGreen == false)
                    flower.MakeGreen();
            }
            else if(collider.TryGetComponent<Coin>(out Coin coin))
            {
                coin.TurnOn();
            }
            else if(collider.TryGetComponent<Cristall>(out Cristall cristall))
            {
                cristall.TurnOn();
            }
        }
    }
}
