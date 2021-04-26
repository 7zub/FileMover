using FileMover.model;
using System;
using System.IO;
using static FileMover.context.EnumContext;

namespace FileMover
{
    public class FileWatcher : IDisposable
    {
        public Response res;
        private RuleItem rule;
        private FileSystemWatcher watcher;

        public FileWatcher(RuleItem rule)
        {
            try
            {
                this.rule = rule;
                FWCheck();        // ФЛК правила (мало ли)
                FWExecuteRule();  // обрабатываем файлы, что уже были до запуска FW
                FWInstance();     // реализация FW для правила

                res = new Response()
                {
                    respType = Response.RespType.Success,
                    Message = "Правило № " + rule.Id + " запущено"
                };
            }
            catch (Exception ex)
            {
                res.respType = Response.RespType.Error;
                res.Message = "Ошибка запуска правила № " + rule.Id + ":\n" + ex.Message;
            }
        }

        private void FWCheck()
        {

        }

        public void FWInstance()
        {
            watcher = new FileSystemWatcher();
            watcher.Path = rule.DirStart;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;
            watcher.Filter = rule.FileMask;
            watcher.Created += FWEvent;
            watcher.Changed += FWEvent;
            watcher.EnableRaisingEvents = true;
        }

        private void FWEvent(object source, FileSystemEventArgs e)
        {
            watcher.EnableRaisingEvents = false;
            FWExecuteRule();
            watcher.EnableRaisingEvents = true;
            //Console.WriteLine("Правило: {0} || Файл: {1}:{2}", rule.DirDest, e.FullPath, e.ChangeType);
        }

        public void FWExecuteRule()
        {
            string[] dir = Directory.GetFiles(rule.DirStart, rule.FileMask);

            for (int i = 0; i < dir.Length; i++)
            {
                string p = rule.DirDest + @"\" + Path.GetFileName(dir[i]);

                try
                {
                    //if (Path.GetFileNameWithoutExtension(dir[i]) == "ff")
                    //    throw new Exception("Что за чертивщина!");

                    File.Copy(
                    dir[i],
                    rule.IfExist == ifEx.rename ? GenNewFName(p) : p,
                    rule.IfExist == ifEx.rewrite
                );

                    if (rule.Operation == op.move)
                        File.Delete(dir[i]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
        }

        private string GenNewFName(string file)
        {
            int i = 1;
            string GenName = file;

            while (File.Exists(GenName))
                GenName = Path.GetDirectoryName(file) + @"\" + Path.GetFileNameWithoutExtension(file) + Const.RenameFile + i++ + Path.GetExtension(file);

            return GenName;
        }

        public void Dispose()
        {
            watcher.Dispose();
        }
    }
}