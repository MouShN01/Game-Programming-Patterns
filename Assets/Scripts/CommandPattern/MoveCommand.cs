using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private PlayerMover _playerMover;
    private Vector3 _movement;

    public MoveCommand(PlayerMover player, Vector3 moveVector)
    {
        this._playerMover = player;
        this._movement = moveVector;
    }

    public void Execute()
    {
        _playerMover.Move(_movement);
    }

    public void Undo()
    {
        _playerMover.Move(-_movement);
    }
}
