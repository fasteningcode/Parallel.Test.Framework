using System;
using System.Linq;

namespace Parallel.Test.Framework.Lib.DotNet {
    public class DataHelpers {

#region Random Emal Number 
        private static readonly Random Random = new Random();

        public string GenerateMobileNumer(int i = 10) {
            if(i==10)
                return GetRandomNumber(11111, 99999) + GetRandomNumber(11111, 99999).ToString();
            else 
                return GetRandomNumber(11111, 99999) + GetRandomNumber(11111, 99999).ToString();
        }

        private int GetRandomNumber(int min, int max) {
            return Random.Next(min, max);
        }

        public string GenerateEmailAddress() {
            return string.Format("Auto{0}Frame{1}@fc{2}.com", Random.Next(10000), Random.Next(999), Random.Next(9));
        }

        public string GetRandomString(int stringLengthNeeded) {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, stringLengthNeeded)
                .Select(x => x[Random.Next(x.Length)]).ToArray());
        }
        #endregion
        #region Get Street, State 
        public string GetState(string state)
        {
            switch (state)
            {
                case "NSW":
                    return "New South Wales";
                case "VIC":
                    return "Victoria";
                case "ACT":
                    return "Australian Capital Territory";
                case "WA":
                    return "Western Australia";
                case "SA":
                    return "South Australia";
                case "QLD":
                    return "Queensland";
                case "NT":
                    return "Northern Territory";
                case "TAS":
                    return "Tasmania";
            }
            return null;
        }

        #endregion

        public string GetStreetType(string sType)
        {
            switch (sType)
            {
                case "ST":
                case "Street":
                    return "Street";
                case "PL":
                case "Place":
                    return "Place";
                case "CT":
                case "Court":
                    return "Centre";
                case "RD":
                    return "Rd";
                case "AVE":
                case "Avenue":
                    return "Avenue";
                case "SQUARE PORT":
                    return "Rd";
                case "POINT RD":
                    return "Point";
                case "CRS":
                    return "Crossroad";
                case "PDE":
                    return "Pde";
                case "CL":
                    return "Close";
                case "STS":
                    return "Stairs";
                case "DR":
                    return "Drive";
                case "GDN":
                    return "Gdns";
                case "WAY":
                case "Way":
                    return "Way";
                default:
                    return "Rd";
            }
        }

        #region Date 

        // ReSharper disable once UnusedMember.Local
        private string GetDateAsString(int year, int month, int day) {
            var date = new DateTime(year, month, day);
            return date.ToString("yyyy/MM/dd");
        }

        public DateTime ExtractDate(string dateString) {
            var dateConverted = Convert.ToDateTime(dateString);
            // Converting the DateTime to String format 31/12/2016
            var shortDate = dateConverted.ToString("dd/MM/yyyy");
            //Converting the string to DateTimeFormat 31/12/2016
            dateConverted = DateTime.ParseExact(shortDate, "dd/MM/yyyy", null);

            return dateConverted;
        }

        #endregion
    }
}