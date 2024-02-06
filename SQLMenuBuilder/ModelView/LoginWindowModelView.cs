using DatabaseManagement;
using ModelViewContext;
using System;
using System.Security.Principal;
using System.Threading;
using System.Windows;

namespace SQLMenuBuilder
{
	public class LoginWindowModelView : ViewModelBase
	{
		private string _login;
		private string _password;

		private Visibility _windowVisibility = Visibility.Visible;

		public string Login
		{
			get { return _login; }
			set
			{
				_login = value;
				OnPropertyChanged(nameof(Login));
			}
		}
		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
				OnPropertyChanged(nameof(Password));
			}
		}

		public Visibility WindowVisibility
		{
			get { return _windowVisibility; }
			set
			{
				_windowVisibility = value;
				OnPropertyChanged(nameof(WindowVisibility));
			}
		}

		public ViewModelCommand LogCommand { get; }
		public ViewModelCommand RegCommand { get; }

		public LoginWindowModelView()
		{
			LogCommand = new ViewModelCommand(UserLogin);
			RegCommand = new ViewModelCommand(UserReg);
		}

		private void UserLogin(object obj)
		{
			try
			{
				if (!DBWorker.GetInstance().Authorization(Login, Password))
					return;

				UserModel user = new UserModel()
				{
					Login = Login,
					Password = Password
				};

				Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(user.Login), null);
				WindowsEvents.UserLogin();

				WindowVisibility = Visibility.Hidden;
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}

		private void UserReg(object obj)
		{
			UserModel user = new UserModel()
			{
				Login = Login,
				Password = Password
			};

			try
			{
				DBWorker.GetInstance().Add(user);
				SuccessMessage("Пользователь успешно зарегистрирован");

			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}
	}
}
