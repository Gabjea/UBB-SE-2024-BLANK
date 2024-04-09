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

			EncryptionModule em = new EncryptionModule();
			Media encrypted = em.encryptFile(new PhotoMedia(path));

			Media decrypted = em.decryptFile(encrypted);
			MessageBox.Show(decrypted.FilePath);
		}
	}

}
