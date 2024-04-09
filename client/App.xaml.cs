﻿using client.models;
using client.repositories;
using client.services;
using client.modules;
using System.Configuration;
using System.Data;
using System.Windows;

namespace client
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application

	{
		App()
		{
			MainWindow m = new MainWindow();
			m.Show();
		}
	}

}
