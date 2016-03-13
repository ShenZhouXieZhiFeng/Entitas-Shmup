﻿using Entitas;
using NUnit.Framework;
using UnityEngine;

public class RenderPositionSystemsTests {

    [Test]
    public void UpdatesView()
    {
        // given
        var pool = TestHelper.CreateCorePool();
        var view = new GameObject();
        var controller = view.AddComponent<ViewController>();
        var entity = pool.CreateEntity()
            .AddPosition(Vector3.one)
            .AddView(controller);

        var system = (IExecuteSystem)pool.CreateSystem<RenderPositionSystem>();

        // when 
        var newPos = new Vector3(2, 3, 4);
        entity.ReplacePosition(newPos);
        system.Execute();

        // then
        Assert.AreEqual(newPos, view.transform.position);
    }
}