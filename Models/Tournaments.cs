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
    public class Tournament
    {
        string _name;
        DateTime _date;
        [XmlIgnore]
        Player _mvp;
        [XmlIgnore]
        ObservableCollection<Match> _matches;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
            }
        }
        [XmlIgnore]
        public Player Mvp
        {
            get { return _mvp; }
            set
            {
                _mvp = value;
            }
        }
        [XmlIgnore]
        public ObservableCollection<Match> Matches
        {
            get { return _matches; }
            set
            {
                _matches = value;
            }
        }

        //для серриализации
        public int id_Serialization { get; set; }
        public int mvpId_Serialization { get; set; }
        public int[] matchesIds_Serialization { get; set; }
        //

        public Tournament() // Из-за datatime пришлось разделить гибридный конструктор на 2
        {
            _name = "someTournament";
            _date = new DateTime();
            _mvp = new Player();
            _matches = new ObservableCollection<Match>();
        }

        public Tournament(string name, DateTime date, Player mvp = null, ObservableCollection<Match> matches = null)
        {
            _name = (name != null) ? _name = name : _name = "someTournament";
            _date = (date != null) ? _date = date : _date = DateTime.Parse("01.01.0001 0:00:00");
            _mvp = (mvp != null) ? _mvp = mvp : _mvp = new Player();
            _matches = (matches != null) ? _matches = matches : _matches = new ObservableCollection<Match>();
        }
    }

    public class ListOfTournaments
    {
        public ObservableCollection<Tournament> List { get; set; }

        public ListOfTournaments()
        {
            List = new ObservableCollection<Tournament>();
        }

        public ListOfTournaments(ObservableCollection<Tournament> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                List.Add(list[i]);
            }
        }
    }
}
