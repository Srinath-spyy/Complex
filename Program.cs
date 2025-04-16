using System;
using System.Collections.Generic;

namespace ComplexExample
{
    /// <summary>
    /// Interface for processing data.
    /// </summary>
    public interface IProcessor
    {
        /// <summary>
        /// Processes a list of integers.
        /// </summary>
        /// <param name="data">The list of integers to process.</param>
        void Process(List<int> data);
    }

    /// <summary>
    /// Concrete implementation of the IProcessor interface.
    /// </summary>
    public class DataProcessor : IProcessor
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the DataProcessor class.
        /// </summary>
        /// <param name="logger">The logger to use for logging information and errors.</param>
        public DataProcessor(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Processes a list of integers.
        /// </summary>
        /// <param name="data">The list of integers to process.</param>
        public void Process(List<int> data)
        {
            if (data == null)
            {
                _logger.LogError("Data cannot be null.");
                return;
            }

            try
            {
                int sum = 0;
                foreach (var item in data)
                {
                    sum += item;
                }

                _logger.LogInformation($"Processed sum: {sum}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing data: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Interface for logging information and errors.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void LogInformation(string message);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void LogError(string message);
    }

    /// <summary>
    /// Concrete implementation of the ILogger interface that logs to the console.
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        /// Logs an informational message to the console.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void LogInformation(string message)
        {
            Console.WriteLine($"INFO: {message}");
        }

        /// <summary>
        /// Logs an error message to the console.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void LogError(string message)
        {
            Console.WriteLine($"ERROR: {message}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var logger = new ConsoleLogger();
            var processor = new DataProcessor(logger);

            List<int> data = new List<int> { 1, 2, 3, 4, 5 };
            processor.Process(data);
        }
    }
}