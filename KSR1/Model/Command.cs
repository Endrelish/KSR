namespace KSR1.Model
{
    using System;
    using System.Windows.Input;
    public class Command : ICommand
    {
        private Action action;

        private Func<bool> canExecute;

        public Command(Action action, Func<bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return this.canExecute();
        }

        public void Execute(object parameter)
        {
            this.action();
        }

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged.Invoke(this, null);
        }

        public event EventHandler CanExecuteChanged;
    }
}