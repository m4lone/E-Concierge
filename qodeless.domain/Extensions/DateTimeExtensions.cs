using System;
using System.Globalization;

namespace qodeless.domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToJLLDateTime(this string dateStr)
        {
            try
            {
                string[] formats = { "MM/dd/yyyy hh:mm:ss", "MM/dd/yyyy", "yyyy/MM/dd hh:mm:ss", "yyyy/MM/dd" };
                return DateTime.ParseExact(dateStr, formats, new CultureInfo("en-US"), DateTimeStyles.None);
            }
            catch (Exception)
            {
                return new DateTime(1970,1,1);
            }            
        }

        public static DateTime ToJLLDate(this string dateStr)
        {
            try
            {
                string[] formats = { "MM-yyyy" };
                return DateTime.ParseExact(dateStr, formats, new CultureInfo("en-US"), DateTimeStyles.None);
            }
            catch (Exception)
            {
                return new DateTime(1970, 1, 1);
            }
        }

        public static DateTime ToJLLDatePattern(this string dateStr)
        {
            try
            {
                string[] formats = { "MM/dd/yyyy HH:mm:ss" };
                return DateTime.ParseExact(dateStr, formats, new CultureInfo("es-ESP"), DateTimeStyles.None);
            }
            catch (Exception)
            {
                return new DateTime(1970, 1, 1);
            }
        }

        public static DateTime ToJLLDateTimePattern(this string dateStr)
        {
            try
            {
                var aux = dateStr.Split('/'); // 13/08/2021
                var auxYear = aux[2].Split(' ');// 2021 00:00:00 -> 2021
                return new DateTime(Convert.ToInt32(auxYear[0]), Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return new DateTime(1970, 1, 1);
            }
        }

        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 23, 59, 59).AddMonths(1).AddDays(-1);
        }

        public static DateTime ToLastMoment(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }

        public static DateTime ToFirstMoment(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }

        public static bool IsBetween(this DateTime date, DateTime begin, DateTime end)
        {
            return date >= begin && date <= end.ToLastMoment();
        }

        public static string ToMonthString(this DateTime date)
        {
            switch (date.Month)
            {
                case 1: return "Janeiro";
                case 2: return "Fevereiro";
                case 3: return "Marco";
                case 4: return "Abril";
                case 5: return "Maio";
                case 6: return "Junho";
                case 7: return "Julho";
                case 8: return "Agosto";
                case 9: return "Setembro";
                case 10: return "Outubro";
                case 11: return "Novembro";
                case 12: return "Dezembro";
                default:
                    break;
            }

            return "";
        }

        public static string ToShortMonthString(this DateTime date)
        {
            switch (date.Month)
            {
                case 1: return "Jan " + date.Year.ToString();
                case 2: return "Fev " + date.Year.ToString();
                case 3: return "Mar " + date.Year.ToString();
                case 4: return "Abr " + date.Year.ToString();
                case 5: return "Mai " + date.Year.ToString();
                case 6: return "Jun " + date.Year.ToString();
                case 7: return "Jul " + date.Year.ToString();
                case 8: return "Ago " + date.Year.ToString();
                case 9: return "Set " + date.Year.ToString();
                case 10: return "Out " + date.Year.ToString();
                case 11: return "Nov " + date.Year.ToString();
                case 12: return "Dez " + date.Year.ToString();
                default:
                    break;
            }

            return "";
        }
        
        public static DateTime ToDateTime(string data)
        {
   
            string[] formats = { "MM/dd/yyyy hh:mm:ss" };
            var dataFormatada = DateTime.ParseExact(data, formats, new CultureInfo("en-US"), DateTimeStyles.None);

            return dataFormatada;
        }

    }
}
