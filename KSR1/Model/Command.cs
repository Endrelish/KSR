namespace KSR1.Model
{
    using System;
    using System.Windows.Input;
    public class Command : ICommand
    {
        private Action action;

        private bool canExecute;

        public Command(Action action, bool canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return this.canExecute;
        }

        public void Execute(object parameter)
        {
            this.action();
        }

        public event EventHandler CanExecuteChanged;
    }
}