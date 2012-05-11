using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Touchality.FoursquareApi;

namespace WP7Square.Classes
{
    public static class FS
    {
        #region Badge

        public static string BadgeGetDescription(Badge badge)
        {
            if (badge == null || string.IsNullOrEmpty(badge.description))
                return string.Empty;

            return badge.description;
        }

        public static string BadgeGetName(Badge badge)
        {
            if (badge == null || string.IsNullOrEmpty(badge.name))
                return string.Empty;

            return badge.name;
        }

        public static bool BadgeHasIcon(Badge badge)
        {
            if (badge == null || string.IsNullOrEmpty(badge.icon))
                return false;

            return true;
        }

        #endregion

        #region CheckIn Has

        public static bool CheckInHasUserHasPhoto(CheckIn checkIn)
        {
            if (checkIn == null || checkIn.user == null || string.IsNullOrEmpty(checkIn.user.photo))
                return false;

            return true;
        }

        public static bool CheckInHasVenueHasName(CheckIn checkIn)
        {
            if (checkIn == null || checkIn.venue == null || string.IsNullOrEmpty(checkIn.venue.name))
                return false;

            return true;
        }

        public static bool CheckInHasMessage(CheckIn checkIn)
        {
            if (checkIn == null || string.IsNullOrEmpty(checkIn.message))
                return false;

            return true;
        }

        public static bool CheckInHasMayor(CheckIn checkIn)
        {
            if (checkIn == null || checkIn.mayor == null)
                return false;

            return true;
        }

        public static bool CheckInHasMayorHasCheckIn(CheckIn checkIn)
        {
            if (checkIn == null || checkIn.mayor == null || string.IsNullOrEmpty(checkIn.mayor.checkins))
                return false;

            return true;
        }

        public static bool CheckInHasMayorHasMessage(CheckIn checkIn)
        {
            if (checkIn == null || checkIn.mayor == null || string.IsNullOrEmpty(checkIn.mayor.message))
                return false;

            return true;
        }

        public static bool CheckInHasScoring(CheckIn checkIn)
        {
            if (checkIn == null || checkIn.scoring == null || checkIn.scoring.Count == 0)
                return false;

            return true;
        }

        public static bool CheckInHasScoringHasPoints(CheckIn checkIn)
        {
            if (checkIn == null || checkIn.scoring == null || checkIn.scoring.Count == 0)
                return false;

            return true;
        }

        #endregion

        #region Check Get

        public static string CheckInGetUserGetPhoto(CheckIn checkIn)
        {
            if (checkIn == null || checkIn.user == null || string.IsNullOrEmpty(checkIn.user.photo))
                return string.Empty;

            return checkIn.user.photo;
        }

        public static string CheckInGetCreatedDate(CheckIn checkIn)
        {
            if (checkIn == null || string.IsNullOrEmpty(checkIn.created))
                return string.Empty;

            return checkIn.created;
        }

        public static string CheckInGetUserGetId(CheckIn checkIn)
        {
            if (checkIn == null || checkIn.user == null || string.IsNullOrEmpty(checkIn.user.id))
                return string.Empty;

            return checkIn.user.id;
        }

        public static string CheckInGetDisplay(CheckIn checkIn)
        {
            if (checkIn == null || string.IsNullOrEmpty(checkIn.display))
                return string.Empty;

            return checkIn.display;
        }

        public static string CheckInGetVenueGetName(CheckIn checkIn)
        {
            if (checkIn == null || checkIn.venue == null || string.IsNullOrEmpty(checkIn.venue.name))
                return string.Empty;

            return checkIn.venue.name;
        }

        public static string CheckInGetMessage(CheckIn checkIn)
        {
            if (checkIn == null || string.IsNullOrEmpty(checkIn.message))
                return string.Empty;

            return checkIn.message;
        }

        public static string CheckInGetMayorGetCheckIn(CheckIn checkIn)
        {
            if (checkIn == null || checkIn.mayor == null || string.IsNullOrEmpty(checkIn.mayor.checkins))
                return string.Empty;

            return checkIn.mayor.checkins;
        }

        public static string CheckInGetMayorGetMessage(CheckIn checkIn)
        {
            if (checkIn == null || checkIn.mayor == null || string.IsNullOrEmpty(checkIn.mayor.message))
                return string.Empty;

            return checkIn.mayor.message;
        }

        #endregion

        #region Score

        public static string ScoreGetPoints(Score score)
        {
            if (score == null || string.IsNullOrEmpty(score.points))
                return string.Empty;

            return score.points;
        }

        public static string ScoreGetMessage(Score score)
        {
            if (score == null || string.IsNullOrEmpty(score.message))
                return string.Empty;

            return score.message;
        }

        public static bool ScoreHasPoints(Score score)
        {
            if (score == null || string.IsNullOrEmpty(score.points))
                return false;

            return true;
        }

        #endregion

        #region User Has

        public static bool UserHasPhoto(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.photo))
                return false;

