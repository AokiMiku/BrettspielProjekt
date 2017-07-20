using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrettspielCore
{
	/// <summary>
	/// Spezielle Dienste, die programmweit benutzt werden können.
	/// </summary>
	public static class Dienste
	{
		#region Felder

		#endregion Felder

		#region Konstruktoren

		#endregion Konstruktoren

		#region Methoden

		public static string TimeToString(double verstricheneZeit)
		{
			double sekunden = verstricheneZeit / 1000;
			double minuten = 0;
			double stunden = 0;

			while (sekunden >= 1)
			{
				sekunden--;
			}
			double millisekunden = Math.Round(sekunden * 1000);
			sekunden = Math.Round(verstricheneZeit / 1000);

			while (sekunden >= 60)
			{
				minuten++;
				sekunden -= 60;
			}

			while (minuten >= 60)
			{
				stunden++;
				minuten -= 60;
			}

			return stunden.ToString("00") + ":" + minuten.ToString("00") + ":" + sekunden.ToString("00") + "." + millisekunden.ToString("000");
		}

		public static int Zaehle<T>(T[] collection, T where)
		{
			int rtn = 0;

			foreach (T item in collection)
			{
				if (item.Equals(where))
				{
					rtn++;
				}
			}

			return rtn;
		}
		public static int Zaehle<T>(T[,] collection, T where)
		{
			int rtn = 0;

			foreach (T item in collection)
			{
				if (item.Equals(where))
				{
					rtn++;
				}
			}

			return rtn;
		}

		#endregion Methoden
	}
}
