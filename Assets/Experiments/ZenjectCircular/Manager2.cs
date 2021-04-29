using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Foundation;
using Zenject;

public class Manager2 : AbstractService<IManager2>, IManager2
{
    [Inject] IManager1 manager1;
    [Inject] Manager2Ref manager2Ref;
    bool printed;

    public void PrintManager2()
    {
        DebugOnly.Message("Manager2");
    }

    public override void Start()
    {
        manager2Ref.instance = this;
    }

    void Update()
    {
        if (!printed) {
            manager1.PrintManager1();
            printed = true;
        }
    }
}
