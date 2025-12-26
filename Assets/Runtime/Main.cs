using System;
using Runtime.Infrastructure;
using UnityEngine;
using Cursor = Runtime.Infrastructure.Cursor;

namespace Runtime
{
    public class Main: MonoBehaviour
    {
        private void Awake()
        {
            UIResources.Initialize();
        }

        private void Start()
        {
            Cursor.Instance.Initialize();
        }
    }
}