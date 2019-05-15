﻿using System;
using Bushbuckridge.Agents.Collector;
using Bushbuckridge.States;
using Mars.Components.Services.Planning.Implementation;

namespace Bushbuckridge.Actions
{
    public class CutShoots : GoapAction
    {
        private readonly FirewoodCollector _agent;

        public CutShoots(FirewoodCollector agent) : base(agent.AgentStates, 30)
        {
            _agent = agent;

            AddOrUpdatePrecondition(FirewoodState.HasAxe, true);

            AddOrUpdatePrecondition(FirewoodState.AtExploitablePosition, true);
            AddOrUpdatePrecondition(FirewoodState.IsNearAlivewoodTree, true);
            AddOrUpdatePrecondition(FirewoodState.IsNearShoot, true);

            AddOrUpdateEffect(FirewoodState.AtExploitablePosition, false);
            AddOrUpdateEffect(FirewoodState.IsNearAlivewoodTree, false);
            AddOrUpdateEffect(FirewoodState.IsNearShoot, false);

            AddOrUpdateEffect(FirewoodState.WoodstockRaised, true);
        }

        protected override bool ExecuteAction()
        {
            return _agent.CutShoots();
        }
    }
}