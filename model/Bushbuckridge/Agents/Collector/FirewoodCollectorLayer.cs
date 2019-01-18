﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOAP_Test.Agents;
using Mars.Components.Agents;
using Mars.Components.Environments;
using Mars.Components.Services;
using Mars.Interfaces.Agent;
using Mars.Interfaces.Environment;
using Mars.Interfaces.Layer;
using Mars.Interfaces.Layer.Initialization;

namespace FirewoodCollectionv2.Agents.Collector
{
    public class FirewoodCollectorLayer : ISteppedActiveLayer

    {
        private readonly ExploitableTreeLayer _treeLayer;
        private readonly GeoGridEnvironment<GeoAgent<FirewoodCollector>> _environment;
        
        private ConcurrentDictionary<Guid, FirewoodCollector> _agents;
        private long CurrentTick { get; set; }

        public FirewoodCollectorLayer(ExploitableTreeLayer treeLayer)
        {
            _treeLayer = treeLayer;
            _environment = new GeoGridEnvironment<GeoAgent<FirewoodCollector>>(-24.8239, -24.8690, 31.1944, 31.2436, 1000);
        }

        public bool InitLayer(TInitData layerInitData, RegisterAgent registerAgentHandle,
            UnregisterAgent unregisterAgentHandle)
        {
            var agentInitConfig = layerInitData.AgentInitConfigs.FirstOrDefault();
            _agents = AgentManager.GetAgentsByAgentInitConfig<FirewoodCollector>(agentInitConfig, registerAgentHandle, unregisterAgentHandle,
                new List<ILayer>
                {
                    _treeLayer,
                    this
                }, _environment);

            Console.WriteLine("[FirewoodCollectorLayer]: Created Agents: " + _agents.Count);
            return true;
        }

        public long GetCurrentTick()
        {
            return CurrentTick;
        }

        public void SetCurrentTick(long currentStep)
        {
            CurrentTick = currentStep;
            Console.WriteLine("-------------- "+currentStep +" --------------");
        }

        public void Tick()
        {
        }

        public void PreTick()
        {
        }

        public void PostTick()
        {
        }
    }
}