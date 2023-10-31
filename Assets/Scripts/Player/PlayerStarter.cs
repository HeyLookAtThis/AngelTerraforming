using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerStarter : MoverToStart
{
    public override void SetPosition()
    {
        GetComponent<PlayerMovement>().TurnOffGravity();
        base.SetPosition();
    }
}