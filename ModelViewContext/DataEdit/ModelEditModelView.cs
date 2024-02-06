using DatabaseManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModelViewContext
{
	public class ModelEditModelView : ViewModelBase
	{
		private Visibility _windowVisibility = Visibility.Visible;

		public ViewModelCommand EditModelCommand { get; private set; }

		public DataModel DataModel { get; private set; }
		public string Tittle { get; set; }


		public Visibility WindowVisibility
		{
			get { return _windowVisibility; }
			set
			{
				_windowVisibility = value;
				OnPropertyChanged(nameof(WindowVisibility));
			}
		}

		protected DBWorker Database => DBWorker.GetInstance();

		public ModelEditModelView() : base()
		{ 
			Tittle = string.Empty;
			DataModel = new DataModel();
			EditModelCommand = new ViewModelCommand(Add);
		}

		public ModelEditModelView(DataModel dataModel) : base()
		{
			Tittle = string.Empty;
			DataModel = dataModel;
			EditModelCommand = new ViewModelCommand(Edit);
		}

		protected virtual void Add(object obj)
		{ 
		
		}

		protected virtual void Edit(object obj)
		{ 
			
		}
	}
}
