using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tarefas.Database
{
    class Constants
    {
        public const string Databasefilename = "Tarefa.db3";

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, Databasefilename);
            }
        }

    }
}
