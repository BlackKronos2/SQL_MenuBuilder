using System.Collections.ObjectModel;
using System.Windows.Input;
using DatabaseManagement;
using System;

namespace ModelViewContext
{
	public class TableEditorViewModel : ViewModelBase
	{
		public ViewModelCommand AddCommand { get; protected set; }
		public ViewModelCommand EditCommand { get; protected set; }
		public ViewModelCommand DeleteCommand { get; protected set; }


		private ObservableCollection<DataModel> _items;
		private DataModel _selectedItem;

		public ObservableCollection<DataModel> Items
		{
			get { return _items; }
			set
			{
				_items = value;
				OnPropertyChanged(nameof(Items));
			}
		}
		public DataModel SelectedItem
		{
			get { return _selectedItem; }
			set
			{
				_selectedItem = value;
				OnPropertyChanged(nameof(SelectedItem));
			}
		}


		protected DBWorker Database => DBWorker.GetInstance();

		public TableEditorViewModel()
		{
			SelectedItem = null;
			Items = new ObservableCollection<DataModel>();

			AddCommand = new ViewModelCommand(Add);
			EditCommand = new ViewModelCommand(Edit);
			DeleteCommand = new ViewModelCommand(Delete);
		}

		protected virtual void Add(object obj)
		{ }
		protected virtual void Edit(object obj) 
		{
			if (SelectedItem == null)
				throw new Exception("Строка не выбрана");
		}
		protected virtual void Delete(object obj) 
		{
			if (SelectedItem == null)
				throw new Exception("Строка не выбрана");
		}
    }
}
