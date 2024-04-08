using client.models;
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

			String path = "C:\\Users\\flori\\Desktop\\dog.png";

			CompressionModule cm = new CompressionModule();
		}
	}

}
