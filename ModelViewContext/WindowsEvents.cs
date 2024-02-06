using System;
using System.Windows;
using System.Windows.Controls;

namespace ModelViewContext
{
	public class WindowsEvents
	{
		public static Action<Page> OnLoadPage { get; set; }
		public static Action OnLogin { get; set; }
		public static Action OnLogOut { get; set; }
		public static Action<Type, ViewModelBase> OnOpenWindow { get; set; }

		public static void LoadPage(Page page) => OnLoadPage?.Invoke(page);
		public static void UserLogin() => OnLogin?.Invoke();
		public static void UserLogOut() => OnLogOut?.Invoke();
		public static void OpenWindow(Type windowType, ViewModelBase viewModel) => OnOpenWindow?.Invoke(windowType, viewModel);
	}
}
