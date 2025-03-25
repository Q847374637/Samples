using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using System.Data;
using System.IO;
using Samples.Entities.SampleActionEntities;
using System.Threading;

namespace Samples
{
    internal class Selection : LoggedSampleAction
    {
        OleDbConnection conn = new OleDbConnection();
        OleDbDataAdapter adapt = new OleDbDataAdapter();
        OleDbCommand cmd = new OleDbCommand();
        DataSet dt = new DataSet();

        public void SelectionStart(List<String> pathGet, List<String> extensions, List<String> columns, List<String> finalColumns, List<String> dbase, int i = 0, string colList = "", string valList = "")
        {
            try
            {
                switch (Thread.CurrentThread.Name)
                {
                    case ("DelaySample"):
                        Delay(pathGet, extensions, columns, finalColumns, dbase, i, colList, valList);
                        break;
                    case ("EasypaySample"):
                        Easypay(pathGet, extensions, columns, finalColumns, dbase, i, colList, valList);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exception)
            {
                base.WriteToLogError($"Ошибка (неопознанная). Подробней: {exception}");
                throw;
            }
        }

        public void Delay(List<String> pathGet, List<String> extensions, List<String> columns, List<String> finalColumns, List<String> dbase, int i, string colList, string valList)
        {
            base.WriteToLogInformation($"Начало процесса обработки выборки Delay");

            ReadData(pathGet[0], dbase, extensions, columns, "out", dbase.Last());

            base.WriteToLogInformation($"Начало выполнения запросов выборки\nВыполнение запроса с id = 0");

            var tq0 = from D in dt.Tables["D"].AsEnumerable()
                      from S61 in dt.Tables["S61"].AsEnumerable().Where(S61 => ((D.Field<string>("Фамилия") == S61.Field<string>("S61__04") && D.Field<string>("Имя") == S61.Field<string>("S61__05") && D.Field<string>("Отчество") == S61.Field<string>("S61__06")) && (D.Field<string>("Серия, номер паспорта") == S61.Field<string>("S61__03") || (D["Дата рождения"] is DBNull ? (DateTime?)null : Convert.ToDateTime(D["Дата рождения"]))== S61.Field<DateTime?>("S61__07") ||  D.Field<string>("Идентификационный номер") == S61.Field<string>("S61__18")))).DefaultIfEmpty()
                      group new { D, S61 } by new { ID = D["ID"], Орган = D["Орган"], ВхНомерЗапроса = D["Вх № запроса"], Исполнитель = D["Исполнитель"], Фамилия = D["Фамилия"], Имя = D["Имя"], Отчество = D["Отчество"], S61__16 = S61 == null ? "" : S61["S61__16"], Идентификационныйномер = S61 == null ? "" : (D["Идентификационный номер"] == S61["S61__18"] ? D["Идентификационный номер"] : S61["S61__18"]), ДатаРождения = S61 == null ? "" : (D["Дата рождения"] == S61["S61__07"] ? D["Дата рождения"] : S61["S61__07"]), СерияНомерПаспорта = D["Серия, номер паспорта"], База = D["База"], НомерСчета = D["Номер счета"], Валюта = D["Валюта"], ДатаПоследнегоЗачисления = D["Дата последнего зачисления"], ДатаПоследнейОперации = D["Дата последней операции"], Остаток = D["Остаток"], Овердрафт = D["Овердрафт"], Арест = D["Арест"] } into D_S61_grouped
                      select new
                      {
                          ID = D_S61_grouped.Key.ID,
                          Орган = D_S61_grouped.Key.Орган,
                          ВхНомерЗапроса = D_S61_grouped.Key.ВхНомерЗапроса,
                          Исполнитель = D_S61_grouped.Key.Исполнитель,
                          Фамилия = D_S61_grouped.Key.Фамилия,
                          Имя = D_S61_grouped.Key.Имя,
                          Отчество = D_S61_grouped.Key.Отчество,
                          S61__16 = D_S61_grouped.Key.S61__16 ?? String.Empty,
                          Идентификационныйномер = D_S61_grouped.Key.Идентификационныйномер,
                          ДатаРождения = D_S61_grouped.Key.ДатаРождения,
                          СерияНомерПаспорта = D_S61_grouped.Key.СерияНомерПаспорта,
                          База = D_S61_grouped.Key.База,
                          НомерСчета = D_S61_grouped.Key.НомерСчета,
                          Валюта = D_S61_grouped.Key.Валюта,
                          Остаток = D_S61_grouped.Key.Остаток,
                          Овердрафт = D_S61_grouped.Key.Овердрафт,
                          ДатаПоследнегоЗачисления = D_S61_grouped.Key.ДатаПоследнегоЗачисления,
                          ДатаПоследнейОперации = D_S61_grouped.Key.ДатаПоследнейОперации,
                          Арест = D_S61_grouped.Key.Арест
                      };

            base.WriteToLogInformation($"Выполнение запроса с id = 0 завершено успешно\nВыполнение запроса с id = 1");

            var tq1 = from S6 in dt.Tables["S6"].AsEnumerable()
                      where S6.Field<string>("S6__02") == "PE" || S6.Field<string>("S6__02") == "BA"
                      select new
                      {
                          S6__02 = S6.Field<string>("S6__02"),
                          S6__03 = S6.Field<string>("S6__03"),
                          S6__08 = S6.Field<DateTime?>("S6__08"),
                          S6__09 = S6.Field<DateTime?>("S6__09"),
                          S6__13 = S6["S6__13"] is DBNull ? 0 : S6.Field<double>("S6__13"),
                          S6__15 = S6.Field<DateTime?>("S6__15"),
                          S6__16 = S6.Field<string>("S6__16"),
                          S6__17 = S6["S6__17"] is DBNull ? 0 : S6.Field<double>("S6__17"),
                          S6__18 = S6["S6__18"] is DBNull ? 0 : S6.Field<double>("S6__18"),
                          S6__20 = S6["S6__20"] is DBNull ? 0 : S6.Field<double>("S6__20"),
                          S6__41 = S6["S6__41"] is DBNull ? 0 : S6.Field<double>("S6__41"),
                          S6__45 = S6.Field<string>("S6__45")
                      };

            base.WriteToLogInformation($"Выполнение запроса с id = 1 завершено успешно\nВыполнение запроса с id = 2");

            var tq2 = (from q0 in tq0
                      join q1 in tq1 on q0.S61__16 equals q1.S6__20 into q0_q1_joined
                      from q0_q1_left_joined in q0_q1_joined.DefaultIfEmpty()
                      select new
                      {
                          q0.ID,
                          q0.Орган,
                          q0.ВхНомерЗапроса,
                          q0.Исполнитель,
                          q0.Фамилия,
                          q0.Имя,
                          q0.Отчество,
                          q0.Идентификационныйномер,
                          q0.ДатаРождения,
                          q0.СерияНомерПаспорта,
                          q0.Овердрафт,
                          Валюта = q0_q1_left_joined == null ? null : "BYN",
                          Остаток = q0_q1_left_joined == null ? (double?)null : q0_q1_left_joined.S6__13 / 100,
                          Арест = q0_q1_left_joined == null ? "Нет" : (q0_q1_left_joined.S6__41 == 9 ? "Да" : "Нет"),
                          ВидОпер = q0_q1_left_joined == null ? (double?)null : q0_q1_left_joined.S6__17,
                          ВидЗачисления = q0_q1_left_joined == null ? (double?)null : q0_q1_left_joined.S6__18,
                          ДатаОткрытияСчета = q0_q1_left_joined == null ? null : q0_q1_left_joined.S6__08,
                          ДатаЗакрытияСчета = q0_q1_left_joined == null ? null : q0_q1_left_joined.S6__09,
                          ДатаПоследнейОперации = q0_q1_left_joined == null ? null : q0_q1_left_joined.S6__15,
                          IBAN = q0_q1_left_joined == null ? "нет данных" : q0_q1_left_joined.S6__45,
                          НомерСчета = q0_q1_left_joined == null ? "нет данных" : "3014000023468-" + q0_q1_left_joined.S6__02 + q0_q1_left_joined.S6__03,
                          ДатаПоследнегоЗачисления = q0_q1_left_joined == null ? null : (q0_q1_left_joined.S6__17 == 30 ? q0_q1_left_joined.S6__15 : null)
                      }).Distinct();

            base.WriteToLogInformation($"Выполнение запроса с id = 2 завершено успешно\nВыполнение запроса с id = 3");

            var tq3 = from q0 in tq0
                      join q1 in tq1 on q0.S61__16 equals q1.S6__20 into q0_q1_joined
                      from q0_q1_left_joined in q0_q1_joined.DefaultIfEmpty()
                      join S88 in dt.Tables["S88"].AsEnumerable() on q0.S61__16 equals S88["S88__02"] into q0_q1_S88_joined
                      from q0_q1_S88_left_joined in q0_q1_S88_joined.DefaultIfEmpty()
                      select new
                      {
                          q0.ID,
                          q0.Орган,
                          q0.ВхНомерЗапроса,
                          q0.Исполнитель,
                          q0.Фамилия,
                          q0.Имя,
                          q0.Отчество,
                          q0.Идентификационныйномер,
                          q0.СерияНомерПаспорта,
                          q0.ДатаРождения,
                          q0.Овердрафт,
                          Валюта = q0_q1_left_joined == null ? null : "BYN",
                          Остаток = q0_q1_left_joined == null ? (double?)null : q0_q1_left_joined.S6__13 / 100,
                          Арест = q0_q1_left_joined == null ? "Нет" : (q0_q1_left_joined.S6__41 == 9 ? "Да" : "Нет"),
                          ВидОпер = q0_q1_left_joined == null ? (double?)null : q0_q1_left_joined.S6__17,
                          ВидЗачисления = q0_q1_left_joined == null ? (double?)null : q0_q1_left_joined.S6__18,
                          ДатаОткрытияСчета = q0_q1_left_joined == null ? null : q0_q1_left_joined.S6__08,
                          ДатаЗакрытияСчета = q0_q1_left_joined == null ? null : q0_q1_left_joined.S6__09,
                          ДатаПоследнейОперации = q0_q1_left_joined == null ? null : q0_q1_left_joined.S6__15,
                          IBAN = q0_q1_left_joined == null ? "нет данных" : q0_q1_left_joined.S6__45,
                          НомерСчета = q0_q1_left_joined == null ? "нет данных" : "3014000222227-" + q0_q1_S88_left_joined["S88__01"],
                          ДатаПоследнегоЗачисления = q0_q1_left_joined == null ? null : (q0_q1_left_joined.S6__17 == 30 ? q0_q1_left_joined.S6__15 : null)
                      };

            base.WriteToLogInformation($"Выполнение запроса с id = 3 завершено успешно"); 

            PutData(pathGet[1], finalColumns, dt, tq2, i, "res1");
            PutData(pathGet[1], finalColumns, dt, tq3, i, "res2");

        } 

        public void Easypay(List<String> pathGet, List<String> extensions, List<String> columns, List<String> finalColumns, List<String> dbase, int i, string colList, string valList)
        {
            base.WriteToLogInformation($"Начало процесса считывания данных");

            ReadData(pathGet[0], dbase, extensions, columns, "out", dbase.Last());

            base.WriteToLogInformation($"Начало выполнения запросов выборки\nВыполнение запроса с id = 0");

            var tq0 = from D in dt.Tables["D"].AsEnumerable()
                      join S61 in dt.Tables["S61"].AsEnumerable() on new { Фамилия = D.Field<string>("Фамилия"), Имя = D.Field<string>("Имя"), Отчество = D.Field<string>("Отчество") } equals new { Фамилия = S61.Field<string>("S61__04"), Имя = S61.Field<string>("S61__05"), Отчество = S61.Field<string>("S61__06") } into D_S61_joined
                      from D_S61_left_joned in D_S61_joined.DefaultIfEmpty()
                      group new { D, D_S61_left_joned } by new { ID = D["ID"], Орган = D["Орган"], ВхНомерЗапроса = D["Вх № запроса"], Исполнитель = D["Исполнитель"], Фамилия = D["Фамилия"], Имя = D["Имя"], Отчество = D["Отчество"], S61__16 = D_S61_left_joned == null ? "" : D_S61_left_joned["S61__16"], Идентификационныйномер = D_S61_left_joned == null ? "" : (D["Идентификационный номер"] == D_S61_left_joned["S61__18"] ? D["Идентификационный номер"] : D_S61_left_joned["S61__18"]), ДатаРождения = D_S61_left_joned == null ? "" : (D["Дата рождения"] == D_S61_left_joned["S61__07"] ? D["Дата рождения"] : D_S61_left_joned["S61__07"]), СерияНомерПаспорта = D["Серия, номер паспорта"], База = D["База"], НомерКошелька = D["Номер кошелька"], ДатаОткрытия = D["Дата открытия"], Остаток = D["Остаток"], Овердрафт = D["Овердрафт"] } into D_S61_grouped
                      orderby D_S61_grouped.Key.ID
                      select new
                      {
                          ID = D_S61_grouped.Key.ID,
                          Орган = D_S61_grouped.Key.Орган,
                          ВхНомерЗапроса = D_S61_grouped.Key.ВхНомерЗапроса,
                          Исполнитель = D_S61_grouped.Key.Исполнитель,
                          Фамилия = D_S61_grouped.Key.Фамилия,
                          Имя = D_S61_grouped.Key.Имя,
                          Отчество = D_S61_grouped.Key.Отчество,
                          S61__16 = D_S61_grouped.Key.S61__16 ?? String.Empty,
                          Идентификационныйномер = D_S61_grouped.Key.Идентификационныйномер,
                          ДатаРождения = D_S61_grouped.Key.ДатаРождения,
                          СерияНомерПаспорта = D_S61_grouped.Key.СерияНомерПаспорта,
                          База = D_S61_grouped.Key.База,
                          НомерКошелька = D_S61_grouped.Key.НомерКошелька,
                          ДатаОткрытия = D_S61_grouped.Key.ДатаОткрытия,
                          Остаток = D_S61_grouped.Key.Остаток,
                          Овердрафт = D_S61_grouped.Key.Овердрафт
                      };

            base.WriteToLogInformation($"Выполнение запроса с id = 0 завершено успешно\nВыполнение запроса с id = 1");

            var tq1 = from S61 in dt.Tables["S61"].AsEnumerable()
                      join S6 in dt.Tables["S6"].AsEnumerable() on S61.Field<double>("S61__16") equals S6.Field<double>("S6__20")
                      where (S6.Field<DateTime?>("S6__09") > DateTime.Today && (S61.Field<string>("S6__02") == "50" || S61.Field<string>("S6__02") == "71" || S6.Field<string>("S6__02") == "61")) || (!S6.Field<DateTime?>("S6__09").HasValue && (S6.Field<string>("S6__02") == "50" || S6.Field<string>("S6__02") == "71" || S6.Field<string>("S6__02") == "61"))
                      orderby S61.Field<double>("S61__16")
                      select new
                      {
                          S61__16 = S61.Field<double>("S61__16"),
                          Счет = S6.Field<string>("S6__02") + S6.Field<string>("S6__03"),
                          S6__08 = S6.Field<DateTime?>("S6__08"),
                          S6__09 = S6.Field<DateTime?>("S6__09"),
                          Остаток = S6.Field<double>("S6__13") / 100
                      };

            base.WriteToLogInformation($"Выполнение запроса с id = 1 завершено успешно\nВыполнение запроса с id = 2");

            var tq2 = from q1 in tq1
                      join C6 in dt.Tables["C6"].AsEnumerable() on q1.Счет equals C6.Field<string>("C6__02")
                      orderby q1.S61__16
                      select new
                      {
                          q1.S61__16,
                          q1.Счет,
                          q1.S6__08,
                          q1.S6__09,
                          q1.Остаток,
                          C6__01 = C6.Field<double>("C6__01")
                      };

            base.WriteToLogInformation($"Выполнение запроса с id = 2 завершено успешно\nВыполнение запроса с id = 3");

            var tq3 = from q2 in tq2
                      join C2 in dt.Tables["C2"].AsEnumerable() on new { S61__16_C2__06 = q2.S61__16, C6__01_C2__01 = q2.C6__01 } equals new { S61__16_C2__06 = C2.Field<double>("C2__06"), C6__01_C2__01 = C2.Field<double>("C2__01") }
                      orderby q2.S61__16
                      select new
                      {
                          q2.S61__16,
                          q2.Счет,
                          q2.S6__08,
                          q2.S6__09,
                          q2.Остаток,
                          q2.C6__01,
                          EP = C2.Field<string>("C2__03").Substring(C2.Field<string>("C2__03").Length - 8)
                      };

            base.WriteToLogInformation($"Выполнение запроса с id = 3 завершено успешно\nВыполнение запроса с id = 4");

            var tq4 = from q0 in tq0
                      join q3 in tq3 on q0.S61__16 equals q3.S61__16 into q0_q3_joined
                      from q0_q3_left_joined in q0_q3_joined.DefaultIfEmpty()
                      where q0_q3_joined != null
                      orderby q0.ID
                      select new
                      {
                          q0.ID,
                          q0.Орган,
                          q0.ВхНомерЗапроса,
                          q0.Исполнитель,
                          q0.Фамилия,
                          q0.Имя,
                          q0.Отчество,
                          q0.S61__16,
                          q0.Идентификационныйномер,
                          q0.ДатаРождения,
                          q0.СерияНомерПаспорта,
                          q0.База,
                          q0.Овердрафт,
                          Номеркошелька = q0_q3_left_joined == null ? null : q0_q3_left_joined.EP,
                          Датаоткрытия = q0_q3_left_joined == null ? null : q0_q3_left_joined.S6__08,
                          Остаток = q0_q3_left_joined == null ? (double?)null : q0_q3_left_joined.Остаток
                      };

            base.WriteToLogInformation($"Выполнение запроса с id = 4 завершено успешно\nВыполнение запроса с id = 5");

            var tq5 = from D in dt.Tables["D"].AsEnumerable()
                      join q4 in tq4 on new { ID = D["ID"], Идентификационныйномер = Convert.ToString(D["Идентификационный номер"]) } equals new { ID = q4.ID, Идентификационныйномер = Convert.ToString(q4.Идентификационныйномер) } into D_q4_joined
                      from D_q4_left_joined in D_q4_joined.DefaultIfEmpty()
                      select new
                      {
                          ID = D["ID"],
                          Орган = D["Орган"],
                          ВхНомерЗапроса = D["Вх № запроса"],
                          Исполнитель = D["Исполнитель"],
                          Фамилия = D["Фамилия"],
                          Имя = D["Имя"],
                          Отчество = D["Отчество"],
                          Идентификационныйномер = D["Идентификационный номер"],
                          ДатаРождения = D["Дата рождения"],
                          СерияНомерПаспорта = D["Серия, номер паспорта"],
                          База = D["База"],
                          Овердрафт = D["Овердрафт"],
                          Номеркошелька = D_q4_left_joined == null ? null : D_q4_left_joined.Номеркошелька,
                          Датаоткрытия = D_q4_left_joined == null ? null : D_q4_left_joined.Датаоткрытия,
                          Остаток = D_q4_left_joined == null ? (double?)null : D_q4_left_joined.Остаток
                      };

            base.WriteToLogInformation($"Выполнение запроса с id = 5 завершено успешно\nВыполнение запроса с id = 6");

            var tq6 = from q5 in tq5
                      join q4 in tq4 on new { ID = q5.ID, Датарождения = q5.ДатаРождения } equals new { ID = q4.ID, Датарождения = q4.ДатаРождения } into q5_q4_joined
                      from q5_q4_left_joined in q5_q4_joined.DefaultIfEmpty()
                      select new
                      {
                          q5.ID,
                          q5.Орган,
                          q5.ВхНомерЗапроса,
                          q5.Исполнитель,
                          q5.Фамилия,
                          q5.Имя,
                          q5.Отчество,
                          q5.Идентификационныйномер,
                          q5.ДатаРождения,
                          q5.СерияНомерПаспорта,
                          q5.База,
                          q5.Овердрафт,
                          Номеркошелька = q5.Номеркошелька == null ? (q5_q4_left_joined == null ? null : q5_q4_left_joined.Номеркошелька) : q5.Номеркошелька,
                          Датаоткрытия = q5.Датаоткрытия == null ? (q5_q4_left_joined == null ? null : q5_q4_left_joined.Датаоткрытия) : q5.Датаоткрытия,
                          Остаток = q5.Остаток == (double?)null ? (q5_q4_left_joined == null ? (double?)null : q5_q4_left_joined.Остаток) : q5.Остаток
                      };

            base.WriteToLogInformation($"Выполнение запроса с id = 6 завершено успешно\nВыполнение запроса с id = 7");

            var tq7 = from q6 in tq6
                      join q4 in tq4 on new { ID = q6.ID, Серияномерпаспорта = q6.СерияНомерПаспорта } equals new { ID = q4.ID, Серияномерпаспорта = q4.СерияНомерПаспорта } into q6_q4_joined
                      from q6_q4_left_joined in q6_q4_joined.Take(1).DefaultIfEmpty()
                      select new
                      {
                          q6.ID,
                          q6.Орган,
                          q6.ВхНомерЗапроса,
                          q6.Исполнитель,
                          q6.Фамилия,
                          q6.Имя,
                          q6.Отчество,
                          q6.Идентификационныйномер,
                          q6.ДатаРождения,
                          q6.СерияНомерПаспорта,
                          q6.База,
                          q6.Овердрафт,
                          Номеркошелька = q6.Номеркошелька == null ? (q6_q4_left_joined == null ? null : q6_q4_left_joined.Номеркошелька) : q6.Номеркошелька,
                          Датаоткрытия = q6.Датаоткрытия == null ? (q6_q4_left_joined == null ? null : q6_q4_left_joined.Датаоткрытия) : q6.Датаоткрытия,
                          Остаток = q6.Остаток == null ? (q6_q4_left_joined == null ? (double?)null : q6_q4_left_joined.Остаток) : q6.Остаток
                      };

            base.WriteToLogInformation($"Выполнение запроса с id = 7 завершено успешно");

            PutData(pathGet[1], finalColumns, dt, tq7, i);
        }

        private void ReadData(string path, List<String> dbase, List<String> extensions, List<String> columns, string output, string input)
        {
            base.WriteToLogInformation($"Начало процесса считывания данных");

            DirectoryInfo dir = new DirectoryInfo(path);
            if (File.Exists(path + output + ".xls"))
            {
                base.WriteToLogInformation($"Старый файл {path + output}.xls был удалён ");
                File.Delete(path + output + ".xls");
            }
            foreach (FileInfo file in dir.GetFiles())
            {
                if (dbase.Contains(file.Name.Split('.').First()))
                {
                    if (extensions.Contains(file.Name.Split('.').Last()) == true)
                    {
                        if (file.Name.Split('.').Last() == "DBF")
                        {
                            conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=dBASE IV;";
                            cmd = new OleDbCommand("SELECT * FROM [" + file + "$]", conn);
                        }
                        else
                        {
                            conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file.FullName + ";Extended Properties='Excel 8.0;HDR=Yes;'";
                            cmd = new OleDbCommand("SELECT * FROM [" + file.Name.Split('.').First() + "$]", conn);
                        }
                        try
                        {
                            conn.Open();
                            DataTable t = dt.Tables.Add(file.Name.Split('.').First());
                            t.Load(cmd.ExecuteReader());
                            conn.Close();
                        }
                        catch (OleDbException oleDbException)
                        {
                            base.WriteToLogError($"Ошибка обнаружения файла {file.FullName} Подробней: {oleDbException}");
                        }
                        catch (Exception exception)
                        {
                            base.WriteToLogError($"Ошибка (неопознанная). Подробней: {exception}");
                        }
                    }
                }

                base.WriteToLogInformation($"Данные с файла {file.FullName} были загружены в память для исполнения запросов");
            }

            base.WriteToLogInformation($"Процесс считывания данных завершён успешно");
            if (dt.Tables[input] == null)
            {
                base.WriteToLogError($"Ошибка обнаружения файла эксель со входными данными {dir.FullName+input}. Добавьте файл, убедитесь что его расширение входит в список расширений для считывания и попробуйте снова ");
                throw new NullReferenceException();
            }

            foreach (string column in columns)
            {               
                DataColumnCollection collection = dt.Tables[input].Columns;

                base.WriteToLogInformation($"Проверка наличия столбцов во входной эксель таблице");

                if (collection.Contains(column) == false)
                {
                    dt.Tables[input].Columns.Add(column);
                    base.WriteToLogInformation($"Был добавлен пустой столбец {column} т.к. первоначально он не был обнаружен");
                }
                else
                    base.WriteToLogInformation($"Столбец {column} был успешно обнаружен");
            }
            for (int x = 0; x < dt.Tables[input].Rows.Count; x++)
                dt.Tables[input].Rows[x].SetField("ID", x + 1);

            base.WriteToLogInformation($"Столбец ID был заполнен инкрементируемыми значениями от 1");
        }
        private void PutData(string path, List<String> finalColumns, DataSet dt, dynamic tq, int i, string sampleFileName ="res", string colList = "", string valList = "")
        {
            base.WriteToLogInformation($"Начало процесса выгрузки данных выборки из памяти в файл");

            dt.Tables.Add(sampleFileName);

            base.WriteToLogInformation($"Промежуточная таблица {sampleFileName} успешно создана в памяти");

            foreach (string finalColumn in finalColumns)
            {
                DataColumnCollection collection = dt.Tables[sampleFileName].Columns;
                dt.Tables[sampleFileName].Columns.Add(finalColumn);

                base.WriteToLogInformation($"Столбец {finalColumn} успешно создан в промежуточной таблице {sampleFileName}");
            }
            base.WriteToLogInformation($"Запущен процесс построчного перенесения данных из финального запроса в таблицу");

            switch (Thread.CurrentThread.Name)
            {
                case ("DelaySample"):
                    PlacingFinalResponceDataToTableDelay();
                    break;
                case ("EasypaySample"):
                    PlacingFinalResponceDataToTableEasypay();
                    break;
                default:
                    break;
            }
          
            void PlacingFinalResponceDataToTableDelay()
            {
                foreach (var row in tq)
                {
                    if (row.Имя.ToString() != "")
                    {
                        dt.Tables[sampleFileName].Rows.Add();
                        dt.Tables[sampleFileName].Rows[i][0] = row.ID;
                        dt.Tables[sampleFileName].Rows[i][1] = row.Орган;
                        dt.Tables[sampleFileName].Rows[i][2] = row.ВхНомерЗапроса;
                        dt.Tables[sampleFileName].Rows[i][3] = row.Исполнитель;
                        dt.Tables[sampleFileName].Rows[i][4] = row.Фамилия;
                        dt.Tables[sampleFileName].Rows[i][5] = row.Имя;
                        dt.Tables[sampleFileName].Rows[i][6] = row.Отчество;
                        dt.Tables[sampleFileName].Rows[i][7] = row.Идентификационныйномер;
                        dt.Tables[sampleFileName].Rows[i][8] = row.ДатаРождения;
                        dt.Tables[sampleFileName].Rows[i][9] = row.СерияНомерПаспорта;
                        dt.Tables[sampleFileName].Rows[i][10] = row.Валюта;
                        dt.Tables[sampleFileName].Rows[i][11] = row.Овердрафт;
                        dt.Tables[sampleFileName].Rows[i][12] = row.Остаток;
                        dt.Tables[sampleFileName].Rows[i][13] = row.ДатаПоследнегоЗачисления;
                        dt.Tables[sampleFileName].Rows[i][14] = row.Арест;
                        dt.Tables[sampleFileName].Rows[i][15] = row.ДатаОткрытияСчета;
                        dt.Tables[sampleFileName].Rows[i][16] = row.ДатаЗакрытияСчета;
                        dt.Tables[sampleFileName].Rows[i][17] = row.ДатаПоследнейОперации;
                        dt.Tables[sampleFileName].Rows[i][18] = row.IBAN;
                    }
                    i++;
                }
            }

            void PlacingFinalResponceDataToTableEasypay()
            {
                foreach (var row in tq)
                {
                    if (row.Имя.ToString() != "")
                    {
                        dt.Tables[sampleFileName].Rows.Add();
                        dt.Tables[sampleFileName].Rows[i][0] = row.ID;
                        dt.Tables[sampleFileName].Rows[i][1] = row.Орган;
                        dt.Tables[sampleFileName].Rows[i][2] = row.ВхНомерЗапроса;
                        dt.Tables[sampleFileName].Rows[i][3] = row.Исполнитель;
                        dt.Tables[sampleFileName].Rows[i][4] = row.Фамилия;
                        dt.Tables[sampleFileName].Rows[i][5] = row.Имя;
                        dt.Tables[sampleFileName].Rows[i][6] = row.Отчество;
                        dt.Tables[sampleFileName].Rows[i][7] = row.Идентификационныйномер;
                        dt.Tables[sampleFileName].Rows[i][8] = row.ДатаРождения is DBNull ? row.ДатаРождения : Convert.ToDateTime(row.ДатаРождения).ToString("dd.MM.yyyy");
                        dt.Tables[sampleFileName].Rows[i][9] = row.СерияНомерПаспорта;
                        dt.Tables[sampleFileName].Rows[i][10] = row.Овердрафт;
                        dt.Tables[sampleFileName].Rows[i][11] = row.Номеркошелька;
                        dt.Tables[sampleFileName].Rows[i][12] = row.Датаоткрытия == null ? "Нет данных" : Convert.ToDateTime(row.Датаоткрытия).ToString("dd.MM.yyyy");
                        dt.Tables[sampleFileName].Rows[i][13] = row.Остаток;
                    }
                    i++;
                }
            }

                base.WriteToLogInformation($"Процесс построчного перенесения завершён успешно");

            try
            {
                conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + "\\"+sampleFileName+".xls;Extended Properties='Excel 8.0;HDR=No;READONLY=False;IMEX=0'";
                conn.Open();
                cmd.Connection = conn;

                base.WriteToLogInformation($"Открытие соединения с файлом {path + sampleFileName}.xls установлено успешно");

                for (int z = 0; z < finalColumns.Count; z++)
                {
                    if (z == finalColumns.Count - 1)
                        colList = colList + @"[" + finalColumns[z] + "] VARCHAR";
                    else
                        colList = colList + @"[" + finalColumns[z] + "] VARCHAR, ";
                }
                cmd.CommandText = string.Format("CREATE TABLE ["+ sampleFileName + "] ({0})", colList);

                base.WriteToLogInformation($"Запрос на создание таблицы успешно отработал");

                cmd.ExecuteNonQuery();
                colList = "";
                foreach (string col in finalColumns)
                {
                    if (finalColumns.Last() == col)
                        colList = colList + "[" + col + "]";
                    else
                        colList = colList + "[" + col + "], ";
                }
                for (int z = 0; z < dt.Tables[sampleFileName].Rows.Count; z++)
                {
                    for (int q = 0; q < dt.Tables[sampleFileName].Columns.Count; q++)
                    {
                        if (q == dt.Tables[sampleFileName].Columns.Count - 1)
                            valList = valList + "'" + dt.Tables[sampleFileName].Rows[z][q] + "'";
                        else
                            valList = valList + "'" + dt.Tables[sampleFileName].Rows[z][q] + "', ";

                    }
                    cmd.CommandText = string.Format("INSERT INTO ["+ sampleFileName + "] ({0}) VALUES ({1})", colList, valList);

                    cmd.ExecuteNonQuery();
                    valList = "";
                }
                base.WriteToLogInformation($"Заполнение таблицы данными успешно произведено");

                conn.Close();

                base.WriteToLogInformation($"Закрытие соединения с файлом {path + sampleFileName}.xls проведено успешно");
            }
            catch (Exception exception)
            {
                base.WriteToLogError($"Ошибка (неопознанная). Подробней: {exception}");
                throw exception;
            }

            base.WriteToLogInformation($"Конец процесса выгрузки данных выборки из памяти в файл");

        }
    }
}
