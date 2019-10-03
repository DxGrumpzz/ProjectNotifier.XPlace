﻿namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// A list that holds information about currently running commands
        /// </summary>
        private HashSet<int> _runningCommands = new HashSet<int>();

        private object _synchronizingObject = new object();

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Updates the UI when a property's value changes
        /// </summary>
        /// <param name="propertyName"> The name of the property </param>
        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        /// Runs a single command until execution is finalized
        /// </summary>
        /// <param name="function"> The function to execute </param>
        /// <returns></returns>
        protected async Task RunCommandAsync(Func<Task> function)
        {
            lock (_synchronizingObject)
            {
                // Check if current command is already running
                if (IsCommandRunning(function.Method.MetadataToken) == true)
                    return;

                // Add the new command to the list
                _runningCommands.Add(function.Method.MetadataToken);
            };

            try
            {
                // Invoke and wait for command to finish execution
                await function?.Invoke();
            }
            finally
            {
                // No matter what happens during execution (execptions and such) make sure that command is removed
                _runningCommands.Remove(function.Method.MetadataToken);
            };
        }


        /// <summary>
        /// Check if a command is currently running
        /// </summary>
        /// <param name="functionToken"> The function to verify </param>
        /// <returns></returns>
        private bool IsCommandRunning(int functionToken)
        {
            // Check if command exists in the list
            if (_runningCommands.Contains(functionToken) == false)
                return false;
            else
                return true;
        }
    }
}
