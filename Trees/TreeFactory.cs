using System.Collections.Generic;
using Behaviour;
using Behaviour.Blackboard;
using Behaviour.Nodes;
using Behaviour.Strategies;
using Cities;
using Tasks;
using UnityEngine.AI;

namespace Trees
{
    public static class TreeFactory
    {
        public static BehaviourTree GetStaticTreeTasks(
            NavMeshAgent agent,
            List<Task> tasks)
        {
            Node sequence = new Sequence("Sequence Main");

            foreach (Task task in tasks)
            {
                Node gotoTask = new Leaf("Go to " + task.Name, new GotoDestinationStrategy(agent, task.Waypoint));
                Node doTask = new Leaf("Do Task " + task.Name, new DoTaskStrategy(task));
                Node sequenceTask = new Sequence("Sequence " + task.Name);
                sequenceTask.AddChild(gotoTask);
                sequenceTask.AddChild(doTask);

                sequence.AddChild(sequenceTask);
            }

            Node utilFail = new UntilFail("Until Fail");
            utilFail.AddChild(sequence);

            BehaviourTree behaviourTree = new BehaviourTree("Root");
            behaviourTree.AddChild(utilFail);

            return behaviourTree;
        }

        public static BehaviourTree GetDynamicTreeTasks(
            NavMeshAgent agent,
            BlackboardTask blackboardTask,
            Task fightTask,
            Task manufactorTask,
            Task plantTask,
            Task healTask,
            Task restTask
        )
        {
            // Heal
            Node healCondition = new Leaf("Check Health", new ConditionStrategy(() => blackboardTask.Health <= 20));
            Node gotoHeal = new Leaf("Go to Heal", new GotoDestinationStrategy(agent, healTask.Waypoint));
            Node doHeal = new Leaf("Do Heal", new DoTaskStrategy(healTask, blackboardTask));

            Node healSequence = new Sequence("Sequence Heal");
            healSequence.AddChild(healCondition);
            healSequence.AddChild(gotoHeal);
            healSequence.AddChild(doHeal);

            // Rest
            Node restCondition = new Leaf("Check Stamina", new ConditionStrategy(() => blackboardTask.Stamina <= 15));
            Node gotoRest = new Leaf("Go to Rest", new GotoDestinationStrategy(agent, restTask.Waypoint));
            Node doRest = new Leaf("Do Rest", new DoTaskStrategy(restTask, blackboardTask));

            Node restSequence = new Sequence("Sequence Rest");
            restSequence.AddChild(restCondition);
            restSequence.AddChild(gotoRest);
            restSequence.AddChild(doRest);

            // Fight
            Node fightCondition = new Leaf("Can Fight",
                new ConditionStrategy(() => blackboardTask.Health >= 40 && blackboardTask.Stamina > 25));
            Node gotoFight = new Leaf("Go to Fight", new GotoDestinationStrategy(agent, fightTask.Waypoint));
            Node doFight = new Leaf("Do Fight", new DoTaskStrategy(fightTask, blackboardTask));

            Node fightSequence = new Sequence("Sequence Fight");
            fightSequence.AddChild(fightCondition);
            fightSequence.AddChild(gotoFight);
            fightSequence.AddChild(doFight);

            // Manufactor
            Node manufactorCondition = new Leaf("Can Manufactor",
                new ConditionStrategy(() => blackboardTask.Stamina > 5 && (60 - blackboardTask.ElapsedTime) >= 5));
            Node gotoManufactor = new Leaf("Go to Manufactor",
                new GotoDestinationStrategy(agent, manufactorTask.Waypoint));
            Node doManufactor = new Leaf("Do Manufactor", new DoTaskStrategy(manufactorTask, blackboardTask));

            Node manufactorSequence = new Sequence("Sequence Manufactor");
            manufactorSequence.AddChild(manufactorCondition);
            manufactorSequence.AddChild(gotoManufactor);
            manufactorSequence.AddChild(doManufactor);

            // Plant
            Node plantCondition = new Leaf("Can Plant",
                new ConditionStrategy(() => blackboardTask.Stamina > 10 && (60 - blackboardTask.ElapsedTime) >= 4));
            Node gotoPlant = new Leaf("Go to Plant", new GotoDestinationStrategy(agent, plantTask.Waypoint));
            Node doPlant = new Leaf("Do Plant", new DoTaskStrategy(plantTask, blackboardTask));

            Node plantSequence = new Sequence("Sequence Plant");
            plantSequence.AddChild(plantCondition);
            plantSequence.AddChild(gotoPlant);
            plantSequence.AddChild(doPlant);

            // Profitable Tasks
            Node profitableTask = new Selector("Selector Profitable Task");
            profitableTask.AddChild(fightSequence);
            profitableTask.AddChild(manufactorSequence);
            profitableTask.AddChild(plantSequence);

            Node mainSelector = new Selector("Main Root");
            mainSelector.AddChild(healSequence);
            mainSelector.AddChild(restSequence);
            mainSelector.AddChild(profitableTask);

            Node utilFail = new UntilFail("Until Fail");
            utilFail.AddChild(mainSelector);

            BehaviourTree behaviourTree = new BehaviourTree("Root");
            behaviourTree.AddChild(utilFail);

            return behaviourTree;
        }

        public static BehaviourTree GetTreeCities(
            NavMeshAgent agent,
            List<City> cities)
        {
            Node sequence = new Sequence("Sequence Main");

            foreach (City city in cities)
            {
                Node gotoCity = new Leaf("Go to " + city.Name, new GotoDestinationStrategy(agent, city.Waypoint));
                sequence.AddChild(gotoCity);
            }

            Node utilFail = new UntilFail("Until Fail");
            utilFail.AddChild(sequence);

            BehaviourTree behaviourTree = new BehaviourTree("Root");
            behaviourTree.AddChild(utilFail);

            return behaviourTree;
        }
    }
}
