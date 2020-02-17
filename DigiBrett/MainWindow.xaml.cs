using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BrettspielCore;

namespace DigiBrett
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Felder
		private DispatcherTimer spielZeit = new DispatcherTimer();
		private double verstricheneZeit = 0;
		private const int intervall = 16;
		private Spiel spiel;
		#endregion Felder

		#region Konstruktoren
		public MainWindow()
		{
			BitmapImage appIcon = new BitmapImage();
			appIcon.BeginInit();
			appIcon.UriSource = new Uri("pack://application:,,,/Content/graphics/spielfeld3232.png");
			appIcon.EndInit();
			Icon = appIcon;
			InitializeComponent();
			spiel = new Spiel(this.grid);
			spiel.Status += AktualisiereStatus;
			spiel.Verloren += Spiel_Verloren;

			SetImageControls();
		}

		private void Spiel_Verloren(object sender, EventArgs e)
		{
			spielZeit.Stop();
		}
		#endregion Konstruktoren

		#region Methoden
		private void SetImageControls()
		{
			SpielfeldHandler.ImageControls.img00 = this.img00;
			SpielfeldHandler.ImageControls.img03 = this.img03;
			SpielfeldHandler.ImageControls.img06 = this.img06;
			SpielfeldHandler.ImageControls.img11 = this.img11;
			SpielfeldHandler.ImageControls.img13 = this.img13;
			SpielfeldHandler.ImageControls.img15 = this.img15;
			SpielfeldHandler.ImageControls.img22 = this.img22;
			SpielfeldHandler.ImageControls.img23 = this.img23;
			SpielfeldHandler.ImageControls.img24 = this.img24;
			SpielfeldHandler.ImageControls.img30 = this.img30;
			SpielfeldHandler.ImageControls.img31 = this.img31;
			SpielfeldHandler.ImageControls.img32 = this.img32;
			SpielfeldHandler.ImageControls.img34 = this.img34;
			SpielfeldHandler.ImageControls.img35 = this.img35;
			SpielfeldHandler.ImageControls.img36 = this.img36;
			SpielfeldHandler.ImageControls.img42 = this.img42;
			SpielfeldHandler.ImageControls.img43 = this.img43;
			SpielfeldHandler.ImageControls.img44 = this.img44;
			SpielfeldHandler.ImageControls.img51 = this.img51;
			SpielfeldHandler.ImageControls.img53 = this.img53;
			SpielfeldHandler.ImageControls.img55 = this.img55;
			SpielfeldHandler.ImageControls.img60 = this.img60;
			SpielfeldHandler.ImageControls.img63 = this.img63;
			SpielfeldHandler.ImageControls.img66 = this.img66;
		}
		private void AktualisiereLabels()
		{
			lblTime.Content = Dienste.TimeToString(verstricheneZeit);
			//this.lblTime.Content = verstricheneZeit.ToString("00:00:00,000");
			lblPlayer.Content = "Spieler " + spiel.Spieler.ToString();
			lblSteine.Content = "Steine: " + Dienste.Zaehle(spiel.SpielSteineGesetzt, spiel.Spieler);
			lblPhase.Content = "Phase " + spiel.SpielPhase.ToString();

		}

		private void ZeigeTutorial()
		{
			if (spielZeit.IsEnabled)
			{
				spielZeit.Stop();
			}

			Tutorial tut = new Tutorial(this);
			tut.Closed += tut_Closed;
			tut.ShowDialog();
		}

		private void AktualisiereStatus(object sender, StatusEventArgs e)
		{
			lblStatus.Content = e.Text;
			lblStatusZusatz.Content = e.ZusaetzlicherText;
		}

		#endregion Methoden

		#region Eventhandler
		private void window_Initialized(object sender, EventArgs e)
		{
			spielZeit.Interval = new TimeSpan(166666);
			spielZeit.Tick += Update;
		}

		private void window_Loaded(object sender, RoutedEventArgs e)
		{
			ZeigeTutorial();
			AktualisiereLabels();
		}

		private void tut_Closed(object sender, EventArgs e)
		{
			if (!spielZeit.IsEnabled && !spiel.SpielVorbei)
			{
				System.Threading.Thread.Sleep(500);
				spielZeit.Start();
			}
		}

		private void Update(object sender, EventArgs e)
		{
			verstricheneZeit += spielZeit.Interval.TotalMilliseconds;
			AktualisiereLabels();

			if (spielZeit.IsEnabled)
				spiel.Update();
		}

		private void window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.H)
			{
				ZeigeTutorial();
			}
		}
		#endregion Eventhandler
	}
}