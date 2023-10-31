using UnityEngine;

public class PanelChanger : MonoBehaviour
{
    [SerializeField] private RewardsPanel _rewardsPanel;
    [SerializeField] private LevelFinisher _levelFinisher;

    private void OnEnable()
    {
        _levelFinisher.Begun += ShowRewards;
    }

    private void OnDisable()
    {
        _levelFinisher.Begun -= ShowRewards;
    }

    private void Start()
    {
        _rewardsPanel.TurnOff();
    }

    private void ShowRewards()
    {
        _rewardsPanel.TurnOn();
        _rewardsPanel.BeginChangeValue();
    }
}
