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
            BlackboardTask blackboardTask,
            List<Task> tasks)
        {
            Node sequence = new Sequence("Sequence Main");

            foreach (Task task in tasks)
            {
                Node gotoTask = new Leaf("Go to " + task.Name, new GotoDestinationStrategy(agent, task.Waypoint));
                Node doTask = new Leaf("Do Task " + task.Name, new DoTaskStrategy(task, () =>
                {
                    blackboardTask.Health += task.TaskSo.Health;
                    blackboardTask.Stamina += task.TaskSo.Stamina;
                    blackboardTask.Profit += task.TaskSo.Profit;
                    blackboardTask.ElapsedTime += task.TimeToComplete;
                }));
                Node sequenceTask = new Sequence("Sequence " + task.Name);
                sequenceTask.AddChild(gotoTask);
                sequenceTask.AddChild(doTask);

                sequence.AddChild(sequenceTask);
            }

            Node untilCondition = new UntilCondition("Until 60 seconds", () => blackboardTask.ElapsedTime >= 60);
            untilCondition.AddChild(sequence);

            BehaviourTree behaviourTree = new BehaviourTree("Root");
            behaviourTree.AddChild(untilCondition);

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
            Node doHeal = new Leaf("Do Heal", new DoTaskStrategy(healTask, () =>
            {
                blackboardTask.Health += healTask.TaskSo.Health;
                blackboardTask.Stamina += healTask.TaskSo.Stamina;
                blackboardTask.Profit += healTask.TaskSo.Profit;
                blackboardTask.ElapsedTime += healTask.TimeToComplete;
            }));

            Node healSequence = new Sequence("Sequence Heal");
            healSequence.AddChild(healCondition);
            healSequence.AddChild(gotoHeal);
            healSequence.AddChild(doHeal);

            // Rest
            Node restCondition = new Leaf("Check Stamina", new ConditionStrategy(() => blackboardTask.Stamina <= 15));
            Node gotoRest = new Leaf("Go to Rest", new GotoDestinationStrategy(agent, restTask.Waypoint));
            Node doRest = new Leaf("Do Rest", new DoTaskStrategy(restTask, () =>
            {
                blackboardTask.Health += restTask.TaskSo.Health;
                blackboardTask.Stamina += restTask.TaskSo.Stamina;
                blackboardTask.Profit += restTask.TaskSo.Profit;
                blackboardTask.ElapsedTime += restTask.TimeToComplete;
            }));

            Node restSequence = new Sequence("Sequence Rest");
            restSequence.AddChild(restCondition);
            restSequence.AddChild(gotoRest);
            restSequence.AddChild(doRest);

            // Fight
            Node fightCondition = new Leaf("Can Fight",
                new ConditionStrategy(() => blackboardTask.Health >= 40 && blackboardTask.Stamina > 25));
            Node gotoFight = new Leaf("Go to Fight", new GotoDestinationStrategy(agent, fightTask.Waypoint));
            Node doFight = new Leaf("Do Fight", new DoTaskStrategy(fightTask, () =>
            {
                blackboardTask.Health += fightTask.TaskSo.Health;
                blackboardTask.Stamina += fightTask.TaskSo.Stamina;
                blackboardTask.Profit += fightTask.TaskSo.Profit;
                blackboardTask.ElapsedTime += fightTask.TimeToComplete;
            }));

            Node fightSequence = new Sequence("Sequence Fight");
            fightSequence.AddChild(fightCondition);
            fightSequence.AddChild(gotoFight);
            fightSequence.AddChild(doFight);

            // Manufactor
            Node manufactorCondition = new Leaf("Can Manufactor",
                new ConditionStrategy(() => blackboardTask.Stamina > 5 && (60 - blackboardTask.ElapsedTime) >= 5));
            Node gotoManufactor = new Leaf("Go to Manufactor",
                new GotoDestinationStrategy(agent, manufactorTask.Waypoint));
            Node doManufactor = new Leaf("Do Manufactor", new DoTaskStrategy(manufactorTask, () =>
            {
                blackboardTask.Health += manufactorTask.TaskSo.Health;
                blackboardTask.Stamina += manufactorTask.TaskSo.Stamina;
                blackboardTask.Profit += manufactorTask.TaskSo.Profit;
                blackboardTask.ElapsedTime += manufactorTask.TimeToComplete;
            }));

            Node manufactorSequence = new Sequence("Sequence Manufactor");
            manufactorSequence.AddChild(manufactorCondition);
            manufactorSequence.AddChild(gotoManufactor);
            manufactorSequence.AddChild(doManufactor);

            // Plant
            Node plantCondition = new Leaf("Can Plant",
                new ConditionStrategy(() => blackboardTask.Stamina > 10 && (60 - blackboardTask.ElapsedTime) >= 4));
            Node gotoPlant = new Leaf("Go to Plant", new GotoDestinationStrategy(agent, plantTask.Waypoint));
            Node doPlant = new Leaf("Do Plant", new DoTaskStrategy(plantTask, () =>
            {
                blackboardTask.Health += plantTask.TaskSo.Health;
                blackboardTask.Stamina += plantTask.TaskSo.Stamina;
                blackboardTask.Profit += plantTask.TaskSo.Profit;
                blackboardTask.ElapsedTime += plantTask.TimeToComplete;
            }));

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

            Node untilCondition = new UntilCondition("Until 60 seconds", () => blackboardTask.ElapsedTime >= 60);
            untilCondition.AddChild(mainSelector);

            BehaviourTree behaviourTree = new BehaviourTree("Root");
            behaviourTree.AddChild(untilCondition);

            return behaviourTree;
        }

        public static BehaviourTree GetOptimalTreeTasks(
            NavMeshAgent agent,
            BlackboardTask blackboardTask,
            Task fightTask,
            Task manufactorTask,
            Task plantTask,
            Task healTask,
            Task restTask
        )
        {
            // Número de Lutar (x1): 0.0
            // Número de Plantar (x2): 1.0
            // Número de Fabricar (x3): 10.0
            // Número de Curar (x4): 0.0
            // Número de Descansar (x5): 6.0
            // Lucro total: 180.0
            
            // Rest
            Node restCondition = new Leaf("Check Stamina", new ConditionStrategy(() => blackboardTask.Stamina <= 10));
            Node gotoRest = new Leaf("Go to Rest", new GotoDestinationStrategy(agent, restTask.Waypoint));
            Node doRest = new Leaf("Do Rest", new DoTaskStrategy(restTask, () =>
            {
                blackboardTask.Health += restTask.TaskSo.Health;
                blackboardTask.Stamina += restTask.TaskSo.Stamina;
                blackboardTask.Profit += restTask.TaskSo.Profit;
                blackboardTask.ElapsedTime += restTask.TimeToComplete;
            }));
            
            Node restSequence = new Sequence("Sequence Rest");
            restSequence.AddChild(restCondition);
            restSequence.AddChild(gotoRest);
            restSequence.AddChild(doRest);
            
            // Plant
            Node gotoPlant = new Leaf("Go to Plant", new GotoDestinationStrategy(agent, plantTask.Waypoint));
            Node doPlant = new Leaf("Do Plant", new DoTaskStrategy(plantTask, () =>
            {
                blackboardTask.Health += plantTask.TaskSo.Health;
                blackboardTask.Stamina += plantTask.TaskSo.Stamina;
                blackboardTask.Profit += plantTask.TaskSo.Profit;
                blackboardTask.ElapsedTime += plantTask.TimeToComplete;
            }));

            Node plantSequence = new Sequence("Sequence Plant");
            plantSequence.AddChild(gotoPlant);
            plantSequence.AddChild(doPlant);
            
            Node plantRepeat = new Repeat("Plant Repeat", 1);
            plantRepeat.AddChild(plantSequence);
            
            // Manufactor
            Node gotoManufactor = new Leaf("Go to Manufactor",
                new GotoDestinationStrategy(agent, manufactorTask.Waypoint));
            Node doManufactor = new Leaf("Do Manufactor", new DoTaskStrategy(manufactorTask, () =>
            {
                blackboardTask.Health += manufactorTask.TaskSo.Health;
                blackboardTask.Stamina += manufactorTask.TaskSo.Stamina;
                blackboardTask.Profit += manufactorTask.TaskSo.Profit;
                blackboardTask.ElapsedTime += manufactorTask.TimeToComplete;
            }));

            Node manufactorSequence = new Sequence("Sequence Manufactor");
            manufactorSequence.AddChild(gotoManufactor);
            manufactorSequence.AddChild(doManufactor);
            
            Node manufactorRepeat = new Repeat("Manufactor Repeat", 10);
            manufactorRepeat.AddChild(manufactorSequence);
            
            // Profitable Tasks
            Node profitableTask = new Selector("Selector Profitable Task");
            profitableTask.AddChild(plantRepeat);
            profitableTask.AddChild(manufactorRepeat);
            
            // Main Selector
            Node mainSelector = new Selector("Main Root");
            mainSelector.AddChild(restSequence);
            mainSelector.AddChild(profitableTask);
            
            Node untilCondition = new UntilCondition("Until 60 seconds", () => blackboardTask.ElapsedTime >= 60);
            untilCondition.AddChild(mainSelector);
            
            BehaviourTree behaviourTree = new BehaviourTree("Root");
            behaviourTree.AddChild(untilCondition);
            
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
