namespace Program
{
    public static class Logger
    {
        public static void Console(string message)
        {
            var template = $"{Environment.ProcessId} {Environment.MachineName} {Environment.UserName} {DateTime.UtcNow} - {message}";
            System.Console.WriteLine(template);
        }

        public static void DrawDottedLine(int width)
        {
            for (int i = 0; i < width; i++)
            {
                if(i == width - 1)
                {
                    System.Console.WriteLine(".");
                }
                else
                {
                    System.Console.Write(".");
                }
            }

            System.Console.WriteLine();
        }
    }
    public class WorkflowRule
    {
        public string Name { get; set; }
    }

    public class Workflow
    {
        public Workflow()
        {
            WorkflowRules = new List<WorkflowRule>();
        }

        public string Name { get; set; }
        public List<WorkflowRule> WorkflowRules { get; set; }
    }

    public class WorkflowProcessor
    {
        public void Process(Workflow workflow)
        {
            ValidationCheck(workflow);
            Thread.Sleep(1000);
            ExecuteWorkflow(workflow);
            Thread.Sleep(1000);
        }

        private void ValidationCheck(Workflow workflow)
        {
            SetConsoleColor(ConsoleColor.Yellow);
            Logger.Console("Checking validity of the workflow...");
            if (workflow is null)
            {
                throw new ArgumentNullException(nameof(workflow));
            }
            ResetColor();
            SetConsoleColor(ConsoleColor.Green);
            Thread.Sleep(1000);
            Logger.Console("Work flow is valid.");
            ResetColor();
        }

        private void ExecuteWorkflow(Workflow workflow)
        {
            SetConsoleColor(ConsoleColor.Yellow);
            Logger.Console("Executing workflow...");
            ResetColor();
            if (!workflow.WorkflowRules.Any())
            {
                SetConsoleColor(ConsoleColor.Green);
                Logger.Console("Workflow doesn't have any rules.");
                ResetColor();
            }
            else
            {
                SetConsoleColor(ConsoleColor.Green);
                foreach (var rule in workflow.WorkflowRules)
                {
                    Logger.Console($"Executing rule: {rule.Name}");
                    Thread.Sleep(2000);
                }
                ResetColor();
            }
        }

        private void SetConsoleColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        private void ResetColor()
        {
            Console.ResetColor();
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            var processor = new WorkflowProcessor();

            var workflows = new List<Workflow>()
            {
                new Workflow
                {
                    Name = "Worflow 1",
                    WorkflowRules = new List<WorkflowRule>
                    {
                        new WorkflowRule
                        {
                            Name = "Rule 1"
                        },
                        new WorkflowRule
                        {
                            Name = "Rule 2"
                        }
                    }
                },
                new Workflow
                {
                    Name = "Workflow 2",
                    WorkflowRules = new List<WorkflowRule>
                    {
                        new WorkflowRule
                        {
                            Name = "Rule 1"
                        },
                        new WorkflowRule
                        {
                            Name = "Rule 2"
                        }
                    }
                }
            };

            foreach (var workflow in workflows)
            {
                processor.Process(workflow);
                Logger.Console("Workflow execution completed.");
                Thread.Sleep(1000);
                Logger.DrawDottedLine(Console.WindowWidth);
            }
        }
    }
}