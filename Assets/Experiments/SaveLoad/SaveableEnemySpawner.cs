using Foundation;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using Zenject;

namespace Experiments
{
    public sealed class SaveableEnemySpawner : MonoBehaviour, ISaveableComponent
    {
        public float SpawnTime = 5.0f;

        [Inject(Id="SaveableEnemyFactory")] IRawFactory factory = default;
        float timeLeft;

        bool ISaveableComponent.Load(uint formatVersion, BinaryReader reader)
        {
            timeLeft = reader.ReadSingle();
            return true;
        }

        bool ISaveableComponent.Save(BinaryWriter writer)
        {
            writer.Write(timeLeft);
            return true;
        }

        void Update()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0.0f) {
                timeLeft = SpawnTime;

                var enemy = (SaveableEnemy)factory.CreateRaw();
                enemy.transform.position = transform.position;
            }
        }
    }
}
