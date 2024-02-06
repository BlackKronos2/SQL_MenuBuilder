using System;
using System.Reflection;
using System.Windows.Controls;
using ModelViewContext;

namespace SQLMenuBuilder
{
	public class PagesLoader
	{
		public PagesLoader()
		{

		}

		public static void LoadPage(string dllName, string pageName)
		{
			Assembly assembly = Assembly.LoadFrom(dllName + ".dll");

			string fullTypeName = dllName + "." + pageName;
			Type type = assembly.GetType(fullTypeName);

			if (type != null && typeof(Page).IsAssignableFrom(type))
			{
				Page page = (Page)Activator.CreateInstance(type);
				WindowsEvents.LoadPage(page);
			}
			else
			{
				throw new Exception($"{pageName} - не может быть загружен как Page");
			}
		}

	}
}
