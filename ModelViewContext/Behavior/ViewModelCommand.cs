using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModelViewContext
{
	public class ViewModelCommand : ICommand
	{
		private readonly Action<object> _executeAction;

		public ViewModelCommand(Action<object> executeAction)
		{
			_executeAction = executeAction;
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter) => _executeAction != null;
		public void Execute(object parameter) => _executeAction(parameter);
	}
}
