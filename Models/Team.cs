using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace WpfLawyersSystem.Models
{
    public class Team
    {
        string _name;
        double _rating;
        int _wins;
        int _loses;

        [XmlIgnore]
        ObservableCollection<Player> _theCrew;

        public string Name {
            get { return _name; }
            set { _name = value; }
        }
        public double Rating {
            get { return _rating; }
            /*set
            {
                _rating = value;
            }*/
        }
        public int Wins {
            get { return _wins; }
            set
            {
                _wins = value;
                CalculateRating();
            }
        }
        public int Loses {
            get { return _loses; }
            set
            {
                _loses = value;
                CalculateRating();
            }
        }

        [XmlIgnore]
        public ObservableCollection<Player> TheCrew {
            get { return _theCrew; }
            set
            {
                if (value.Count <= 5)
                {
                    _theCrew = value;
                }
                else
                {
                    MessageBox.Show("Команда имеет более 5 игроков");
                }
            }
        }

        // Для серриализации
        public int id_Serialization { get; set; }
        public int[] playersIds_Serialization { get; set; }
        //

        public Team()
        {
            _theCrew = new ObservableCollection<Player>();
        }
        public Team(string name = "", int wins = 0, int loses = 0, ObservableCollection<Player> theCrew = null)
        {
            this._name = name;
            this._wins = wins;
            this._loses = loses;
            CalculateRating();
            _theCrew = (theCrew != null) ? this._theCrew = theCrew : _theCrew = new ObservableCollection<Player>();
        }

        public Team(Team otherTeam)
        {
            this._name = otherTeam.Name;
            this._rating = otherTeam.Rating;
            this._wins = otherTeam.Wins;
            this._loses = otherTeam.Loses;
            if (otherTeam.TheCrew != null)
            {
                this._theCrew = otherTeam.TheCrew;
            }
            else
            {
                this._theCrew = new ObservableCollection<Player>();
            }

        }

        public void CalculateRating() // Можно в будущем поставить private т.к. пересчет будет только внутри класса 
        {
            if (_loses != 0) _rating = _wins / _loses; // Тут можно вставить более сложную формулу
            else _rating = _wins / 1;
        }
    }

    public class ListOfTeams
    {
        public ObservableCollection<Team> List { get; set; }

        public ListOfTeams()
        {
            List = new ObservableCollection<Team>();
        }

        public ListOfTeams(ObservableCollection<Team> ListParam = null)
        {
            List = ListParam;
        }
    }
}
