using System.Collections.Generic;
using UnityEngine;

public class VolcanoCreator : Instantiator
{
    [SerializeField] private Volcano _prefab;

    private List<Volcano> _objects = new List<Volcano>();

    public int ObjectsCount => _objects.Count;

    public IReadOnlyList<Volcano> Objects => _objects;

    public override void Create()
    {
        int count = LevelGenerator.CurrentLevel;

        while (count > 0)
        {
            Volcano newVolcano = Instantiate(_prefab, GetRandomCoordinate(), Quaternion.identity, Container);
            newVolcano.Initialize(GetComponent<Ground>());
            _objects.Add(newVolcano);

            count--;

            if (count == 0)
                break;
        }
    }

    public override void SetDefaultState()
    {
        if (_objects != null)
        {
            foreach (var volcano in _objects)
                volcano.Destroy();

            _objects.Clear();
        }
    }
}
