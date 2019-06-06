using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfLawyersSystem.Models
{
    public class Match
    {
        [XmlIgnore]
        Team _team1;

        [XmlIgnore]
        Team _team2;

        [XmlIgnore]
        Team _winner;

        DateTime _time;

        [XmlIgnore]
        public Team Team1
        {
            get { return _team1; }
            set
            {
                _team1 = value;
            }
        }

        [XmlIgnore]
        public Team Team2
        {
            get { return _team2; }
            set
            {
                _team2 = value;
            }
        }

        [XmlIgnore]
        public Team Winner
        {
            get { return _winner; }
            set
            {
                _winner = value;
            }
        }

        public DateTime Time
        {
            get { return _time; }
            set
            {
                _time = value;
            }
        }


        //Для серриализации
        public int id_Serialization { get; set; }
        public int team1Id_Serialization { get; set; }
        public int team2Id_Serialization { get; set; }
        public int teamWinnerId_Serialization { get; set; }
        //

        public Match()
        {
            _team1 = new Team();
            _team2 = new Team();
            _winner = new Team();
            this._time = DateTime.Now;
        }

        public Match(Team team1 = null, Team team2 = null, Team winner = null)
        {
            this._team1 = team1;
            this._team2 = team2;
            this._winner = winner;
            this._time = DateTime.Now;
        }
    }

    public class ListOfMatches
    {
        public ObservableCollection<Match> List { get; set; }

        public ListOfMatches()
        {
            List = new ObservableCollection<Match>();
        }

        public ListOfMatches(ObservableCollection<Match> ListParam = null)
        {
            if (ListParam != null)
            {
                List = ListParam;
            }
            else
            {
                List = new ObservableCollection<Match>();
            }
        }
    }
}
