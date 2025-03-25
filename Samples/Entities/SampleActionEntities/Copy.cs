using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Samples.Entities.SampleActionEntities;

namespace Samples
{
    internal class Copy : LoggedSampleAction
    {

        internal void CopyFiles(List<String> dbase, List<string> extensions, List<string> pathCopyFrom, List<string> pathCopyTo, int dbaseType = 0, List<string> pathList = null, int dbaseCounterFrom = 0, int dbaseCounterTo = 0, int PathTo = 0, int pathCopyIndex = 0)
        {
            foreach (string path in pathCopyTo)
            {
                Directory.CreateDirectory(path);
                base.WriteToLogInformation($"Рабочий каталог для выборок {path} уже существует либо успешно создан");
            }
            base.WriteToLogInformation($"Процесс создания входного и выходного каталогов для выборок успешно завершён");


            if (dbaseCounterFrom == 0)
            {
                pathList = new List<string>();
                if (dbaseType == 1)
                {
                    dbaseCounterTo = pathCopyIndex = 2;
                }
                else
                    dbaseCounterTo = dbase.Count - 1;
            }
            else
            {
                PathTo = 1;
            }

            base.WriteToLogInformation($"Переход к циклу копирования баз с входных каталогов");

            for (; dbaseCounterFrom < dbaseCounterTo; dbaseCounterFrom++)
            {
                int bufferSize = 1024 * 1024;

                base.WriteToLogInformation($"Буфер для побитового чтения успешно создан");

                try
                {
                    using (FileStream fileStream = new FileStream(pathCopyTo.First() + dbase[dbaseCounterFrom] + "\u002E" + extensions[extensions.Count - 1], FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                    {
                        base.WriteToLogInformation($"Файловый поток для базы {fileStream.Name} успешно создан");

                        using (FileStream fs = new FileStream(pathCopyFrom[PathTo] + dbase[dbaseCounterFrom] + "\u002E" + extensions[extensions.Count - 1], FileMode.Open, FileAccess.ReadWrite))
                        {
                            base.WriteToLogInformation($"Файловый поток для базы {fs.Name} успешно создан");

                            fileStream.SetLength(fs.Length);
                            int bytesRead = -1;
                            byte[] bytes = new byte[bufferSize];

                            while ((bytesRead = fs.Read(bytes, 0, bufferSize)) > 0)
                            {
                                fileStream.Write(bytes, 0, bytesRead);
                            }
                        }

                    }
                    base.WriteToLogInformation($"Копирование файла с базой успешно завершено. Файловые потоки закрыты");
                }
                catch (FileNotFoundException fileNotFoundException)
                {
                    base.WriteToLogError($"Ошибка нахождения баз. Проверьте расположение входных баз для копирования. Подробней: {fileNotFoundException}");
                    throw;
                }
                catch (IOException iOException)
                {
                    base.WriteToLogError($"Ошибка ввода-вывода. Вероятно, файл базы уже редактируется другим пользователем. Удостоверьтесь, что запущен только один процесс выборки. Подробней: {iOException}");
                    throw;
                }
                catch (Exception exception)
                {
                    base.WriteToLogError($"Ошибка (неопознанная). Подробней: {exception}");
                    throw;
                }

                pathList.Add(pathCopyTo.First() + dbase[dbaseCounterFrom] + "\u002E" + extensions[extensions.Count - 1]);
            }
            if (dbaseCounterFrom < dbase.Count - 1)
            {
                base.WriteToLogInformation($"Все требуемые файлы баз с текущего каталога скопированы. Переход к копированию баз для следующего каталога по списку");

                CopyFiles(dbase, extensions, pathCopyFrom, pathCopyTo, dbaseType, pathList, dbaseCounterFrom, dbase.Count - 1, PathTo, pathCopyIndex);
            }
            else
            {
                CheckFile(pathList);
                base.WriteToLogInformation($"Процесс копирования успешно завершён");
            }              

        }

        private void CheckFile(List<string> pathList)
        {
            base.WriteToLogInformation($"Начало проверки наличия необходимых скопированных баз во входящем рабочем каталоге");

            foreach (string path in pathList)
            {
                if (File.Exists(path) == false)
                {
                    base.WriteToLogError($"Ошибка проверки файлов для обработки. Ожидаемый файл {path} отсутствует");
                    throw new InvalidOperationException();
                }
            }
            base.WriteToLogInformation($"Наличие всех необходимых для выборки баз в рабочем каталоге подтверждено");
        }
    }
}
