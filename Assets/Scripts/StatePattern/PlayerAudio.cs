using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAudio : MonoBehaviour
{
    private PlayerController _player;

    public StateMachine _stateMachine;

    private AudioSource _audioSource;

    [SerializeField] private AudioClip walking;
    [SerializeField] private AudioClip jump;
    
    private void Awake()
    {
        _player = GetComponent<PlayerController>();
        _stateMachine = _player.PlayerStateMachine;
        _audioSource = GetComponent<AudioSource>();

        _stateMachine.stateChanged += OnStateChanged;
    }

    private void OnStateChanged(IState state)
    {
        if (state == _stateMachine.walkState)
        {
            _audioSource.loop = true;
            _audioSource.clip = walking;
            _audioSource.Play();
        }

        if (state == _stateMachine.jumpState)
        {
            _audioSource.loop = false;
            _audioSource.clip = jump;
            _audioSource.Play();
        }

        if (state == _stateMachine.idleState)
        {
            _audioSource.loop = false;
            _audioSource.Stop();
        }
    }
}
