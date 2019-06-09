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
using System.Threading;

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
        public static void SerializeAbstract<ListType>(ListType list, string way)
        {
            var formatter = new XmlSerializer(list.GetType());
            // PreserveObjectReferences true разрешает цикличные ссылки (ссылки объектов друг на друга)
            using (Stream fs = new FileStream(way, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(fs, list);
            }
        }
        public static void DeserializeAbstract<ListType>(ListType list, string way)
        {
            if (File.Exists(way))
            {
                using (FileStream fs = new FileStream(way, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        XmlSerializer formatter = new XmlSerializer(list.GetType());
                        list = (ListType)formatter.Deserialize(fs);// as ListType;
                    }
                    catch (InvalidOperationException e)
                    {
                        MessageBox.Show(string.Format("Ошибка при загрузке(дессериализации) {0}: {1}", way, e.Message), "Ошибка", MessageBoxButton.OK);
                    }
                }
            }
        }

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
                        XmlSerializer formatter = new XmlSerializer(ObjMatches.GetType());
                        ObjMatches = formatter.Deserialize(fs) as ListOfMatches;
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
                        XmlSerializer formatter = new XmlSerializer(ObjPlayers.GetType());
                        ObjPlayers = formatter.Deserialize(fs) as ListOfPlayers;
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
                        XmlSerializer formatter = new XmlSerializer(ObjTeams.GetType());
                        ObjTeams = formatter.Deserialize(fs) as ListOfTeams;
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
                        XmlSerializer formatter = new XmlSerializer(ObjTop.GetType());
                        ObjTop = formatter.Deserialize(fs) as Top10;
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
                        XmlSerializer formatter = new XmlSerializer(ObjTournaments.GetType());
                        ObjTournaments = formatter.Deserialize(fs) as ListOfTournaments;
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
            Views.LoadSaveTheme loadWindow = new Views.LoadSaveTheme("Сохранение");
            loadWindow.Show();
            //Раздача собственных id
            for (int i = 0; i < ObjMatches.List.Count; i++) ObjMatches.List[i].id_Serialization = i;
            for (int i = 0; i < ObjPlayers.List.Count; i++) ObjPlayers.List[i].id_Serialization = i;
            for (int i = 0; i < ObjTeams.List.Count; i++) ObjTeams.List[i].id_Serialization = i;
            for (int i = 0; i < ObjTop.Top.Count; i++) ObjTop.Top[i].id_Serialization = i;
            for (int i = 0; i < ObjTournaments.List.Count; i++) ObjTournaments.List[i].id_Serialization = i;

            ///Раздача внешних ключей
            //Матчам
            for (int i = 0; i < ObjMatches.List.Count; i++)
            {
                ObjMatches.List[i].team1Id_Serialization = ObjMatches.List[i].Team1.id_Serialization;
                ObjMatches.List[i].team2Id_Serialization = ObjMatches.List[i].Team2.id_Serialization;
                ObjMatches.List[i].teamWinnerId_Serialization = ObjMatches.List[i].Winner.id_Serialization;
            }

            //Игрокам
            for (int i = 0; i < ObjPlayers.List.Count; i++)
            {
                ObjPlayers.List[i].teamId_Serialization = ObjPlayers.List[i].Team.id_Serialization;
            }

            //Командам
            for (int i = 0; i < ObjTeams.List.Count; i++)
            {
                ObjTeams.List[i].playersIds_Serialization = new int[ObjTeams.List[i].TheCrew.Count];
                if (ObjTeams.List[i].TheCrew != null && ObjTeams.List[i].TheCrew.Count > 0)
                {
                    for (int k = 0; k < ObjTeams.List[i].TheCrew.Count; k++)
                    {
                        ObjTeams.List[i].playersIds_Serialization[k] = ObjTeams.List[i].TheCrew[k].id_Serialization;
                    }
                }
            }

            //Топам
            ObjTop.teamIds_Serialization = new int[ObjTop.Top.Count];
            for (int i = 0; i < ObjTop.Top.Count; i++)
            {
                ObjTop.teamIds_Serialization[i] = ObjTop.Top[i].id_Serialization;
            }

            //Турнирам
            for (int i = 0; i < ObjTournaments.List.Count; i++)
            {
                ObjTournaments.List[i].mvpId_Serialization = ObjTournaments.List[i].Mvp.id_Serialization;
                ObjTournaments.List[i].matchesIds_Serialization = new int[ObjTournaments.List[i].Matches.Count];
                for (int k = 0; k < ObjTournaments.List[i].Matches.Count; k++)
                {
                    ObjTournaments.List[i].matchesIds_Serialization[k] = ObjTournaments.List[i].Matches[k].id_Serialization;
                }
            } 

            SerializeAbstract(ObjMatches, @"..\..\DataBase\Matches.xml");
            SerializeAbstract(ObjPlayers, @"..\..\DataBase\Players.xml");
            SerializeAbstract(ObjTeams, @"..\..\DataBase\Teams.xml");
            SerializeAbstract(ObjTop, @"..\..\DataBase\Top.xml");
            SerializeAbstract(ObjTournaments, @"..\..\DataBase\Tournaments.xml");
            Thread.Sleep(500);
            loadWindow.Close();

        }
        public static void Load()
        {
            Views.LoadSaveTheme loadWindow = new Views.LoadSaveTheme("Загрузка");
            loadWindow.Show();

            DeserializeMatches();
            DeserializePlayers();
            DeserializeTeams();
            DeserializeTop();
            DeserializeTournaments();

            ///Раздача внешних ключей
            //Матчам
            for (int i = 0; i < ObjMatches.List.Count; i++)
            {
                ObjMatches.List[i].Team1 = ObjTeams.List.ToList().Find(item => item.id_Serialization == ObjMatches.List[i].team1Id_Serialization);
                ObjMatches.List[i].Team2 = ObjTeams.List.ToList().Find(item => item.id_Serialization == ObjMatches.List[i].team2Id_Serialization);
                ObjMatches.List[i].Winner = ObjTeams.List.ToList().Find(item => item.id_Serialization == ObjMatches.List[i].teamWinnerId_Serialization);
            }

            //Игрокам
            for (int i = 0; i < ObjPlayers.List.Count; i++)
            {
                ObjPlayers.List[i].Team = ObjTeams.List.ToList().Find(item => item.id_Serialization == ObjPlayers.List[i].teamId_Serialization);
            }

            //Командам
            for (int i = 0; i < ObjTeams.List.Count; i++)
            {
                for (int k = 0; k < ObjTeams.List[i].playersIds_Serialization.Length; k++)
                {
                    ObjTeams.List[i].TheCrew.Add(ObjPlayers.List.ToList().Find(item => item.teamId_Serialization == ObjTeams.List[i].id_Serialization));
                }
            }

            //Топам
            for (int i = 0; i < ObjTop.Top.Count; i++)
            {
                ObjTop.teamIds_Serialization[i] = ObjTop.Top[i].id_Serialization;
                ObjTop.Top[i] = ObjTeams.List.ToList().Find(item => item.id_Serialization == ObjTop.Top[i].id_Serialization);
            }

            //Турнирам
            for (int i = 0; i < ObjTournaments.List.Count; i++)
            {
                ObjTournaments.List[i].Mvp = ObjPlayers.List.ToList().Find(item => item.id_Serialization == ObjTournaments.List[i].mvpId_Serialization);
                for (int k = 0; k < ObjTournaments.List[i].matchesIds_Serialization.Length; k++)
                {
                    ObjTournaments.List[i].Matches.Add(ObjMatches.List.ToList().Find(item => item.id_Serialization == ObjTournaments.List[i].matchesIds_Serialization[k]));
                }
            }
            Thread.Sleep(500);
            loadWindow.Close();
        }
    }
}
