using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChanger : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    [SerializeField] private RewardsPanel _rewardsPanel;
    [SerializeField] private VolcanoDisplayer _displayer;

    private void OnEnable()
    {
        _displayer.Fulled += ShowRewards;
    }

    private void OnDisable()
    {
        _displayer.Fulled -= ShowRewards;
    }

    private void Start()
    {
        _menu.TurnOn();
        _rewardsPanel.TurnOff();
    }

    private void ShowRewards()
    {
        _rewardsPanel.TurnOn();
        _rewardsPanel.BeginChangeValue();
    }
}