            return true;
        }

        public static bool UserHasCheckInHasVenue(User user)
        {
            if (user == null || user.checkin == null || user.checkin.venue == null)
                return false;

            return true;
        }

        public static bool UserHasCheckIn(User user)
        {
            if (user == null || user.checkin == null)
                return false;

            return true;
        }

        public static bool UserHasBadges(User user)
        {
            if (user == null || user.badges == null || user.badges.Count == 0)
                return false;

            return true;
        }

        #endregion

        #region User Get

        public static string UserGetPhoto(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.photo))
                return string.Empty;

            return user.photo;
        }

        public static string UserGetId(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.id))
                return string.Empty;

            return user.id;
        }

        public static string UserGetEmail(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.email))
                return string.Empty;

            return user.email;
        }

        public static string UserGetPhone(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.phone))
                return string.Empty;

            return user.phone;
        }

        public static Venue UserGetCheckInGetVenue(User user)
        {
            if (user == null || user.checkin == null)
                return null;

            return user.checkin.venue;
        }

        public static string UserGetCheckInGetVenueGetAddress(User user)
        {
            if (user == null || user.checkin == null || user.checkin.venue == null || string.IsNullOrEmpty(user.checkin.venue.address))
                return string.Empty;

            return user.checkin.venue.address;
        }

        public static string UserGetCheckInGetVenueGetName(User user)
        {
            if (user == null || user.checkin == null || user.checkin.venue == null || string.IsNullOrEmpty(user.checkin.venue.name))
                return string.Empty;

            return user.checkin.venue.name;
        }

        public static string UserGetCheckInGetCreatedDate(User user)
        {
            if (user == null || user.checkin == null || string.IsNullOrEmpty(user.checkin.created))
                return string.Empty;

            return user.checkin.created;
        }

        public static string UserGetFirstName(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.firstname))
                return string.Empty;

            return user.firstname;
        }

        public static string UserGetLastName(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.lastname))
                return string.Empty;

            return user.lastname;
        }

        #endregion

        #region Venue Has

        public static bool VenueHasPhone(Venue venue)
        {
            if (venue == null || venue.phone == null)
                return false;

            return true;
        }

        public static bool VenueHasZip(Venue venue)
        {
            if (venue == null || venue.zip == null)
                return false;

            return true;
        }

        public static bool VenueHasState(Venue venue)
        {
            if (venue == null || venue.state == null)
                return false;

            return true;
        }

        public static bool VenueHasCity(Venue venue)
        {
            if (venue == null || venue.city == null)
                return false;

            return true;
        }

        public static bool VenueHasCrossStreet(Venue venue)
        {
            if (venue == null || venue.crossstreet == null)
                return false;

            return true;
        }

        public static bool VenueHasAddress(Venue venue)
        {
            if (venue == null || venue.address == null)
                return false;

            return true;
        }

        public static bool VenueHasStatsHasBeenHere(Venue venue)
        {
            if (venue == null || venue.stats == null || venue.stats.beenhere == null)
                return false;

            return true;
        }

        public static bool VenueHasStats(Venue venue)
        {
            if (venue == null || venue.stats == null)
                return false;

            return true;
        }

        public static bool VenueHasName(Venue venue)
        {
            if (venue == null || string.IsNullOrEmpty(venue.name))
                return false;

            return true;
        }

        #endregion

        #region Venue Get

        public static string VenueGetPhone(Venue venue)
        {
            if (venue == null || string.IsNullOrEmpty(venue.phone))
                return string.Empty;

            return venue.phone;
        }

        public static string VenueGetZip(Venue venue)
        {
            if (venue == null || string.IsNullOrEmpty(venue.zip))
                return string.Empty;

            return venue.zip;
        }

        public static string VenueGetState(Venue venue)
        {
            if (venue == null || string.IsNullOrEmpty(venue.state))
                return string.Empty;

            return venue.state;
        }

        public static string VenueGetCrossStreet(Venue venue)
        {
            if (venue == null || string.IsNullOrEmpty(venue.crossstreet))
                return string.Empty;

            return venue.crossstreet;
        }

        public static string VenueGetAddress(Venue venue)
        {
            if (venue == null || string.IsNullOrEmpty(venue.address))
                return string.Empty;

            return venue.address;
        }

        public static bool VenueGetStatsGetBeenHereGetFriends(Venue venue)
        {
            if (venue == null || venue.stats == null || venue.stats.beenhere == null)
                return false;

            return venue.stats.beenhere.friends;
        }

        public static bool VenueGetStatsGetBeenHereGetMe(Venue venue)
        {
            if (venue == null || venue.stats == null || venue.stats.beenhere == null)
                return false;

            return venue.stats.beenhere.me;
        }

        public static int VenueGetStatsGetCheckIns(Venue venue)
        {
            if (venue == null || venue.stats == null)
                return 0;

            return venue.stats.checkins;
        }

        public static string VenueGetDistance(Venue venue)
        {
            if (venue == null || string.IsNullOrEmpty(venue.distance))
                return string.Empty;

            return venue.distance;
        }

        public static string VenueGetName(Venue venue)
        {
            if (venue == null || string.IsNullOrEmpty(venue.name))
                return string.Empty;

            return venue.name;
        }

        public static string VenueGetCity(Venue venue)
        {
            if (venue == null || string.IsNullOrEmpty(venue.city))
                return string.Empty;

            return venue.city;
        }

        #endregion
    }
}
