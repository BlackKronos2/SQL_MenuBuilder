using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ModelViewContext
{
	public class TableEditorPageManager
	{
		private DataGrid _table;
		private Button _addButton;
		private Button _editButton;
		private Button _deleteButton;

		public TableEditorPageManager(Button addButton, Button editButton, Button deleteButton, DataGrid table) 
		{
			_table = table;

			_addButton = addButton;
			_editButton = editButton;
			_deleteButton = deleteButton;
		}

		private void SetSettings()
		{
			_table.AutoGenerateColumns = false;
			_table.SelectionMode = DataGridSelectionMode.Single;
			_table.IsReadOnly = true;
			_table.CanUserResizeColumns = false;
			_table.CanUserDeleteRows = false;
			_table.CanUserAddRows = false;
			_table.CanUserReorderColumns = false;
		}

		private void SetBinding(TableEditorViewModel viewModel)
		{
			if (viewModel == null)
				return;

			Binding itemsBinding = new Binding("Items");
			itemsBinding.Source = viewModel;

			Binding selectedItemBinding = new Binding("SelectedItem");
			selectedItemBinding.Mode = BindingMode.TwoWay;
			selectedItemBinding.Source = viewModel;

			_table.AutoGenerateColumns = false;
			_table.SelectionMode = DataGridSelectionMode.Single;
			_table.IsReadOnly = true;

			_table.SetBinding(DataGrid.SelectedItemProperty, selectedItemBinding);
			_table.SetBinding(DataGrid.ItemsSourceProperty, itemsBinding);
		}

		public void LoadModelView(TableEditorViewModel viewModel)
		{
			if (viewModel == null)
				return;

			_addButton.Command = viewModel.AddCommand;
			_editButton.Command = viewModel.EditCommand;
			_deleteButton.Command = viewModel.DeleteCommand;

			SetBinding(viewModel);
			SetSettings();
		}

	}
}
