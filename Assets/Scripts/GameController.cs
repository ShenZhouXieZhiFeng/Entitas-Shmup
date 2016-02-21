﻿using UnityEngine;
using Entitas;

public class GameController : MonoBehaviour {

    Systems _systems;

    void Awake() {
        Application.targetFrameRate = 60;
    }

    void Start() {
        _systems = createSystems(Pools.core, Pools.input, Pools.bullets);
        _systems.Initialize();
    }

    void Update() {
        _systems.Execute();
    }

    Systems createSystems(Pool corePool, Pool inputPool, Pool bulletsPool) {
        #if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
        return new Entitas.Unity.VisualDebugging.DebugSystems()
        #else
        return new Systems()
        #endif

        // Initialize
        .Add(corePool.CreateSystem<CreatePlayerSystem>())

        // Input
        .Add(inputPool.CreateSystem(new ProcessMoveInputSystem(corePool)))
        .Add(inputPool.CreateSystem(new ProcessShootInputSystem(corePool, bulletsPool)))

        // Update core
        .Add(corePool.CreateSystem<VelocitySystem>())

        // Update bullets
        .Add(bulletsPool.CreateSystem<VelocitySystem>())

        // Render core
        .Add(corePool.CreateSystem<AddViewSystem>())
        .Add(corePool.CreateSystem<RenderPositionSystem>())

        // Render bullets
        .Add(bulletsPool.CreateSystem<AddViewSystem>())
        .Add(bulletsPool.CreateSystem<RenderPositionSystem>());
    }
}