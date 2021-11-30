using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus
{
    //Плохой код для теста
    public sealed class EnemyBrain : MonoBehaviour
    {
        [Inject]
        private IDynamicObject entity;

        [Inject]
        private IGameManager gameManager;

        private bool isEnabled;

        [Header("Move")]
        [SerializeField]
        private Transform pointA;

        [SerializeField]
        private Transform moveTransform;
        
        [SerializeField]
        private Transform pointB;

        private MoveData moveData;

        private Transform targetPoint;
        
        [Header("Attack")]
        [SerializeField]
        private float attackCountdown;

        private float timeToAttack;

        #region Lifecycle

        private void OnEnable()
        {
            this.gameManager.OnStartGame += this.OnStartGame;
        }

        private void OnStartGame()
        {
            this.isEnabled = true;
            
            this.moveData = new MoveData();
            this.targetPoint = this.pointA;

            this.timeToAttack = this.attackCountdown;
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

        private void OnDisable()
        {
            this.gameManager.OnStartGame -= this.OnStartGame;
        }

        #endregion

        private void ProcessAttack()
        {
            this.timeToAttack -= Time.fixedDeltaTime;
            if (this.timeToAttack <= 0)
            {
                this.entity.TryInvokeMethod(ActionKey.ATTACK);
                this.timeToAttack = this.attackCountdown;
            }
        }

        private void ProcessMove()
        {
            var distanceVector = this.targetPoint.position - this.moveTransform.position;
            distanceVector.y = 0;
            
            var distance = distanceVector.magnitude;
            if (distance > 0.1f)
            {
                this.moveData.direction = distanceVector.normalized;
                this.entity.TryInvokeMethod(ActionKey.MOVE, this.moveData);
            }
            else
            {
                if (this.targetPoint == this.pointA)
                {
                    this.targetPoint = this.pointB;
                }
                else
                {
                    this.targetPoint = this.pointA;
                }
            }
        }
    }
}