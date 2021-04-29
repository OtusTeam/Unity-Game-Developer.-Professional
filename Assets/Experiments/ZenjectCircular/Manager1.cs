using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Foundation;
using Zenject;

public class Manager1 : AbstractService<IManager1>, IManager1
{
    [Inject] Manager2Ref manager2Ref;
    bool printed;

    public void PrintManager1()
    {
        DebugOnly.Message("Manager1");
    }

    void Update()
    {
        if (!printed) {
            manager2Ref.instance.PrintManager2();
            printed = true;
        }
    }
}
