using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace WpfLawyersSystem.Models
{ 
    // Статическая основная база данных.
    public class FactoryOfLists
    {
        private static ListOfPlayers _objPlayers;
        private static ListOfTeams _objTeams;
        private static ListOfMatches _objMatches;
        private static ListOfTournaments _objTournaments;
        private static Top10 _objTop;

        /// <summary>
        /// ////Свойства данных
        /// </summary>
        public static ListOfPlayers ObjPlayers
        {
            get
            {

                if (_objPlayers == null)
                {
                    _objPlayers = new ListOfPlayers();
                    /*                                                      //До серриализации это была инициализация тестовой бд
                    _objPlayers.List.Add(new Player("Zeus", 31, 0.95));
                    _objPlayers.List.Add(new Player("Edward", 32, 0.98));
                    ObservableCollection<Player> players = new ObservableCollection<Player>();
                    players.Add(_objPlayers.List[0]);
                    players.Add(_objPlayers.List[1]);
                    ObjTeams.List.Add(new Team("NaVi", 1.05, 15, 20, players));

                    _objPlayers.List.Add(new Player("NiKo", 26, 1.44));
                    _objPlayers.List.Add(new Player("Rain", 25, 1.20));
                    players.Clear();
                    players = new ObservableCollection<Player>();
                    players.Add(_objPlayers.List[0]);
                    players.Add(_objPlayers.List[0]);
                    ObjTeams.List.Add(new Team("FaZe", 1.12, 22, 10, players));
                    _objPlayers.List.Add(new Player("ЖМА", 54, 1.54, ObjTeams.List[0]));
                    _objPlayers.List.Add(new Player("Детров", 27, 2.28, ObjTeams.List[1]));
                    */
                }
                return _objPlayers;
            }
            set
            {
                _objPlayers = value;
            }
        }
        public static ListOfTeams ObjTeams
        {
            get
            {
                if (_objTeams == null)
                    _objTeams = new ListOfTeams();
                return _objTeams;
            }
            set
            {
                _objTeams = value;
            }
        }
        public static ListOfMatches ObjMatches
        {
            get
            {
                if (_objMatches == null)
                    _objMatches = new ListOfMatches();
                return _objMatches;
            }
            set
            {
                _objMatches = value;
            }
        }
        public static ListOfTournaments ObjTournaments
        {
            get
            {
                if (_objTournaments == null)
                    _objTournaments = new ListOfTournaments();
                return _objTournaments;
            }
            set
            {
                _objTournaments = value;
            }
        }
        public static Top10 ObjTop
        {
            get
            {
                if (_objTop == null)
                    _objTop = new Top10();
                return _objTop;
            }
            set
            {
                _objTop = value;
            }
        }

        private FactoryOfLists()  // стандартный конструктор.
        { }

        /// <summary>
        /// /// Методы сохранения и загрузки данных из файла
        /// </summary>
        public static void SerializeMatches(string way = @"..\..\DataBase\Matches.xml")
        {
            var formatter = new DataContractSerializer(ObjMatches.GetType(), new DataContractSerializerSettings { PreserveObjectReferences = true});
            // PreserveObjectReferences true разрешает цикличные ссылки (ссылки объектов друг на друга)
            using (Stream fs = new FileStream(way, FileMode.OpenOrCreate, FileAccess.Write))
            {
                formatter.WriteObject(fs, _objMatches);
            }
        }
        public static void SerializePlayers(string way = @"..\..\DataBase\Players.xml")
        {
            var formatter = new DataContractSerializer(ObjPlayers.GetType(), new DataContractSerializerSettings { PreserveObjectReferences = true });
            //XmlSerializer formatter = new XmlSerializer(ObjPlayers.GetType());
            using (FileStream fs = new FileStream(way, FileMode.Create, FileAccess.Write))
            {
                formatter.WriteObject(fs, _objPlayers);
            }
        }
        public static void SerializeTeams(string way = @"..\..\DataBase\Teams.xml")
        {
            var formatter = new DataContractSerializer(ObjTeams.GetType(), new DataContractSerializerSettings { PreserveObjectReferences = true });
            //XmlSerializer formatter = new XmlSerializer(ObjTeams.GetType());
            using (FileStream fs = new FileStream(way, FileMode.Create, FileAccess.Write))
            {
                formatter.WriteObject(fs, _objTeams);
            }
        }
        public static void SerializeTop(string way = @"..\..\DataBase\Top.xml")
        {
            var formatter = new DataContractSerializer(ObjTop.GetType(), new DataContractSerializerSettings { PreserveObjectReferences = true });
            //XmlSerializer formatter = new XmlSerializer(ObjTop.GetType());
            using (FileStream fs = new FileStream(way, FileMode.Create, FileAccess.Write))
            {
                formatter.WriteObject(fs, _objTop);
            }
        }
        public static void SerializeTournaments(string way = @"..\..\DataBase\Tournaments.xml")
        {
            var formatter = new DataContractSerializer(ObjTournaments.GetType(), new DataContractSerializerSettings { PreserveObjectReferences = true });
            //XmlSerializer formatter = new XmlSerializer(ObjTournaments.GetType());
            using (FileStream fs = new FileStream(way, FileMode.Create, FileAccess.Write))
            {
                formatter.WriteObject(fs, _objTournaments);
            }
        }

        public static void DeserializeMatches(string way = @"..\..\DataBase\Matches.xml")
        {
            if (File.Exists(way))
            {
                using (FileStream fs = new FileStream(way, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        DataContractSerializer formatter = new DataContractSerializer(ObjMatches.GetType());
                        ObjMatches = formatter.ReadObject(fs) as ListOfMatches;
                    }
                    catch (InvalidOperationException e)
                    {
                        MessageBox.Show(string.Format("Ошибка при загрузке(дессериализации) матчей: {0}", e.Message), "Ошибка", MessageBoxButton.OK);
                    }
                }
            }
        }
        public static void DeserializePlayers(string way = @"..\..\DataBase\Players.xml")
        {
            if (File.Exists(way))
            {
                using (FileStream fs = new FileStream(way, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        DataContractSerializer formatter = new DataContractSerializer(ObjPlayers.GetType(),
                            new DataContractSerializerSettings { PreserveObjectReferences = true });
                        ObjPlayers = formatter.ReadObject(fs) as ListOfPlayers;
                    }
                    catch (InvalidOperationException e)
                    {
                        MessageBox.Show(string.Format("Ошибка при загрузке(дессериализации) игроков: {0}", e.Message), "Ошибка", MessageBoxButton.OK);
                    }
                }
            }
        }
        public static void DeserializeTeams(string way = @"..\..\DataBase\Teams.xml")
        {
            if (File.Exists(way))
            {
                using (FileStream fs = new FileStream(way, FileMode.Open))
                {
                    try
                    {
                        DataContractSerializer formatter = new DataContractSerializer(ObjTeams.GetType());
                        ObjTeams = (ListOfTeams)formatter.ReadObject(fs);
                    }
                    catch (InvalidOperationException e)
                    {
                        MessageBox.Show(string.Format("Ошибка при загрузке(дессериализации) комманд: {0}", e.Message), "Ошибка", MessageBoxButton.OK);
                    }
                }
            }
        }
        public static void DeserializeTop(string way = @"..\..\DataBase\Top.xml")
        {
            if (File.Exists(way))
            {
                using (FileStream fs = new FileStream(way, FileMode.Open))
                {
                    try
                    {
                        DataContractSerializer formatter = new DataContractSerializer(ObjTop.GetType());
                        ObjTop = (Top10)formatter.ReadObject(fs);
                    }
                    catch (InvalidOperationException e)
                    {
                        MessageBox.Show(string.Format("Ошибка при загрузке(дессериализации) топа: {0}", e.Message), "Ошибка", MessageBoxButton.OK);
                    }
                }
            }
        }
        public static void DeserializeTournaments(string way = @"..\..\DataBase\Tournaments.xml")
        {
            if (File.Exists(way))
            {
                using (FileStream fs = new FileStream(way, FileMode.Open))
                { // Отладчик сказал что так можно делать и файл закроется без обязательного закрытия его в Finnally?
                    try
                    {
                        DataContractSerializer formatter = new DataContractSerializer(ObjTournaments.GetType());
                        ObjTournaments = (ListOfTournaments)formatter.ReadObject(fs);
                    }
                    catch (InvalidOperationException e)
                    {
                        MessageBox.Show(string.Format("Ошибка при загрузке(дессериализации) турниров: {0}", e.Message), "Ошибка", MessageBoxButton.OK);
                    }
                }
            }

        }

        public static void Save()
        {
            SerializeMatches();
            SerializePlayers();
            SerializeTeams();
            SerializeTop();
            SerializeTournaments();
        }
        public static void Load()
        {
            DeserializeMatches();
            DeserializePlayers();
            DeserializeTeams();
            DeserializeTop();
            DeserializeTournaments();
        }
    }
}
