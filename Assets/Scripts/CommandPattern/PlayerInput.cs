using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerInput : MonoBehaviour
{
    private Vector3 _moveVector;
    private PlayerMover _playerMover;

    private void Awake()
    {
        _playerMover = gameObject.GetComponent<PlayerMover>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RunPlayerCommand(_playerMover, Vector3.left);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            RunPlayerCommand(_playerMover, Vector3.right);
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            RunPlayerCommand(_playerMover, Vector3.forward);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            RunPlayerCommand(_playerMover, Vector3.back);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            CommandInvoker.UndoCommand();
        }
    }
    private void RunPlayerCommand(PlayerMover playerMover, Vector3 movement)
    {
        if (playerMover == null)
        {
            return;
        }

        ICommand command = new MoveCommand(playerMover, movement);
        CommandInvoker.ExecuteCommand(command);
    }
}
