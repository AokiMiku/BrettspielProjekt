using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BrettspielCore
{
	/// <summary>
	/// Verwaltet die Spielsteine.
	/// </summary>
    internal class Spielsteine
	{
		#region Felder
		internal BitmapImage Schwarz = new BitmapImage();
		internal BitmapImage Weiss = new BitmapImage();

		#endregion Felder

		#region Konstruktoren
		public Spielsteine()
		{
			Weiss.BeginInit();
			Weiss.UriSource = new Uri("pack://application:,,,/Content/graphics/spielsteinWeiss.png");
			Weiss.EndInit();

			Schwarz.BeginInit();
			Schwarz.UriSource = new Uri("pack://application:,,,/Content/graphics/spielsteinSchwarz.png");
			Schwarz.EndInit();
		}
		#endregion Konstruktoren

		#region Methoden

		#endregion Methoden
	}
}