using RockyHockey.Common;
using RockyHockey.MoveCalculationFramework;
using System;
using System.Drawing;

namespace RockyHockey
{
    /// <summary>
    /// Handles the user input
    /// </summary>
    public class CommandHandler
    {
        /// <summary>
        /// Creates a new Instance of the CommandHandler
        /// </summary>
        /// <param name="logger">logger where the execution and error info will be logged</param>
        public CommandHandler(ILogger logger)
        {
            this.logger = logger;
            ConfigInitializer.InitializeConfig(objectSerializer);
        }

        private ILogger logger;

        /// <summary>
        /// Instanz des MoveCalculationProvider, um die KI zu starten
        /// </summary>
        private IMoveCalculationProvider moveCalculationProvider = new MoveCalculationProvider();

        private ObjectSerializer objectSerializer = new ObjectSerializer("RockyHockeyConfig.xml");

        /// <summary>
        /// TextFileLogger
        /// </summary>
        public static ILogger LoggerTextFile { get; } = new TextFileLogger("Log.txt");

        /// <summary>
        /// executes the given command
        /// </summary>
        /// <param name="command"></param>
        public void HandleInput(string command)
        {
            try
            {
                ConsoleCommands consoleCommand = (ConsoleCommands)Enum.Parse(typeof(ConsoleCommands), command);
                switch (consoleCommand)
                {
                    case ConsoleCommands.Start:
                        StartCalculationFramework();
                        break;
                    case ConsoleCommands.Stop:
                        StopCalculationFramework();
                        break;
                    case ConsoleCommands.Esc:
                        Environment.Exit(0);
                        break;
                    case ConsoleCommands.SetGameFieldSize:
                        SetGameFieldSize();
                        break;
                    case ConsoleCommands.SetFrameRate:
                        SetFrameRate();
                        break;
                    case ConsoleCommands.SetDifficulty:
                        SetDifficulty();
                        break;
                    case ConsoleCommands.SetPunchAxis:
                        SetPunchAxis();
                        break;
                    case ConsoleCommands.SetTolerance:
                        SetTolerance();
                        break;
                    case ConsoleCommands.SetCameraIndexOne:
                        Config.Instance.Camera1Index = SetIntValue("camera index one", Config.Instance.Camera1Index);
                        objectSerializer.SerializeObject(Config.Instance);
                        break;
                    case ConsoleCommands.SetCameraIndexTwo:
                        Config.Instance.Camera2Index = SetIntValue("camera index two", Config.Instance.Camera2Index);
                        objectSerializer.SerializeObject(Config.Instance);
                        break;
                    case ConsoleCommands.SetMaxBatVelocity:
                        SetMaxBatVelocity();
                        break;
                    case ConsoleCommands.SetRestPositionDivisor:
                        SetRestPositionDivisor();
                        break;
                    default:
                        logger?.Log("Command not implemented").Wait();
                        break;
                }
            }
            catch
            {
                logger?.Log("Unkown command").Wait();
            }
        }

        /// <summary>
        /// Startet die KI asynchron
        /// </summary>
        private async void StartCalculationFramework()
        {
            try
            {
                moveCalculationProvider = new MoveCalculationProvider();
                logger?.Log("RockyHockey started successfully!");
                await moveCalculationProvider.StartCalculation().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger?.Log(ex);
                //LoggerTextFile?.Log(ex);
            }
        }

        /// <summary>
        /// Stoppt die KI asynchron
        /// </summary>
        private async void StopCalculationFramework()
        {
            try
            {
                await moveCalculationProvider.StopCalculation().ConfigureAwait(false);
                logger?.Log("RockyHockey stopped successfully!");
                moveCalculationProvider = null;
            }
            catch (Exception ex)
            {
                logger?.Log(ex);
                //LoggerTextFile?.Log(ex);
            }
        }

        private int SetIntValue(string valueName, int currentValue)
        {
            int newValue = 0;
            while (newValue == 0)
            {
                logger?.Log($"Current {valueName}: {currentValue}").Wait();
                logger?.Log($"Please enter the new {valueName}:").Wait();
                string input = Console.ReadLine();
                try
                {
                    newValue = Convert.ToInt32(input);
                }
                catch
                {
                    logger?.Log("Please enter an integer value:").Wait();
                }
            }
            logger?.Log($"{valueName} changed to {newValue}").Wait();
            return newValue;
        }

        private void SetDifficulty()
        {
            logger?.Log($"Current difficulty: {Config.Instance.GameDifficulty}").Wait();
            logger?.Log("Please enter the new difficulty:").Wait();
            logger?.Log("Possible difficulties:").Wait();
            foreach (string difficulty in Enum.GetNames(typeof(Difficulties)))
            {
                logger?.Log($"    * {difficulty}").Wait();
            }

            Difficulties consoleCommand = Difficulties.hard;
            bool validInput = false;
            while (!validInput)
            {
                try
                {
                    string input = Console.ReadLine();
                    consoleCommand = (Difficulties)Enum.Parse(typeof(Difficulties), input);
                    validInput = true;
                }
                catch
                {
                    logger?.Log("Please enter a valid difficulty:").Wait();
                }
            }
            Config.Instance.GameDifficulty = consoleCommand;
            logger?.Log($"difficulty changed to {consoleCommand}").Wait();
            objectSerializer.SerializeObject(Config.Instance);
        }

        private void SetFrameRate()
        {
            Config.Instance.FrameRate = SetIntValue("framerate", Config.Instance.FrameRate);
            objectSerializer.SerializeObject(Config.Instance);
        }

        private void SetPunchAxis()
        {
            Config.Instance.ImaginaryAxePosition = SetIntValue("punch axis position", Config.Instance.ImaginaryAxePosition);
            objectSerializer.SerializeObject(Config.Instance);
        }

        private void SetTolerance()
        {
            Config.Instance.Tolerance = SetIntValue("tolerance", Config.Instance.Tolerance);
            objectSerializer.SerializeObject(Config.Instance);
        }

        private void SetGameFieldSize()
        {
            int width = SetIntValue("width", Config.Instance.GameFieldSize.Width);
            int height = SetIntValue("height", Config.Instance.GameFieldSize.Height);
            Config.Instance.GameFieldSize = new Size(width, height);
            objectSerializer.SerializeObject(Config.Instance);
        }

        private void SetMaxBatVelocity()
        {
            //TODO: bestimmen ob das Setzen eines Integer-Wertes für die Geschwindigkeit reicht
            Config.Instance.MaxBatVelocity = SetIntValue("max bat velocity", Convert.ToInt32(Config.Instance.MaxBatVelocity));
            objectSerializer.SerializeObject(Config.Instance);
        }

        private void SetRestPositionDivisor()
        {
            Config.Instance.RestPositionDivisor = SetIntValue("rest position divisor", Convert.ToInt32(Config.Instance.RestPositionDivisor));
            objectSerializer.SerializeObject(Config.Instance);
        }
    }
}
