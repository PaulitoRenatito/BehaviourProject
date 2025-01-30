using Behaviour;
using Behaviour.Nodes;
using Behaviour.Strategies;
using Tasks;
using UnityEngine.AI;

namespace Trees
{
    public static class TreeTasks
    {
        public static BehaviourTree GetTreeTasks(
            NavMeshAgent agent,
            Task fightTask,
            Task manufactorTask,
            Task plantTask,
            Task healTask,
            Task restTask
        )
        {
            Node gotoFight = new Leaf("Go to fight", new GotoDestinationStrategy(agent, fightTask.Waypoint));
            Node doFight = new Leaf("Do Task fight", new DoTaskStrategy(fightTask));
            Node sequenceFight = new Sequence("Sequence Fight");
            sequenceFight.AddChild(gotoFight);
            sequenceFight.AddChild(doFight);

            Node gotoManufactor = new Leaf("Go to manufactor",
                new GotoDestinationStrategy(agent, manufactorTask.Waypoint));
            Node doManufactor = new Leaf("Do Task manufactor", new DoTaskStrategy(manufactorTask));
            Node sequenceManufactor = new Sequence("Sequence Manufactor");
            sequenceManufactor.AddChild(gotoManufactor);
            sequenceManufactor.AddChild(doManufactor);

            Node gotoPlant = new Leaf("Go to plant", new GotoDestinationStrategy(agent, plantTask.Waypoint));
            Node doPlant = new Leaf("Do Task plant", new DoTaskStrategy(plantTask));
            Node sequencePlant = new Sequence("Sequence Plant");
            sequencePlant.AddChild(gotoPlant);
            sequencePlant.AddChild(doPlant);

            Node gotoHeal = new Leaf("Go to heal", new GotoDestinationStrategy(agent, healTask.Waypoint));
            Node doHeal = new Leaf("Do Task heal", new DoTaskStrategy(healTask));
            Node sequenceHeal = new Sequence("Sequence Heal");
            sequenceHeal.AddChild(gotoHeal);
            sequenceHeal.AddChild(doHeal);

            Node gotoRest = new Leaf("Go to rest", new GotoDestinationStrategy(agent, restTask.Waypoint));
            Node doRest = new Leaf("Do Task rest", new DoTaskStrategy(restTask));
            Node sequenceRest = new Sequence("Sequence Rest");
            sequenceRest.AddChild(gotoRest);
            sequenceRest.AddChild(doRest);

            Node sequence = new Sequence("Sequence Main");
            sequence.AddChild(sequenceFight);
            sequence.AddChild(sequenceManufactor);
            sequence.AddChild(sequencePlant);
            sequence.AddChild(sequenceHeal);
            sequence.AddChild(sequenceRest);

            Node utilFail = new UntilFail("Until Fail");
            utilFail.AddChild(sequence);

            BehaviourTree behaviourTree = new BehaviourTree("Root");
            behaviourTree.AddChild(utilFail);

            return behaviourTree;
        }
    }
}
