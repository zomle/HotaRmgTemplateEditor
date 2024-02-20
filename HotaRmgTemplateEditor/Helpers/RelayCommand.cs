using System.Diagnostics;
using System.Windows.Input;

namespace HotaRmgTemplateEditor.Helpers
{
	public class RelayCommand : ICommand
	{
		private Action<object?> ExecuteProp { get; }
		private Predicate<object?> CanExecuteProp { get; }

		public RelayCommand(Action<object?> execute)
			: this(execute, _ => true)
		{
		}

		public RelayCommand(Action<object?> execute, Predicate<object?> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}
			ExecuteProp = execute;
			CanExecuteProp = canExecute;
		}

		[DebuggerStepThrough]
		public bool CanExecute(object? parameter)
		{
			return CanExecuteProp == null || CanExecuteProp(parameter);
		}

		public event EventHandler? CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object? parameter)
		{
			ExecuteProp(parameter);
		}
	}
}
