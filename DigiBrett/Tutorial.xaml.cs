using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DigiBrett
{
	/// <summary>
	/// Interaktionslogik für Tutorial.xaml
	/// </summary>
	public partial class Tutorial : Window
	{
		public Tutorial(object owner)
		{
			InitializeComponent();

			MainWindow main = null;
			if (owner is MainWindow)
				main = (MainWindow)owner;

			if (main != null)
			{
				Left = main.Left;
				Top = main.Top;
				Height = main.Height;
				Width = main.Width;
			}

			tutText.Content = "Willkommen zum Mühle-Spiel!" + Environment.NewLine
									+ "Die Regeln lauten: " + Environment.NewLine
									+ "1. Jeder Spieler setzt abwechselnd einen Stein auf das Spielfeld." + Environment.NewLine
									+ "2. Dabei können die Steine nur an Eck- und Kreuzpunkten gesetzt werden." + Environment.NewLine
									+ "3. Sobald jeder Spieler insgesamt 9 Steine platziert hat, beginnt die zweite Phase des Spiels." + Environment.NewLine
									+ "   In dieser Phase kann jeder Spieler abwechselnd einen seiner Steine auf einen benachbarten Punkt verschieben." + Environment.NewLine
									+ "4. Entsteht während dieser Phasen eine Mühle (3 gleichfarbige Steine in einer Reihe (horizontal oder vertikal)) "+Environment.NewLine
									+ "   darf ein Stein des Gegenspielers vom Spielfeld entfernt werden." + Environment.NewLine
									+ "4.1 Steine, die Teil einer Mühle sind können nicht entfernt werden." +Environment.NewLine
									+ "5. Hat einer der Spieler nur noch 3 Steine auf dem Spielfeld beginnt die dritte Phase des Spiels." + Environment.NewLine
									+ "   In dieser Phase kann derjenige, der nur noch 3 Steine besitzt, pro Zug einen dieser Steine frei "+ Environment.NewLine
									+ "   auf dem Spielfeld platzieren. Also nicht nur auf benachbarte Punkte." + Environment.NewLine
									+ "6. Das Spiel endet, wenn einer der Spieler weniger als 3 Steine besitzt oder keinen Stein verschieben "+Environment.NewLine
									+ "   kann, da alle benachbarten Punkte besetzt sind." + Environment.NewLine
									+ "   In beiden Fällen gewinnt der jeweils andere Spieler das Spiel." + Environment.NewLine + Environment.NewLine
									+ "Klicken Sie auf das Fenster, um zum Spiel zu gelangen.";
		}

		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Close();
		}
	}
}
