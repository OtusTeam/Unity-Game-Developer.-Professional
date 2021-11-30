using System;
using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus
{
    //Для теста
    public sealed class EnemyBrain : MonoBehaviour
    {
        [Inject]
        private IDynamicObject entity;

        [Inject]
        private IGameManager gameManager;

        [Inject]
        // private IWeaponAttackComponent attackComponent;

        private bool isEnabled;

        [Header("Move")]
        [SerializeField]
        private Transform pointA;

        [SerializeField]
        private Transform pointB;

        private Transform targetPoint;
        
        [Header("Attack")]
        [SerializeField]
        private float attackCountdown;

        private float currentCountdown;

        private void OnEnable()
        {
            this.gameManager.OnStartGame += this.OnStartGame;
        }

        private void OnStartGame()
        {
            this.isEnabled = true;
            this.targetPoint = this.pointA;
        }

        private void FixedUpdate()
        {
            if (!this.isEnabled)
            {
                return;
            }

            this.ProcessMove();
            this.ProcessAttack();
        }

        private void ProcessAttack()
        {
            
        }

        private void ProcessMove()
        {
        }

        private void OnDisable()
        {
            this.gameManager.OnStartGame -= this.OnStartGame;
        }
    }
}