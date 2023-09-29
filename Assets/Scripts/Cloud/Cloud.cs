using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.ShaderData;

[RequireComponent(typeof(Reservoir), typeof(Resizer))]
public class Cloud : MonoBehaviour
{
    [SerializeField] private float _level;
    [SerializeField] private PlayerColliderController _playerCollider;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Transform _targetPlace;

    private Scanner _scanner;
    private Reservoir _reservoir;
    private Resizer _resizer;

    private bool _isAboveTheWater;
    private bool _locatedUnderPlayer;

    public PlayerColliderController PlayerCollider => _playerCollider;

    public PlayerMovement PlayerMovement => _playerMovement;

    public Transform TargetPlace => _targetPlace;

    public bool LocatedUnderPlayer => _locatedUnderPlayer;

    private void Awake()
    {
        _reservoir = GetComponent<Reservoir>();
        _resizer = GetComponent<Resizer>();
        _scanner = GetComponentInChildren<Scanner>();

        _scanner.SetLevel(_level);
    }

    private void OnEnable()
    {
        _scanner.FoundFlower += TryGrowFlower;
        _scanner.FoundGrass += TryGrowGrass;
        _scanner.FoundTree += TryMakeGreenTree;
        _scanner.FoundWater += SetBeAboveTheWater;
    }

    private void OnDisable()
    {
        _scanner.FoundFlower -= TryGrowFlower;
        _scanner.FoundGrass -= TryGrowGrass;
        _scanner.FoundTree -= TryMakeGreenTree;
        _scanner.FoundWater -= SetBeAboveTheWater;
    }

    private void Update()
    {
        if (_locatedUnderPlayer == false)
            return;

        if (_isAboveTheWater == false)
            return;

        DrinkWater();
    }

    public void TurnOnLocationUnderPlayer()
    {
        _locatedUnderPlayer = true;
        _scanner.Activate();
    }

    public void TurnOffLacationUnderPlayer()
    {
        _locatedUnderPlayer = false;
        _scanner.Deactivate();
    }

    private void TryGrowGrass(Grass grass)
    {
        _isAboveTheWater = false;

        if(_reservoir.IsEmpty != true)
        {
            grass.Grow();
            MakeRain();
        }
    }

    private void TryGrowFlower(Flower flower)
    {
        _isAboveTheWater = false;

        if (_reservoir.IsEmpty != true)
        {
            flower.TurnOnVisible();
            MakeRain();
        }
    }

    private void TryMakeGreenTree(Tree tree)
    {
        _isAboveTheWater = false;

        if (_reservoir.IsEmpty != true)
        {
            tree.MakeGreenAround();
            MakeRain();
        }
    }

    private void MakeRain()
    {
        _reservoir.PourWater();
        _resizer.DecreaseSize();
    }

    private void DrinkWater()
    {
        _reservoir.ReplenishWater();
        _resizer.IncreaseSize();
    }

    private void SetBeAboveTheWater()
    {
        _isAboveTheWater = true;
    }
}
