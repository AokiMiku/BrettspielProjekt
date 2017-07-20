using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace BrettspielCore
{
	/// <summary>
	/// Verwaltet den gesamten Spielablauf.
	/// </summary>
	public class Spiel
	{
		#region Felder
		SpielfeldHandler spielfeldHandler;
		Spielsteine spielsteine;
		private int spieler = 1;
		private int weissGesetzt = 0;
		private int schwarzGesetzt = 0;
		private int spielPhase = 1;
		private bool zugBeendet = false;
		private bool steinInZugGesetzt = false;
		private bool steinZiehen = false;
		private bool spielVorbei = false;

		/// <summary>
		/// Tritt bei geändertem Spielstatus auf.
		/// </summary>
		public event EventHandler<StatusEventArgs> Status;

		/// <summary>
		/// Tritt auf, wenn ein Spieler verloren hat.
		/// </summary>
		public event EventHandler<EventArgs> Verloren;

		/// <summary>
		/// Gibt den Spieler, der derzeit am Zug ist, zurück.
		/// </summary>
		public int Spieler
		{
			get { return spieler; }
		}
		/// <summary>
		/// Gibt die aktuelle Spielphase zurück
		/// </summary>
		public int SpielPhase
		{
			get { return spielPhase; }
		}
		/// <summary>
		/// Gibt ein 2D-Feld mit Informationen zu den derzeitigen Positionen von allen Spielsteinen zurück.
		/// </summary>
		public int[,] SpielSteineGesetzt
		{
			get { return spielfeldHandler?.SpielSteineGesetzt; }
		}
		/// <summary>
		/// Gibt zurück, ob das Spiel von einem Spieler gewonnen wurde.
		/// </summary>
		public bool SpielVorbei
		{
			get { return spielVorbei; }
		}
		#endregion Felder

		#region Konstruktoren
		public Spiel(Grid grid)
		{
			spielfeldHandler = new SpielfeldHandler(grid);
			spielsteine = new Spielsteine();
		}
		#endregion Konstruktoren

		#region Methoden
		/// <summary>
		/// Stellt den wiederholbaren Spielablauf dar. Muss ständig aufgerufen werden, solange das Spiel nicht vorbei ist.
		/// </summary>
		public void Update()
		{
			if (!spielVorbei)
			{
				if (spielfeldHandler.mausStatus == MouseButtonState.Pressed && Mouse.LeftButton == MouseButtonState.Released)
				{
					int column = 0;
					int row = 0;
					if (spielfeldHandler.ValidateClickedCell(out column, out row))
					{
						Image img = spielfeldHandler.SelectImageControl(column, row);

						//Phase 1: Steine setzen
						if (spielPhase == 1)
						{
							if (img != null && img.Source == null && !steinInZugGesetzt)
							{
								if (spieler == 1)
								{
									img.Source = spielsteine.Weiss;
									weissGesetzt++;
									zugBeendet = true;
									steinInZugGesetzt = true;
								}
								else if (spieler == 2)
								{
									img.Source = spielsteine.Schwarz;
									schwarzGesetzt++;
									zugBeendet = true;
									steinInZugGesetzt = true;
								}
								spielfeldHandler.SpielSteineGesetzt[column, row] = spieler;
								img.Visibility = Visibility.Visible;
								AktualisiereStatus(string.Format("Stein wurde auf Feld {0}/{1} gesetzt.", column, row));

								if (schwarzGesetzt == 9 && weissGesetzt == 9 && zugBeendet)
								{
									spielPhase++;
								}

							}
						}

						//Phase 2: Steine ziehen //Phase 3: ein Spieler hat weniger als 4 Steine und darf frei ziehen
						else if (spielPhase == 2 || spielPhase == 3)
						{
							if (spielfeldHandler.SpielSteineGesetzt[column, row] == spieler && !steinZiehen)
							{
								img.Source = null;
								this.spielfeldHandler.SpielSteineGesetzt[column, row] = 0;
								steinZiehen = true;
								img.Visibility = Visibility.Hidden;
								this.spielfeldHandler.spalteAlt = column;
								this.spielfeldHandler.reiheAlt = row;
								AktualisiereStatus(string.Format("Spieler {0} zieht einen Stein.", spieler), "Setzen Sie ihn auf ein neues Feld.");
							}
							else if (spielfeldHandler.SpielSteineGesetzt[column, row] == 0 && steinZiehen && (spielfeldHandler.DarfZiehen(column, row) || Dienste.Zaehle(spielfeldHandler.SpielSteineGesetzt, spieler) < 4))
							{
								if (spieler == 1)
								{
									img.Source = spielsteine.Weiss;
								}
								else if (spieler == 2)
								{
									img.Source = spielsteine.Schwarz;
								}
								img.Visibility = Visibility.Visible;
								this.spielfeldHandler.SpielSteineGesetzt[column, row] = spieler;
								steinZiehen = false;

								if (!(column == this.spielfeldHandler.spalteAlt && row == this.spielfeldHandler.reiheAlt))
								{
									zugBeendet = true;
									steinInZugGesetzt = true;
									AktualisiereStatus(string.Format("Spieler {0} hat den Stein auf {1}/{2} gesetzt.", spieler, column, row));
								}
								else
								{
									AktualisiereStatus(string.Format("Stein wurde auf gleiches Feld gesetzt."), "Wählen Sie einen neuen Stein.");
								}
							}

							if ((Dienste.Zaehle(spielfeldHandler.SpielSteineGesetzt, 1) < 4 || Dienste.Zaehle(spielfeldHandler.SpielSteineGesetzt, 2) < 4) && zugBeendet && spielPhase == 2)
							{
								spielPhase++;
							}
						}


						//else if (spielPhase == 3)
						//{

						//}


						//in allen Phasen gültige Funktionalitäten
						if (spielfeldHandler.Muehle(column, row, spieler) && steinInZugGesetzt)
						{
							AktualisiereStatus(string.Format("Spieler {0} hat eine Mühle!", spieler), "Entfernen Sie einen Stein des Gegenspielers.");
							spielfeldHandler.hatMuehle[spieler] = true;
							zugBeendet = false;
						}

						if (this.spielfeldHandler.hatMuehle[spieler])
						{
							if (this.spielfeldHandler.SpielSteineGesetzt[column, row] != spieler && this.spielfeldHandler.SpielSteineGesetzt[column, row] != 0)
							{
								if (!this.spielfeldHandler.Muehle(column, row, spieler == 1 ? spieler + 1 : spieler == 2 ? spieler - 1 : -1))
								{
									img.Source = null;
									img.Visibility = Visibility.Hidden;
									this.spielfeldHandler.SpielSteineGesetzt[column, row] = 0;
									this.spielfeldHandler.hatMuehle[spieler] = false;
									zugBeendet = true;
									AktualisiereStatus(string.Format("Spieler {0} hat den Stein auf Feld {1}/{2} entfernt.", spieler, column, row));
								}
								else if (Dienste.Zaehle(this.spielfeldHandler.SpielSteineGesetzt, spieler == 1 ? spieler + 1 : spieler == 2 ? spieler - 1 : -1) < 4)
								{
									this.spielfeldHandler.hatMuehle[spieler] = false;
									zugBeendet = true;
								}
							}
						}


					}
				}

				//jeweiligen Zug beenden
				if (spieler == 1 && !this.spielfeldHandler.hatMuehle[spieler] && zugBeendet)
				{
					spieler++;
					zugBeendet = false;
					steinInZugGesetzt = false;
				}
				else if (spieler == 2 && !this.spielfeldHandler.hatMuehle[spieler] && zugBeendet)
				{
					spieler--;
					zugBeendet = false;
					steinInZugGesetzt = false;
				}

				
				this.spielfeldHandler.mausStatus= Mouse.LeftButton;

				if (this.HatVerloren(spieler))
					this.Verloren?.Invoke(this, new EventArgs());
			}
		}
		private bool HatVerloren(int spieler)
		{
			// Siegbedingungen
			if (Dienste.Zaehle(this.spielfeldHandler.SpielSteineGesetzt, spieler) < 3 && (spielPhase == 2 || spielPhase == 3) && !steinZiehen)
			{
				AktualisiereStatus(string.Format("Spieler {0} hat weniger als 3 Steine!", spieler), string.Format("Spieler {0} hat verloren.", spieler));
				spielVorbei = true;
				return true;
			}


			return false;
		}

		private void AktualisiereStatus(string text, string zusaetzlicherText = "")
		{
			this.Status?.Invoke(this, new StatusEventArgs(text, zusaetzlicherText));
		}
		#endregion Methoden
	}

	/// <summary>
	/// Stellt Texte über aktuellen Status bereit.
	/// </summary>
	public class StatusEventArgs : EventArgs
	{
		/// <summary>
		/// Der entsprechende Text zum Status.
		/// </summary>
		public string Text
		{ get; protected set; }
		/// <summary>
		/// Eventueller zusätlzlicher Text zum Status. Ist nicht immer befüllt.
		/// </summary>
		public string ZusaetzlicherText
		{ get; protected set; }
		public StatusEventArgs(string text, string zusaetzlicherText = "")
		{
			this.Text = text;
			this.ZusaetzlicherText = zusaetzlicherText;
		}
	}
}
