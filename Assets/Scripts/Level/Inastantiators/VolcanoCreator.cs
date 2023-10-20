using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ground))]
public class VolcanoCreator : Instantiator
{
    [SerializeField] private List<Volcano> _prefabs;

    private List<Volcano> _objects = new List<Volcano>();

    private int _countCoefficient = 6;

    public int VolcanoCount => _countCoefficient - LevelGenerator.CurrentLevel;

    public void ClearLevel()
    {
        if (_objects != null)
        {
            foreach (var volcano in _objects)
                Destroy(volcano);

            _objects.Clear();
        }
    }

    public override void Create()
    {
        int count = VolcanoCount;

        while (count > 0)
        {
            foreach (var volcano in _prefabs)
            {
                Volcano newVolcano = Instantiate(volcano, GetRandomCoordinate(), Quaternion.identity, Container);
                newVolcano.Initialize(GetComponent<Ground>());
                _objects.Add(newVolcano);

                count--;

                if (count == 0)
                    break;
            }
        }
    }
}
