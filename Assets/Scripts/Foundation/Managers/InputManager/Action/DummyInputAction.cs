using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Foundation
{
    public sealed class DummyInputAction : IInputAction
    {
        public static readonly DummyInputAction Instance = new DummyInputAction();

        public bool Active => false;
        public bool Triggered => false;
        public Vector2 Vector2Value => Vector2.zero;
    }
}
