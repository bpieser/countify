using System.Windows.Input;

namespace Countify.UI;

public class DelegateCommand(Action<object> executeDelegate, Predicate<object> canExecuteDelegate) : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public DelegateCommand(Action action, Predicate<object> canExecuteDelegate) 
        :this(_ => action(), canExecuteDelegate) { }

    public bool CanExecute(object? parameter) => canExecuteDelegate(parameter!);

    public void Execute(object? parameter) => executeDelegate(parameter!);
    
    public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}

